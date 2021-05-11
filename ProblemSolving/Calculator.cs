using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{
    class Calculator
    {
        public string Expression { get; set; }

        public static Stack<char> queue = new Stack<char>(); //Holds operators temporarily until precedence is determined. 
        public static StringBuilder rpnString = new StringBuilder(); //StringBuilder which will hold the RPN conversion.
        public static String[] strArray;//String array to pass rpnString into RPNSolver

        public static string answer;


        public string InfixToRPNConverter()
        {
            rpnString.Clear();
            queue.Clear();

            //Checks to see if expression contains and ans, if so it replaces it with the answer of the last expression.
            if (this.Expression.Contains("ans"))
            {
                int ansBeginning = this.Expression.IndexOf("a");
                if(!OperatorTasks.IsOperator(this.Expression[ansBeginning - 1]))
                {
                    return "Invalid equation. Please retry.";
                }
                else
                {
                    this.Expression = this.Expression.Replace("ans", answer);
                }
                
            }

            //If expression is only one character check to see if it's a digit if so return. Else return invalid expression. 
            if (this.Expression.Length == 1)
            {
                if (Char.IsDigit(this.Expression[0]))
                {
                    answer = this.Expression[0].ToString();
                    return answer;
                }
                else
                {
                    return "Invalid expression. Please retry.";
                }
            }


            //Loops through infix equation string
            for (int i = 0; i <= this.Expression.Length; i++)
            {
                //Error handling to see if there are unwanted characters found in front of the expression.
                if (i == 0)
                {
                    if (!Char.IsDigit(this.Expression[i]) && this.Expression[i]!='-' && this.Expression[i] != '(')
                    {
                            return "Invalid expression. Please retry.";
                    }
                    else if(this.Expression[i] == '-' && !Char.IsDigit(this.Expression[i+1]))
                    {
                            return "Invalid expression. Please retry.";
                    }
                }

                #region If statement for when i equals length of expression
                //If i is equal to length of infix equation string pop and append last object in stack.
                if (i == this.Expression.Length)
                {
                    //Try catch to throw exception if any opening parenthesis is found in queue.
                    try
                    {
                        if (queue.Count > 0)
                        {
                            if (queue.Peek() == '(')
                            {
                                throw new Exception();
                            }
                            else
                            {
                                while (queue.Count > 0)
                                {
                                    rpnString.Append(queue.Pop());
                                    rpnString.Append(" ");
                                }
                            }

                        }
                        strArray = rpnString.ToString().Trim().Split();
                        break;
                    }
                    catch (Exception)
                    {
                        return "Opening parenthesis was never closed. Please retry.";
                    }

                }
                #endregion

                #region If statement to determine if char is a number.
                //If current char is a digit or a negative symbol.
                if (Char.IsDigit(this.Expression[i]) || this.Expression[i] == '-' && OperandTasks.NegativeChecker(i, this.Expression))
                {
                    //Combines multiple digit or negative numbers into one entity before appending.
                    rpnString.Append(OperandTasks.MultiDigitChecker(i, this.Expression, out int currentIndex));

                    //Sets i to the index that is after number that was just returned. 
                    i = currentIndex;

                    //Error handling for missing operator in front of parenthesis. 
                    if (this.Expression[i] == '(')
                    {
                        return "Operator not found before opening parenthesis. Please retry.";
                    }
                }
                #endregion

                #region If statement to determine if char is an operator.
                if (OperatorTasks.IsOperator(this.Expression[i]) && i != this.Expression.Length)
                {
                    if (this.Expression[i] == '(')
                    {
                        queue.Push(this.Expression[i]);
                    }

                    //If char is closing parenthesis loop through queue and append each operator to rpnString because parenthesis have the highest precedence.
                    else if (this.Expression[i] == ')')
                    {
                        //Try catch to determine if closing parenthesis has a matching opening parenthesis 
                        try
                        {
                            if (queue.Count() == 0)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                while (queue.Count() > 0)
                                {

                                    if (queue.Peek() != '(')
                                    {
                                        rpnString.Append(queue.Pop());
                                        rpnString.Append(" ");
                                    }
                                    else if (queue.Peek() == '(')
                                    {
                                        queue.Pop();
                                        break;
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            return "Mismatched parenthesis. Please retry.";
                        }

                    }
                    else
                    {
                        if (queue.Count > 0)
                        {
                            //Loop while count is > 0 and until operator on the top of stack is lower precedence than current char.
                            while (queue.Count > 0 && OperatorTasks.OperatorPrecedence(queue.Peek()) >= OperatorTasks.OperatorPrecedence(this.Expression[i]))
                            {
                                if (queue.Peek() == '(')
                                {
                                    break;
                                }

                                else
                                {
                                    rpnString.Append(queue.Pop());
                                    rpnString.Append(" ");

                                }
                            }
                            queue.Push(this.Expression[i]);
                        }
                        else
                        {
                            queue.Push(this.Expression[i]);
                        }
                    }
                }
                #endregion


                //Error handling to see if there are unwanted characters found within the expression.
                else if(i != this.Expression.Length && !Char.IsDigit(this.Expression[i]) )
                {
                    return "Invalid equation. Please retry.";
                }

            }
            try
            {
                answer = RPNSolver.RPNCalculator(strArray);
                return answer;
            }
            catch(Exception)
            {
                return "Invalid equation. Please retry.";
            }
         
        }
    }
}