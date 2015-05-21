using RMA.OpenNURBS;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    ///  Restores the last saved turtle state
    /// </summary>
    public class PopStateTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d Turtle)
        {
            Turtle.PopState();
        }
    }
}

