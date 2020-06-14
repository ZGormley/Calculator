using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorLogic
    {
        private static CalculatorLogic instance;
        public static CalculatorLogic Instance 
        {
            get    
            {   
                if (instance == null)
                    {
                        instance = new CalculatorLogic();
                    }
                    return instance;
            }
        }

        public string inputText = "";
        private LinkedList<Token> equation;
        private enum state
        {
            start,
            num,
            op,
            err,
            end
        }
        public int outputValue;
        private string outputText;

        //This validates the new text before parsing into tokens, then validating the tokens are a valid equation before finally calculating it and outputting it
        public void updateInputText(string newInput)
        {
            inputText = newInput;
            if (inputTextIsValid())
            {
                parseInput();
                if(tokenListIsValid())
                {
                    calculateOuput();
                }
                else
                {
                    outputText = "ERROR";
                    MainWindow.instance.updateOutput(outputText);
                }
            }
            else
            {
                outputText = "ERROR";
                MainWindow.instance.updateOutput(outputText);
            }
        }

        //This creates a single number token for a contiguous string of numbers or a token for each individual + or * character this can easily be extended to add division and subtraction
        private void parseInput()
        {
            equation = new LinkedList<Token>();
            Token currentToken = new Token();
            string input = "";

            for (int i = 0; i < inputText.Length; i++)
            {
                if (inputText[i] == '+')
                {
                    input += inputText[i];
                    currentToken.addToTokenString(input);
                    equation.AddLast(currentToken);

                    input = "";
                    currentToken = new Token();
                    
                }

                else if (inputText[i] == '*')
                {
                    input += inputText[i];
                    currentToken.addToTokenString(input);
                    equation.AddLast(currentToken);

                    input = "";
                    currentToken = new Token();
                }

                else if (char.IsNumber(inputText[i]))
                {
                    while (char.IsNumber(inputText[i]))
                    {
                        input += inputText[i++];

                        if(i >= inputText.Length)
                        {
                            break;
                        }
                    }

                    currentToken.addToTokenString(input);
                    equation.AddLast(currentToken);

                    input = "";
                    currentToken = new Token();
                    i--;
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
                else if (inputChar == '*' || inputChar == '+')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //A simple state machine to validate the tokenlist as an equation
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

        private void calculateOuput()
        {
           LinkedListNode<Token> CurrentToken = equation.First;
            int newValue;

            while (CurrentToken.Next != null)
            {
                while (CurrentToken.Value.tokenType != Token.type.mul)
                {
                    if (CurrentToken.Next == null)
                    {
                        break;
                    }
                    else
                    {
                        CurrentToken = CurrentToken.Next;
                    }
                }

                if (CurrentToken.Value.tokenType == Token.type.mul)
                {
                    newValue = CurrentToken.Previous.Value.tokenVal * CurrentToken.Next.Value.tokenVal;
                    CurrentToken.Value = new Token(newValue);
                    equation.Remove(CurrentToken.Previous);
                    equation.Remove(CurrentToken.Next);
                }
            }

            CurrentToken = equation.First;
            while (CurrentToken.Next != null)
            {
                while (CurrentToken.Value.tokenType != Token.type.add)
                {
                    CurrentToken = CurrentToken.Next;
                }
                newValue = CurrentToken.Previous.Value.tokenVal + CurrentToken.Next.Value.tokenVal;
                CurrentToken.Value = new Token(newValue);
                equation.Remove(CurrentToken.Previous);
                equation.Remove(CurrentToken.Next);
            }

            outputValue = CurrentToken.Value.tokenVal;
            outputText = outputValue.ToString();
            MainWindow.instance.updateOutput(outputText);
        }
    }
}
