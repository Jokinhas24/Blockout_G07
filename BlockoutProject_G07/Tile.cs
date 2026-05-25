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
        /// Tile's constructor setting default state
        /// </summary>
        public Tile()
        {
            // Default state (OFF = false)
            State = false;
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