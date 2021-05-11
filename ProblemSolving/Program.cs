using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{

    public class Program
    {
        public static string ProcessCommand(string Expression)
        {
            try
            {
                // TODO Evaluate the expression and return the result
                Calculator mycalc = new Calculator();
                mycalc.Expression = Expression.Replace(" ", string.Empty);
                return mycalc.InfixToRPNConverter(); 
            }
            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }
        }

        static void Main(string[] args)
        {
            string Expression;
            while ((Expression = Console.ReadLine()) != "exit")
            {
                Console.WriteLine(ProcessCommand(Expression));
            }
        }
    }
}
