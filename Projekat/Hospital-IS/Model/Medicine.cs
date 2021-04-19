using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Medicine
    {
        public String Name { get; set; }
        public String Composition { get; set; }
        public String SideEffects { get; set; }
        public String Usage { get; set; }

        public Medicine(String name, String composition, String sideEffects, String usage)
        {
            Name = name;
            Composition = composition;
            SideEffects = sideEffects;
            Usage = usage;
        }
    }
}
