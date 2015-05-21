using System;
using System.Collections.Generic;
using System.Text;
using Grasshopper.Kernel;
using System.Drawing;
using Rabbit.Properties;


namespace Rabbit.GH.LSystems
{
    /**
     * Abstract Base component for all components under the CellularAutomata category of Rabbit
     * 
     * @author MORPHOCODE.COM
     */ 
    public abstract class Component_LSBase : Component_RabbitBase
    {

        /**
         * Constructor
         */
        public Component_LSBase(String name, String nick, String description)
            : base(name, nick, description, "L-Systems")//base(name, abbreviation, description, category, subcategory)
        {
        }


 


    }
}
