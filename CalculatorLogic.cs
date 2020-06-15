using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorLogic
    {
        private LinkedList<Token> equation;

        public string inputText = "";

        private string outputText = "";
        private long outputValue;

        public delegate void OutputHandler(string output);
        public event OutputHandler updateOutput;

        //This validates the new text before parsing into tokens, then validating the tokens are a valid equation before finally calculating it and outputting it
        public void Calculate()
        {
            if (inputText.Length > 0)
            {
                if (inputTextIsValid())
                {
                    createTokenList();
                    if (tokenListIsValid())
                    {
                        calculateOutput();
                    }
                    else
                    {
                        outputText = "ERROR";
                        updateOutput(outputText);
                    }
                }
                else
                {
                    outputText = "ERROR";
                    updateOutput(outputText);
                }
            }
            else
            {
                outputText = "";
                updateOutput(outputText);
            }
        }

        //This creates a single number token for a contiguous string of numbers or a token for each individual + or * character this can easily be extended to add division and subtraction
        private void createTokenList()
        {
            equation = new LinkedList<Token>();
            Token currentToken = new Token();
            string input = "";

            for (int i = 0; i < inputText.Length; i++)
            {
                switch(inputText[i])
                {
                    case '+':
                    case '*':
                    case '/':
                    case '-':

                        input += inputText[i];
                        currentToken.addToTokenString(input);
                        equation.AddLast(currentToken);

                        input = "";
                        currentToken = new Token();
                        break;

                    default:
                        while (char.IsNumber(inputText[i]))
                        {
                            input += inputText[i++];

                            if (i >= inputText.Length)
                            {
                                break;
                            }
                        }

                        currentToken.addToTokenString(input);
                        equation.AddLast(currentToken);

                        input = "";
                        currentToken = new Token();
                        i--;
                        break;
                }
            }

        }
        private bool inputTextIsValid()
        {
            foreach (char inputChar in inputText)
            {
                if (char.IsNumber(inputChar))
                {
                    continue;
                }
                else
                {
                    switch (inputChar)
                    {
                        case '+':
                        case '*':
                        case '/':
                        case '-':
                            continue;
                        default:
                            return false;
                    }
                }
            }
            return true;
        }

        private enum state
        {
            start,
            num,
            op,
            err,
            end
        }
        //State machine for validation token list
        private bool tokenListIsValid()
        {
            state validationState = state.start;
            LinkedListNode<Token> currentToken = equation.First;

            while (validationState != state.end)
            {
                switch (validationState)
                {
                    case state.start:
                        if (currentToken.Value.tokenType != Token.type.num)
                        {
                            validationState = state.err;
                        }
                        else
                        {
                            validationState = state.num;
                            currentToken = currentToken.Next;
                        }
                        break;

                    case state.num:
                        if (currentToken == null)
                        {
                            validationState = state.end;
                        }
                        else if (currentToken.Value.tokenType == Token.type.num)
                        {
                            validationState = state.err;
                        }
                        else
                        {
                            validationState = state.op;
                            currentToken = currentToken.Next;
                        }

                        break;

                    case state.op:
                        if (currentToken == null)
                        {
                            validationState = state.err;
                        }
                        else if (currentToken.Value.tokenType == Token.type.num)
                        {
                            validationState = state.num;
                            currentToken = currentToken.Next;
                        }
                        else
                        {
                            validationState = state.err;
                        }
                        break;

                    case state.err:
                        return false;
                }
            }
            return true;
        }
    
        private void calculateOutput()
        {
           LinkedListNode<Token> currentToken = equation.First;
           long newValue;

            while(currentToken != null)
            {
                if (currentToken.Value.tokenType == Token.type.mul)
                {
                    newValue = currentToken.Previous.Value.tokenVal * currentToken.Next.Value.tokenVal;
                    updateTokenList(currentToken, newValue);
                }
                else if(currentToken.Value.tokenType == Token.type.div)
                {
                    newValue = currentToken.Previous.Value.tokenVal / currentToken.Next.Value.tokenVal;
                    updateTokenList(currentToken, newValue);
                }

                currentToken = currentToken.Next;
            }

            currentToken = equation.First;
            while (currentToken != null)
            {
                if (currentToken.Value.tokenType == Token.type.add)
                {
                    newValue = currentToken.Previous.Value.tokenVal + currentToken.Next.Value.tokenVal;
                    updateTokenList(currentToken, newValue);
                }
                else if (currentToken.Value.tokenType == Token.type.sub)
                {
                    newValue = currentToken.Previous.Value.tokenVal - currentToken.Next.Value.tokenVal;
                    updateTokenList(currentToken, newValue);
                }

                currentToken = currentToken.Next;
            }

            currentToken = equation.First;
            outputValue = currentToken.Value.tokenVal;
            outputText = outputValue.ToString();  
            updateOutput(outputText);
        }

        private void updateTokenList(LinkedListNode<Token> currentToken, long newValue)
        {
            currentToken.Value = new Token(newValue);
            equation.Remove(currentToken.Previous);
            equation.Remove(currentToken.Next);
        }
    }
}
