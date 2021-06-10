using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Utils
{
    public class UtilityMethods
    {
        private static UtilityMethods instance = null;
        public static UtilityMethods Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UtilityMethods();
                }
                return instance;
            }
        }
        public DateTime RoundUp(DateTime date, TimeSpan time)
        {
            return new DateTime((date.Ticks + time.Ticks - 1) / time.Ticks * time.Ticks, date.Kind);
        }
    }
}
