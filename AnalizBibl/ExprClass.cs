using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizBibl
{
    public class ExprClass
    {
        //static int countOp = 0;
        int number;
        ExprClass first, second, operation;

        string sValue;

        public ExprClass(string sValue)
        {
            this.sValue = sValue;
        }
        public ExprClass(ExprClass second, ExprClass first, ExprClass operation, int number)
        {
            this.number = number;
            this.sValue = "";
            this.first = first;
            this.second = second;
            this.operation = operation;
        }
        public string RPN()
        {
            string res = "";

            if (first != null && second != null)
            {
                    
                if (first.sValue == "")
                {
                    res += first.RPN();
                }
                else
                {
                    res += first.sValue + " ";
                }
                if (second.sValue == "")
                {
                    res += second.RPN();
                }
                else
                {
                    res += second.sValue + " ";
                }    
                if (operation.sValue != "")
                {
                    res += operation.sValue + " ";
                }
            }
            return res;

        }
        public string GetValue { get { return sValue; } }
        public ExprClass First { get { return first; } }
        public ExprClass Second { get { return second; } }
        public ExprClass Operation { get { return operation; } }
        public int Number { get { return number; } }
        
        public override string ToString()
        {
            string result = "";
            
            if (first != null && second != null) 
            { 
                result += first.ToString();

                result += second.ToString();

                result += "(M" + number + "): ";
                if (operation.sValue != "")
                {
                    result += operation.sValue + " ";
                }
                if (first.sValue == "")
                {
                    result += "(M" + first.number + ")";
                }
                else
                {
                    result += first.GetValue + " ";
                }
                if (second.sValue == "")
                {
                    result += "(M" + second.number + ")";
                }
                else
                {
                    result += second.GetValue;
                }
                result += "\n";
            }
            
            return result;
        }
    }
}
