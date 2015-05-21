using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Geometry.Util
{
    /**
     * Utility class that provides some common features related to surface manipulation
     * 
     */ 
    public class SurfaceUtils
    {

        private SurfaceUtils() { }

        /*
        public static SurfaceFromPointGrid(List<On3dPoint> points, int onUPointsCount, ) {
        {
            int num;
            List<GH_Point> list = new List<GH_Point>();
            GH_Boolean destination = null;
            if (iData.GetData<int>(1, out num) && iData.GetData<GH_Boolean>(2, out destination))
            {
                if (num < 2)
                {
                    base.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid UCount value. Count must be larger than 1.");
                }
                else if (iData.GetDataList<GH_Point>(0, list))
                {
                    int num2 = Convert.ToInt32((double) (((double) list.Count) / ((double) num)));
                    if ((num2 * num) != list.Count)
                    {
                        base.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "The UCount value is not valid for this amount of points.");
                    }
                    else
                    {
                        OnNurbsSurface surface;
                        int[] numArray2 = new int[] { num, num2 };
                        int[] degree = new int[] { Math.Min(3, num - 1), Math.Min(3, num2 - 1) };
                        bool[] flagArray = new bool[] { false, false };
                        On3dPointArray array = new On3dPointArray(list.Count);
                        int num4 = list.Count - 1;
                        for (int i = 0; i <= num4; i++)
                        {
                            if (!list[i].IsValid)
                            {
                                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "A point in the grid failed to load. Fitting operation aborted");
                                return;
                            }
                            array.Append(list[i].Point);
                        }
                        if (destination.Value)
                        {
                            surface = RhUtil.RhinoSrfPtGrid(numArray2, degree, flagArray, array);
                        }
                        else
                        {
                            surface = RhUtil.RhinoSrfControlPtGrid(numArray2, degree, array);
                        }
                        if (surface == null)
                        {
                            base.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Surface could not be fitted");
                        }
                        else
                        {
                            iData.SetData(0, new GH_Surface(surface));
                        }
                    }
                }
            }
        }


        */
    }
}
