using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.TurtleGraphics;
using Rabbit.Kernel.TurtleGraphics.Commands;

namespace Rabbit.Kernel.RLogo.Parser
{
    public class StandardRLogoParser : RLogoParser
    {

        public StandardRLogoParser()
        {
            base.turtleCommandFactory = new TurtleCommandFactory();
            turtleCommandFactory.MapTurtleCommand('F', new MoveForwardAndDrawTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('f', new MoveForwardTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('+', new TurnLeftTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('-', new TurnRightTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('/', new RollRightTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('\\', new RollLeftTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('^', new PitchUpTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('&', new PitchDownTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('|', new TurnAroundTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('[', new PushStateTurtleCommand());
            turtleCommandFactory.MapTurtleCommand(']', new PopStateTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('"', new ScaleStepLengthTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('!', new ScaleThicknessTurtleCommand());
            turtleCommandFactory.MapTurtleCommand('J', new CopyGeometryTurtleCommand());
        }

    }
}
