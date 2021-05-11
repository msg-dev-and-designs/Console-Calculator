using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{
    static class RPNSolver
    {
        //Holds RPN equation 
        public static Stack<double> operandStack = new Stack<double>();

        
        public static string RPNCalculator(string[] rpnExpression)
        {

            foreach (string s in rpnExpression)
            {
                double temp;
                bool isDouble = double.TryParse(s, out temp);

                //If s is a number push to stack.
                if (isDouble)
                {
                    operandStack.Push(temp);
                }

                //If s is an operator pop the two number on the top of stack and preform operation. Push answer back on to stack.
                else if(operandStack.Count > 0)
                {
                    double num2 = operandStack.Pop();
                    double num1 = operandStack.Pop();
                    double answer;
                    switch (Char.Parse(s))
                    {
                        case '/':
                            if(num2 == 0)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                answer = num1 / num2;
                                operandStack.Push(answer);
                                break;
                            }

                        case '*':
                            answer = num1 * num2;
                            operandStack.Push(answer);
                            break;

                        case '+':
                            answer = num1 + num2;
                            operandStack.Push(answer);
                            break;

                        case '-':
                            answer = num1 - num2;
                            operandStack.Push(answer);
                            break;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            return operandStack.Pop().ToString();
        
         
            
        }
    }
}
