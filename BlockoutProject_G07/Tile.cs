using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockoutProject_G07
{
    public class Tile
    {
        //Defining Tile state (ON or OFF)
        public bool State {get; private set;}
        /// <summary>
        /// Tile's constructor
        /// </summary>
        /// <param name="state"> Tile's state (ON = true or OFF = false) </param>
        public Tile(bool state)
        {
            State = state;
        }
        /// <summary>
        /// Returns the Tile's state
        /// </summary>
        /// <returns> Bool, current state </returns>
        public bool GetState()
        {
            return State;
        }
        /// <summary>
        /// Toggles Tile's state (ON to OFF or OFF to ON)
        /// </summary>
        public void ToggleState()
        {
            State = !State;
        }
    }
}