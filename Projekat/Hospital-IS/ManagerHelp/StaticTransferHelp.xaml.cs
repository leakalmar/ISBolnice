using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerHelp
{
    /// <summary>
    /// Interaction logic for StaticTransferHelp.xaml
    /// </summary>
    public partial class StaticTransferHelp : Window
    {
        public StaticTransferHelp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
