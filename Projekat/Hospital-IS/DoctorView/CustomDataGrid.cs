using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public class CustomDataGrid : DataGrid
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Return && e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                return;
            }

            base.OnKeyDown(e);
        }
    }
}
