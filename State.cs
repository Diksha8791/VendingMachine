using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class State
    {
        public enum StateEnum
        {
            Ready,
            CollectingCash,
            ReturningCash,
            DispenseProduct
        }

        public enum ActionEnum
        {
            SelectProduct,
            Cancel,
            AddCash
        }

        private StateEnum currentState;
        public State()
        {
            this.currentState = StateEnum.Ready;
        }

        public StateEnum GetCurrentState()
        {
            return currentState;
        }

        public void SetCurrentState(StateEnum state)
        {
            currentState = state;
        }
    }
}
