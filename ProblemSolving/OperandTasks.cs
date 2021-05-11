using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{
    class OperandTasks
    {
        public static StringBuilder temp = new StringBuilder();

        //Loops and merges digits into one entity until an operator is found. Returns entity(number) as string and current index of the equation. 
        public static string MultiDigitChecker(int index, string equation , out int currentIndex)
        {
            temp.Clear();
            while(Char.IsDigit(equation[index]) || equation[index] == '.' || equation[index] == '-' && NegativeChecker(index, equation))
            {
                temp.Append(equation[index]);
                if(index == equation.Length - 1)
                {
                    break;
                }
                else
                {
                    index++;
                }
               
            }


            temp.Append(" ");
            currentIndex = index;
            return temp.ToString();
        }

        //Returns true if - is a negative and not a minus.
        public static bool NegativeChecker(int index, string equation)
        {
            if(index == 0)
            {
                return true;
            } 

            if(!Char.IsDigit(equation[index - 1]) && (equation[index - 1] != ')'))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
