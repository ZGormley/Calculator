﻿using System;
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
            add,
            mul,
        }
        public type? tokenType;
        public int tokenVal;

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

                        default:
                            tokenType = type.num;
                            break;
                    }
                }
            }
        }
        private string tokenString = "";

        public Token(int inputInt)
        {
            tokenVal = inputInt;
            TokenString = tokenVal.ToString();         
        }
        public Token()
        {
        }
                          
        private void updateTokenVal()
        {
            if(tokenType == type.num)
                tokenVal = int.Parse(TokenString);
        }

        public void addToTokenString(string input)
        {
                TokenString += input;

            updateTokenVal();
        }
    }
}