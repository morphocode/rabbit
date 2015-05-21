using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    //<summary>
    // A mapping that maps a Symbol(Character) <-> Turtle Command
    // 
    // Different symbols could have different commands associated, 
    // thus each symbol have a different meaning for the turtle.
    //</summary>
    public class TurtleCommandFactory
    {

        protected Dictionary<Char, TurtleCommand> CommandsMap;

        /**
         * Constructor.
         */ 
        public TurtleCommandFactory() 
        {
            CommandsMap = new Dictionary<char, TurtleCommand>();
        }

        public void MapTurtleCommand(Char Char, TurtleCommand TurtleCommand)
        {
            CommandsMap.Add(Char, TurtleCommand);
        }

        /**
         * Returns the Command mapped to that Char
         * Null if there is no associated Command
         * 
         */ 
        public TurtleCommand GetTurtleCommand(Char Char)
        {
            if(CommandsMap.ContainsKey(Char))
                return CommandsMap[Char];
            else
                return EmptyTurtleCommand.INSTANCE;//do nothing, if a Char<->Command mapping is missing
        }

    }
}
