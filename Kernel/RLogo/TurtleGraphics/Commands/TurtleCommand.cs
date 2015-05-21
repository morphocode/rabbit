using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A TurtleCommand is an abstraction of a command that drives the Turtle.
    /// Every operation that a Trutle does is implemented as a Command: MoveForwardCommand, TurnLeft Command, etc.
    /// This gives the flexibility of implementing new commands in a generic way.
    /// </summary>
    public interface TurtleCommand 
    {
        ///<summary>
        /// Does the job. Drives the turtle in a specific way
        ///</summary>
        void Execute(Turtle3d Turtle);
    }
}

