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
    /// Логика взаимодействия для RegistrationNewVisitWindow.xaml
    /// </summary>
    public partial class RegistrationNewVisitWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDataBase;

        public RegistrationNewVisitWindow()
        {
            InitializeComponent();

            cbxDoctorType.ItemsSource = db.DoctorTypes.ToList();
            cbxDoctorType.DisplayMemberPath = "Name";
            cbxDoctorType.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void select_cbxDoctorType(object sender, SelectionChangedEventArgs e)
        {
            cbxDoctor.ItemsSource = db.Doctors.Where(w => w.TypeID == ((DB.DoctorTypes)cbxDoctorType.SelectedItem).ID).ToList();
            cbxDoctor.DisplayMemberPath = "FullName";
            cbxDoctor.SelectedIndex = 0;
        }

        private void click_Dalee(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.Patients pat = null;
                if (tc.SelectedIndex == 0)
                {
                    if ((tbxFIO.Text.Length == 0) || (tbxNumber.Text.Length == 0) || (tbxAddress.Text.Length == 0) || (tbxDistrict.Text.Length == 0) || (tbxPolicyNumber.Text.Length == 0))
                        throw new Exception();

                    HelpClasses.StaticClass.SelectPatient = new DB.Patients {
                        FIO = tbxFIO.Text,
                        Number = tbxNumber.Text,
                        Address = tbxAddress.Text,
                        District = tbxDistrict.Text,
                        PolicyNumber = tbxPolicyNumber.Text,
                        Year = int.Parse(tbxYear.Text),
                        Sign = chbxSign.IsChecked == true ? true : false,
                        Department = tbxDepartment.Text.Length == 0 ? null : tbxDepartment.Text
                    };
                }
                else
                {
                    HelpClasses.StaticClass.SelectPatient = (DB.Patients)cbxPatient.SelectedItem;
                }
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            if (cbxDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите доктора!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            HelpClasses.StaticClass.SelectDoctors = (DB.Doctors)cbxDoctor.SelectedItem;

            new Registration2Window().Show();
            this.Close();
        }

        private void select_tc(object sender, SelectionChangedEventArgs e)
        {
            if (tc.SelectedIndex == 1)
            {
                cbxPatient.ItemsSource = db.Patients.ToList();
                cbxPatient.DisplayMemberPath = "FIO";
                cbxPatient.SelectedIndex = 0;
            }
            else
            {
                cbxPatient.ItemsSource = null;
            }
            
        }

        private void click_chbxSign(object sender, RoutedEventArgs e)
        {
            if (chbxSign.IsChecked == true)
                sp.Visibility = Visibility.Visible;
            else
                sp.Visibility = Visibility.Hidden;
        }
    }
}
