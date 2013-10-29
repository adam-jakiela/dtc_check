using System;
using System.Collections.Generic;
using System.Text;

namespace ICSNeoCSharp
{
    #region Delegates
    public delegate void MyStateMachineEvent(object source);
    #endregion

    class stateMachine
    {
        #region Variables
		private bool ignition = false;
        private bool timer2 = false;
        public event MyStateMachineEvent engineRun;
        public event MyStateMachineEvent engineOff;

        #endregion

        #region Constructor
        public stateMachine()
        {
            
        }
        #endregion

        #region Properties
        /// <summary>
        /// Accessor Ignition state.
        /// </summary>
        public bool Ignition
        {
            get
            {
                return ignition;
            }

            set
            {
                ignition = value;
                if (ignition == true)
                    engineRun.Invoke(this);
                else
                    engineOff.Invoke(this);
            }
        }
        public bool Timer2
        {
            get
            {
                return timer2;
            }
            set
            {
                timer2 = value;
            }
        }
        #endregion
    }
}
