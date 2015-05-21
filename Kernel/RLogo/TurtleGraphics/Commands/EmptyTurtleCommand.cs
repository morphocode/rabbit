using RMA.OpenNURBS;

using Rabbit.Kernel.TurtleGraphics;

namespace Rabbit.Kernel.TurtleGraphics.Commands
{
    ///<summary>
    /// A Turtle commands that does nothing. Keeps the consistency of the API. Used in a collection of commands instead of Null
    /// </summary>
    public class EmptyTurtleCommand : TurtleCommand
    {
        public static EmptyTurtleCommand INSTANCE = new EmptyTurtleCommand();

        //Could not be initialized from outside. Use the Single Instance
        private EmptyTurtleCommand() { }

        public void Execute(Turtle3d Turtle)
        {
        }
    }
}

