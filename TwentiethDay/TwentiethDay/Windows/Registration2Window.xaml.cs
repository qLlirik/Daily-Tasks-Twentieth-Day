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
using System.Windows.Shapes;

namespace TwentiethDay.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration2Window.xaml
    /// </summary>
    public partial class Registration2Window : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDataBase;

        public Registration2Window()
        {
            InitializeComponent();

            for(var i = 6; i <= 20; i++)
            {
                cbxTime.Items.Add(new TimeSpan(i,0,0));
            }
            cbxTime.SelectedIndex = 0;

            var list = db.Exempts.ToList();
            list.Insert(0, new DB.Exempts { Name = "Нет льгот" , Percent = 0});
            cbxExempt.ItemsSource = list;
            cbxExempt.DisplayMemberPath = "Name";
            cbxExempt.SelectedIndex = 0;

            dpDateStart.SelectedDate = DateTime.Now;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            HelpClasses.StaticClass.SelectDoctors = null;
            HelpClasses.StaticClass.SelectPatient = null;
            new RegistrationNewVisitWindow().Show();
            this.Close();
        }

        private void click_Oform(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxCost.Text.Length == 0)
                    throw new Exception();

                db.Visits.Add(new DB.Visits {
                    Patients = HelpClasses.StaticClass.SelectPatient,
                    Doctors = HelpClasses.StaticClass.SelectDoctors,
                    DateStart = dpDateStart.SelectedDate.Value.Date,
                    TimeStart = (TimeSpan)cbxTime.SelectedItem,
                    Cost = decimal.Parse(tbxCost.Text),
                    Exempts = (DB.Exempts)cbxExempt.SelectedItem,
                    Summa = Convert.ToDecimal(double.Parse(tbxCost.Text) - (double.Parse(tbxCost.Text) * ((DB.Exempts)cbxExempt.SelectedItem).Percent / 100)),
                    Comment = tbxComment.Text.Length == 0 ? null : tbxComment.Text
                });
                db.SaveChanges();

                MessageBox.Show("Визит оформлен!", "Perfect", MessageBoxButton.OK, MessageBoxImage.Information);
                new MainWindow().Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
