using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.TurtleGraphics;
using Rabbit.Kernel.TurtleGraphics.Commands;

namespace Rabbit.Kernel.RLogo.Parser
{
    public class RLogoParser
    {

        protected TurtleCommandFactory turtleCommandFactory;

        /**
         * <returns>A list of commands derived from the specified String. Each character in the string is mapped to a command </returns>
         * 
         */
        public List<TurtleCommand> Parse(String source)
        {
            List<TurtleCommand> TurtleCommands = new List<TurtleCommand>();
            for (int i = 0; i < source.Length; i++)
            {
                Char nextSymbol = source[i];
                TurtleCommands.Add(turtleCommandFactory.GetTurtleCommand(nextSymbol));
            }
            return TurtleCommands;
        }

    }
}
