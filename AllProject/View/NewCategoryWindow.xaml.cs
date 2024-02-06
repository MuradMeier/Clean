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
    public partial class NewCategoryWindow : Window
    {
        public Category category { get; set; }
        public NewCategoryWindow()
        {
            InitializeComponent();
        }

        public void ClearInputs()
        {
            name.Text = "";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
            {
                MessageBox.Show("Пожалуйста введите наименование");
                return;
            }
            App.db.Categories.Add(new Category { Name = name.Text });

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
