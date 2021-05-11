using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{
    static class OperatorTasks
    {
        //Returns true is char is an operator. 
        public static bool IsOperator(char c)
        {
            if (c == '-' || c == '+' || c == '/' || c == '*' || c == '(' || c == ')')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Returns int representing the precedence of the current operator. 
        public static int OperatorPrecedence(char c)
        {
            switch (c)
            {
                case '(':
                    return 3;

                case '/':
                    return 2;


                case '*':
                    return 2;


                case '+':
                    return 1;


                case '-':
                    return 1;

                default:
                    return 0;

            }
        }
    }
}
