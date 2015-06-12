// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace MastermindLibrary
{
    // a class to maintain game state information
    [Serializable]
    public class StateInfo
    {
        // member data
        [SerializableAttribute]
        public enum StatusType { Playing, Won, Lost };
        public StatusType status = StatusType.Playing;
        public Hashtable guesses;
        public string playerTurn = "";
    }
}
