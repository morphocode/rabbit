//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turle move one step forward and draw a line while moving
    /// </summary>
    public class MoveForwardAndDrawTurtleCommand : MoveForwardTurtleCommand
    {
        public override void Execute(Turtle3d turtle)
        {
            turtle.MoveForwardAndDraw();
        }
    }
}

