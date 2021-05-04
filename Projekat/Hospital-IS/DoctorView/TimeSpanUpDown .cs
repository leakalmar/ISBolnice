﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorView
{
    public class TimeSpanUpDown : Xceed.Wpf.Toolkit.TimeSpanUpDown
    {
        protected override void OnIncrement()
        {
            if (Value.HasValue)
            {
                UpdateTimeSpan(15);
            }
            else
            {
                Value = DefaultValue ?? TimeSpan.Zero;
            }
        }

        protected override void OnDecrement()
        {
            if (Value.HasValue)
            {
                UpdateTimeSpan(-15);
            }
            else
            {
                Value = DefaultValue ?? TimeSpan.Zero;
            }
        }

        private void UpdateTimeSpan(int value)
        {
            TimeSpan timeSpan = (TimeSpan)Value;
            timeSpan = timeSpan.Add(new TimeSpan(0, value, 0));

            if (IsLowerThan(timeSpan, Minimum))
            {
                Value = Minimum;
                return;
            }

            if (IsGreaterThan(timeSpan, Maximum))
            {
                Value = Maximum;
                return;
            }

            Value = timeSpan;
        }

    }
}
