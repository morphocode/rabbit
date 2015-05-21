using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.Automata.FSM;

namespace Rabbit.Kernel.CellularAutomata.Impl.Greenberg_Hastings
{
    public class ExcitableCell:Cell
    {

        //tresholds
        private int excitedNeighborsTreshold = 3;


        //Excitable Cell states
        private CellState restingState;
        private IList<CellState> refractoryStates;
        private IList<CellState> excitedStates;


        private static Random random = new Random();

        private static double ExcitedProbability = 1.5 / 100.0;

        private int currentRefractoryIndex;
        private int currentExcitedIndex;

        public ExcitableCell(int id, CellState restingState, IList<CellState> refractoryStates, IList<CellState> excitedStates, int excitedNeighborsTreshold) //, int state)//, IList<CellState> finiteStates, List<ExcitedTransition> transitions)
            : base(id, null, null)
        {
            this.restingState = restingState;
            this.refractoryStates = refractoryStates;
            this.excitedStates = excitedStates;
            this.excitedNeighborsTreshold = excitedNeighborsTreshold;

            //init the finite states of the cell
            finiteStates = new List<CellState>();
            //set all possible states
            finiteStates.Add(restingState);
            foreach(CellState refractoryState in refractoryStates)
                finiteStates.Add(refractoryState);
            foreach (CellState excitedState in excitedStates)
                finiteStates.Add(excitedState);

            cellState = GetDefaultState();

        }

        /**
         * Resting cells could become active by some probability
         */ 
        private CellState GetNoiseState()
        {
            double probability = random.NextDouble();
            if (probability < 0.01)
                return excitedStates[0];
            else
                return cellState;
        }

        public override ICell Clone()
        {
            return new ExcitableCell(-1, restingState, refractoryStates, excitedStates, excitedNeighborsTreshold);
        }

        public override CellState GetDefaultState()
        {
            return restingState;
        }

        public Boolean IsExcited()
        {
            return excitedStates.Contains(cellState);
        }
        public Boolean IsRefractory()
        {
            return refractoryStates.Contains(cellState);
        }
        public Boolean IsResting()
        {
            return cellState.Equals(restingState);
        }

        public override CellState UpdateState()
        {            
           
            //is excited:
            if (IsExcited()) {
                currentExcitedIndex++;
                if (currentExcitedIndex < excitedStates.Count)
                    return excitedStates[currentExcitedIndex];
                else //no more excited states -> go refractory
                {                    
                    currentExcitedIndex = 0;//reset the index
                    return refractoryStates[currentRefractoryIndex];
                }
            }
            //Refractory
            else if (IsRefractory()) {
                currentRefractoryIndex++;
                if (currentRefractoryIndex < refractoryStates.Count)
                    return refractoryStates[currentRefractoryIndex];
                else
                {
                    currentRefractoryIndex = 0;//reset the index
                    return restingState;
                }
            }
            //Resting
            else if (IsResting())
            {
                int excitedNeighborsCount = 0;
                //check for excited neighbors:
                foreach (ICell neighbor in GetNeighbors())
                {
                    CellState neighborState = neighbor.GetState();
                    if (excitedStates.Contains(neighborState))//check if the neighbor is excited
                        excitedNeighborsCount++;
                }
                if (excitedNeighborsCount >= excitedNeighborsTreshold)
                    return excitedStates[0]; //go excited
            }
            //no change in the status
            return cellState;

            //return GetNoiseState();
            //return GetRandomState();
            //throw new InvalidOperationException("Invalid stated of an excitable cell!: "+cellState+":"+restingState+":"+cellState.Equals(restingState));
        }


    }
}

