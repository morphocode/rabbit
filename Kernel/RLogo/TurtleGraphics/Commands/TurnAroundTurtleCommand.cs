using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turtle turn right
    /// </summary>
    public class TurnAroundTurtleCommand : TurtleCommand {

        public void Execute(Turtle3d turtle)
        {
            turtle.TurnAround();
        }

    }
}

