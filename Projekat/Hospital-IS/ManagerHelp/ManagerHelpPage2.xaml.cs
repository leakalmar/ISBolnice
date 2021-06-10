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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerHelp
{
    /// <summary>
    /// Interaction logic for ManagerHelpPage2.xaml
    /// </summary>
    public partial class ManagerHelpPage2 : Page
    {
        public ManagerHelpPage2()
        {
            InitializeComponent();
            this.DataContext = HelpViewModel.Instance;
        }
    }
}
