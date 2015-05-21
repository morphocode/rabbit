using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.ABMS.Space.Projection
{
    /**
     * 
     * Interface for classes that wish to add objects to a space. An adder is used by setting it on a space. The space then will use whatever strategy is defined by the Adder to add objects to itself. For example, a random grid adder may add objects at random locations on a grid. 
     * 
     * 
     */
    public interface IAdder<D, O>
    {

        void Add(D destination, O o);
    }
}
