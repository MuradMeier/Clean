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
using AllProject.Model;

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        public Product product {  get; set; }
        public NewProductWindow()
        {
            InitializeComponent();
            foreach (var item in App.db.Categories.ToList())
            {
                cat.Items.Add(item.Name);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var categ = App.db.Categories.FirstOrDefault(x => x.Name == cat.Text);
            App.db.Products.Add(new Product { Price = Convert.ToUInt16(price.Text), Name = name.Text, Category = categ, Duration = Convert.ToInt16(dur.Text) });
            App.db.SaveChanges();
            ClearInputs();
        }

        public void ClearInputs()
        {
            price.Text = "";
            name.Text = "";
            cat.Text = "";
            dur.Text = "";
        }


        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }
    }



}