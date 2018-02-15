using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TwentiethDay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void click_Registration(object sender, RoutedEventArgs e)
        {
            new Windows.RegistrationNewVisitWindow().Show();
            this.Close();
        }

        private void click_ListVisits(object sender, RoutedEventArgs e)
        {
            new Windows.ListVisitsWindow().Show();
            this.Close();
        }
    }
}
