using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{
    public class Token
    {
        //TODO: extends with different types of Tokens? http://web.duke.edu/~mky/slogo/


        private String tokenString;



        public Token(String tokenString) 
        {
            this.tokenString = tokenString;
        }

        public String ToString()
        {
            return tokenString;
        }


        public override bool Equals(Object obj)
        {
            if (obj.GetType() != this.GetType()) return false;//TODO: this could break inheritance
            Token aToken = (Token)obj;

            return tokenString.Equals(aToken.ToString());
        }

        public override int GetHashCode()
        {
            return tokenString.GetHashCode();
        }


        //public enum TokenType { IDENTIFIER, NUMBER,  };
    }
}
