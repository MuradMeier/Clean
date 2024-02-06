using AllProject.Model;
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

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для AddClientsForm.xaml
    /// </summary>
    public partial class NewServiceWindow : Window
    {
        public Service service { get; set; }
        public NewServiceWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            App.db.Services.Add(new Service { ServiceCoefficient = Convert.ToUInt16(serv_Coeff.Text), ServiceName = servName.Text });

            App.db.SaveChanges();

            ClearInputs();
        }

        public void ClearInputs()
        {
            servName.Text = "";
            serv_Coeff.Text = "";
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }
    }

 
    
}
