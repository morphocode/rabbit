using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.RLogo.Parser;
using Rabbit.Kernel.TurtleGraphics;

using RMA.OpenNURBS;


namespace Rabbit.Kernel.RLogo
{
    /**
     * The TurtleInterpreter interprets a string(source code) written in a LOGO dialect(Turtle Language).
     *
     * The interpreter uses a Parser to parse the source into some kind of data structure(Parse tree of instructions)
     * 
     * The Interpreter executes these instructions.
     * 
     * 
     * 
     */
    public class RLogoInterpreter
    {

        private RLogoParser _turtleParser;

        private Turtle3d _turtle;

        public RLogoInterpreter(Turtle3d turtle)
        {
            _turtleParser = new StandardRLogoParser();
            _turtle = turtle;

        }

        public void Interpret(String source)
        {
            //this should be done in the Turtle command execute
            //the interpreter should init the context of the command
            _turtle.Do(_turtleParser.Parse(source));//TODO: check for arrayOfOfBounds exception here!
        }
        


    }
}
