using System;
using System.Collections.Generic;
using System.Text;

using RMA.OpenNURBS;


namespace Rabbit.Kernel.Util
{
    /**
     * Utility class that provides some common functionality related to GH to ON points converting
     * 
     */ 
    public class PresentationUtils
    {

        private PresentationUtils() { }


        public static String ListToString<T>(IList<T> list)
        {
            if (list == null) throw new InvalidOperationException("Invalid list to format!");
            int itemsCount = list.Count;
            StringBuilder sb = new StringBuilder();
            int currentItemIndex = 0;
            foreach (T element in list)
            {
                sb.Append(element);
                currentItemIndex++;
                if (currentItemIndex < itemsCount - 1)
                    sb.Append(",");
                else if (itemsCount>1 && currentItemIndex == itemsCount - 1)
                    sb.Append(" or ");
            }

            return sb.ToString();
        }

    }
}
