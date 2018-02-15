using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ListVisitsWindow.xaml
    /// </summary>
    public partial class ListVisitsWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDataBase;

        public ListVisitsWindow()
        {
            InitializeComponent();

            var dt = DateTime.Now;
            lv.ItemsSource = db.Visits.Where(w => w.DateStart >= dt).ToList();

            cbxFormat.ItemsSource = new List<string> { "CSV", "XML", "XLS", "XLSX"};
            cbxFormat.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void click_Delete(object sender, RoutedEventArgs e)
        {
            db.Visits.Remove((DB.Visits)((Button)sender).DataContext);
            db.SaveChanges();
            var dt = DateTime.Now.AddDays(-1);
            lv.ItemsSource = db.Visits.Where(w=>w.DateStart >= dt).ToList();

            MessageBox.Show("Приём отменён!","Perfect",MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void click(object sender, RoutedEventArgs e)
        {
            pp.IsOpen = true;
            var btn = (Button)sender;
            txbName.Text = btn.Content + ": ";
            btnName.Content = btn.Content;
            btnName.Tag = btn.Tag;
        }

        private void WriteListCSV(List<DB.Visits> list, string path, string filename)
        {
            List<string> strList = new List<string>();
            foreach(var i in list)
            {
                strList.Add(i.Patients.FIO + ";" + i.Doctors.FullName + ";" + i.DateStart.Date + ";" + i.TimeStart + ";" + i.Cost + ";" + i.Exempts.Name + ";" + i.Comment);
            }
            File.WriteAllLines(path + @"\" + filename + ".csv", strList.ToArray());
        }

        private void WriteListXML(List<DB.Visits> list, string path, string filename)
        {
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(path + @"\" + filename + ".xml");
            writer.WriteStartDocument();
            writer.WriteStartElement("Visits");

            foreach(var i in list)
            {
                writer.WriteStartElement("Visit");
                writer.WriteElementString("Patient", i.Patients.FIO);
                writer.WriteElementString("Doctor", i.Doctors.FullName);
                writer.WriteElementString("DateStart", i.DateStart.Date + "");
                writer.WriteElementString("TimeStart", i.TimeStart + "");
                writer.WriteElementString("Cost", i.Cost + "");
                writer.WriteElementString("Exempt", i.Exempts.Name);
                writer.WriteElementString("Summa", i.Summa + "");
                writer.WriteElementString("Comment", i.Comment);
                writer.WriteEndElement();
                writer.Flush();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void click_Sek(object sender, RoutedEventArgs e)
        {
            pp.IsOpen = false;
            if (((Button)sender).Tag + "" == "0")
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                fbd.Description = "Выберите папку для сохранения экспорта.";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    pp.IsOpen = true;
                    return;
                }

                List<DB.Visits> Visits = (List<DB.Visits>)lv.ItemsSource;

                switch (cbxFormat.SelectedIndex)
                {
                    case 0:
                        {
                            WriteListCSV(Visits, fbd.SelectedPath , "Export");
                            break;
                        }
                    case 1:
                        {
                            WriteListXML(Visits, fbd.SelectedPath, "Export");
                            break;
                        }
                }

                MessageBox.Show("Экспорт совершён!","Perfect",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void click_ClosePP(object sender, RoutedEventArgs e)
        {
            pp.IsOpen = false;
        }
    }
}
