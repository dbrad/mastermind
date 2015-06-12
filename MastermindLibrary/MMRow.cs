// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MastermindLibrary
{
    // a class to maintain row data
    [Serializable]
    public class MMRow
    {
        // member data
        public enum marks { Right_Colour, Perfect };
        public Hashtable results = new Hashtable();
        public Hashtable pegs = new Hashtable();
    }
}
