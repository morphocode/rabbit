using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.CellularAutomata.Cells
{
    /**
     * Wraps a cell and attach an object to that cell.
     * 
     * NOTE: CellWrapper does not do the job because ICells are casted to specific types 
     * FIXME: use a custom TypeConvertor
     * so that (LivingCell) CellWrapper acutally calls (LivingCell)cellWrapper.getCell()
     * http://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
     */
    public class CellWrapper<O>:ICell
    {

        private ICell wrappedCell;
        //the attached object
        private O obj;

        public CellWrapper(ICell wrappedCell, O obj)
        {
            this.wrappedCell = wrappedCell;
            this.obj = obj;
        }


        //ICell delegates --------------------------------------------
        public Object GetId()
        {
            return wrappedCell.GetId();
        }

        public void SetId(Object id)
        {
            wrappedCell.SetId(id);
        }

        public Object GetAttachedObject()
        {
            return wrappedCell.GetAttachedObject();
        }

        public void SetAttachedObject(Object obj)
        {
            wrappedCell.SetAttachedObject(obj);
        }

        public CellState GetState()
        {
            return wrappedCell.GetState();
        }

        public IList<CellState> GetFiniteStates()
        {
            return wrappedCell.GetFiniteStates();
        }

        public void SetState(CellState cellState)
        {
            wrappedCell.SetState(cellState);
        }

        public CellState GetDefaultState()
        {
            return wrappedCell.GetDefaultState();
        }

        public IList<CellularRule> GetRules()
        {
            return wrappedCell.GetRules();
        }

        public void SetTransitionRules(List<CellularRule> transitionRules)
        {
            wrappedCell.SetTransitionRules(transitionRules);
        }

        public IList<ICell> GetNeighbors()
        {
            return wrappedCell.GetNeighbors();
        }

        public void SetNeighbors(IList<ICell> neighbors)
        {
            wrappedCell.SetNeighbors(neighbors);
        }

        public ICell Clone()
        {
            return wrappedCell.Clone();
        }

        public CellState UpdateState()
        {
            return wrappedCell.UpdateState();
        }

        //CellWrapper -----------------------------------------------------
        public ICell GetCell()
        {
            return wrappedCell;
        }

        public O GetObject()
        {
            return obj;
        }

    }
}
