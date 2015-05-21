using RMA.OpenNURBS;
using System;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that makes the turle reverse it's heading
    /// </summary>
    public class ReverseHeadingTurtleCommand : TurtleCommand
    {
        public void Execute(Turtle3d Turtle)
        {
            //Turtle.GetTurtleState().GetHeading().Reverse();
            throw new NotImplementedException();
        }
    }
}

