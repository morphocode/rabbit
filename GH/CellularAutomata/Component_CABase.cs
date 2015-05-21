using System;
using System.Collections.Generic;
using System.Text;
using Grasshopper.Kernel;
using System.Drawing;
using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * Abstract Base component for all components under the CellularAutomata category of Rabbit
     * 
     * @author MORPHOCODE.COM
     */ 
    public abstract class Component_CABase : Component_RabbitBase
    {

        protected static String CATEGORY_1D_CA = "1D CA";
        protected static String CATEGORY_2D_CA = "2D CA";
        protected static String CATEGORY_CA_MODEL = "Cellular Automata";

        /**
         * Constructor
         */
        protected Component_CABase(String name, String nick, String description)
            : this(name, nick, description, Component_CABase.CATEGORY_CA_MODEL)//base(name, abbreviation, description, category, subcategory)
        {
        }

        /**
         * Constructor
         */
        protected Component_CABase(String name, String nick,  String description, String category)
            : base(name, nick, description, category)//base(name, abbreviation, description, category, subcategory)
        {
        }


 


    }
}
