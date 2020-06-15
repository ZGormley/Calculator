using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Token
    {

        //Token types could be extended to add an equation token for parenteses, division and subtraction tokens
        //Division and addition tokens could also just be simplified into add and mul toekns after modifying the value to be negative for subtraction or inverse for division for
        public enum type
        {
            num,
            mul,
            div,
            add,
            sub
        }
        public type? tokenType;
        public long tokenVal;

        public string TokenString
        {
            get { return tokenString; }
            set 
            {
                tokenString = value;

                if(tokenType == null)
                {
                    switch(tokenString)
                    {
                        case "*":
                            tokenType = type.mul;
                            break;

                        case "+":
                            tokenType = type.add;
                            break;

                        case "-":
                            tokenType = type.sub;
                            break;

                        case "/":
                            tokenType = type.div;
                            break;

                        default:
                            tokenType = type.num;
                            break;
                    }
                }
            }
        }
        private string tokenString = "";

        public Token(long inputInt)
        {
            tokenVal = inputInt;
            TokenString = tokenVal.ToString();         
        }
        public Token()
        {
        }
                          
        private bool updateTokenVal()
        {
            try
            {
                if (tokenType == type.num)
                    tokenVal = long.Parse(TokenString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool addToTokenString(string input)
        {
            TokenString += input;

            if (updateTokenVal())
                return true;
            return false;
        }
    }
}
