using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.ABMS.Space.Projection
{
    /**
     * A projection defines the relationships between objects.
     * Implementations are quite different: grid, network, etc..
     * 
     */ 
    public interface IProjection<O>
    {

        /**
         * <returns> All cells contained on the lattice</returns>
         */ 
        IList<O> GetObjects();



        /**
         * <returns> The cell identified with that id</returns>
         */ 
        //O GetAgent(Object id);
    }
}
