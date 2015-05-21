//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turtle turn left
    /// </summary>
    public class PitchUpTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d Turtle)
        {
            Turtle.PitchUp();
        }
    }
}

