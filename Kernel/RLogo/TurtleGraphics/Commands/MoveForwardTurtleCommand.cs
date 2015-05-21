//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turle move one step forward
    /// </summary>
    public class MoveForwardTurtleCommand : TurtleCommand
    {

        public virtual void Execute(Turtle3d turtle)
        {
            turtle.MoveForward();
        }
    }
}

