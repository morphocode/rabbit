using Rhino.Geometry;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turle move one step forward and draw a line while moving
    /// </summary>
    public class ScaleThicknessTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d turtle)
        {
            //scales the thickness using the default thickness scale
            turtle.ScaleThickness();
        }
    }
}

