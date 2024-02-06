using AllProject.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для AddClientsForm.xaml
    /// </summary>
    public partial class NewClientWindow : Window
    {
        public Client client {  get; set; }
        public NewClientWindow()
        {
            InitializeComponent();
        }

        public void ClearInputs()
        {
             adrs.Text = ""; phone.Text = ""; fullName.Text = "";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumberText = phone.Text;
            decimal phoneNumber;

            if (!decimal.TryParse(phoneNumberText, out phoneNumber))
            {
                MessageBox.Show("В номере телефона не должно быть букв");
                return;
            }
            
            if (fullName.Text == "" )
            {
                MessageBox.Show("Пожалуйста введите ФИО");
                return;
            }
            App.db.Clients.Add(new Client { Address = adrs.Text, FullName = fullName.Text, Phone = phone.Text });

            App.db.SaveChanges();

            ClearInputs();
        }


        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }
    }
}
