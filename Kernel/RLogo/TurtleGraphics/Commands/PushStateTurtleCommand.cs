//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    ///  Push the state of the turtle in the states stack
    /// </summary>
    public class PushStateTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d Turtle)
        {
            Turtle.PushState();
        }
    }
}

