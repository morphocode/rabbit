//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turtle pitch down. Rotation along the Y Axis
    /// </summary>
    public class PitchDownTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d Turtle)
        {
            Turtle.PitchDown();
        }
    }
}

