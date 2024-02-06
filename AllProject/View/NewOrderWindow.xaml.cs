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
using AllProject;
using AllProject.Model;

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public Order order {  get; set; }
        public NewOrderWindow()
        {
            InitializeComponent();

            foreach (var item in App.db.Clients.ToList())
            {
                fullName.Items.Add(item.FullName);
            }

            foreach (var item in App.db.Categories.ToList())
            {
                categoryName.Items.Add(item.Name);
            }
            int maxId = 0;
            foreach (var item in App.db.Orders.ToList())
            {
                if (maxId < item.ID)
                    maxId = item.ID;
            }
            id.Text = (maxId + 1).ToString();
            productName.Visibility = Visibility.Collapsed;
            serviceName.Visibility = Visibility.Collapsed;
            dp1.SelectedDate = DateTime.Now;
            ordPrice.Visibility = Visibility.Visible;
            ordPrice.Text = "";
        }


        List<OrderComposition> orderCompositions = new List<OrderComposition>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = App.db.Clients.FirstOrDefault(x => x.FullName == fullName.Text);
            var ord = new Order { Client = client, CompilationDate = dp1.SelectedDate.Value, TotalDuration = Convert.ToInt32(totDur.Text), OrderPrice = Convert.ToInt32(ordPrice.Text) };
            foreach (var item in orderCompositions)
            {
                item.Order = ord;
                App.db.OrderCompositions.Add(item);
            }
            App.db.SaveChanges();
            ClearInputs();
        }

        public void ClearInputs()
        {
            fullName.Text = "";
            categoryName.Text = "";
            productName.Text = "";
            ordPrice.Text = "";
            Dur.Text = "";
            serviceName.Text = "";
            quant.Text = "";
            prodPrice.Text = "";
            dp1.Text = "";
            dgOrdComp.ItemsSource = null;
            
        } 

        private void AddinOrderComposition_Click(object sender, RoutedEventArgs e)
        {
            var product = App.db.Products.FirstOrDefault(x => x.Name == productName.Text);
            var prType = App.db.Services.FirstOrDefault(x => x.ServiceName == serviceName.Text);
            orderCompositions.Add(new OrderComposition { Product = product, Quantity = Convert.ToInt32(quant.Text), Service=prType });
            dgOrdComp.ItemsSource = null;
            if (ordPrice.Text == "")
            {
                ordPrice.Text = (Convert.ToDouble(prodPrice.Text) * Convert.ToDouble(quant.Text)).ToString();
            }
            else
            {
                ordPrice.Text = (Convert.ToDouble(ordPrice.Text) + Convert.ToDouble(prodPrice.Text) * Convert.ToDouble(quant.Text)).ToString();
            }
            dgOrdComp.ItemsSource = orderCompositions.ToList();
            if (Readiness_Date.Text == "" || Readiness_Date.SelectedDate < DateTime.Now.AddDays(product.Duration))
            {
                totDur.Text = product.Duration.ToString();
                Readiness_Date.SelectedDate = DateTime.Now.AddDays(product.Duration);
            }
        }

        private void categoryName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryName.Text == "")
            {
                categoryName.Text = categoryName.Items.GetItemAt(0).ToString();
            }
            else
            {
                categoryName.Text = categoryName.SelectedItem.ToString();
            }
            productName.Text = "";
            productName.Items.Clear();
            foreach (var item in App.db.Products.Where(x => x.Category.Name == categoryName.Text).ToList())
            {
                productName.Items.Add(item.Name);
            }
            productName.Visibility = Visibility.Visible;
        }

        private void productName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productName.Text == "")
            {
                productName.Text = productName.Items.GetItemAt(0).ToString();
            }
            else
            {
                productName.Text = productName.SelectedItem.ToString();
            }
            var prod = App.db.Products.FirstOrDefault(x => x.Name == productName.Text);
            if (serviceName.Text == "")
            {
                prodPrice.Text = prod.Price.ToString();
            }
            else
            {
                var ser = App.db.Services.FirstOrDefault(x => x.ServiceName == serviceName.Text);
                prodPrice.Text = (prod.Price * ser.ServiceCoefficient).ToString();
            }
            Dur.Text = prod.Duration.ToString();
            serviceName.Visibility = Visibility.Visible;
            foreach (var item in App.db.Services.ToList())
            {
                serviceName.Items.Add(item.ServiceName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (OrderComposition)dgOrdComp.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            try
            {
                orderCompositions.Remove(item);
                dgOrdComp.ItemsSource = orderCompositions;

            }
            catch (Exception ex)
            {
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void serviceName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serviceName.Text == "")
            {
                serviceName.Text = serviceName.Items[0].ToString();
            }
            else
            {
                serviceName.Text = serviceName.SelectedItem.ToString();
            }
            var ser = App.db.Services.FirstOrDefault(x => x.ServiceName == serviceName.Text);
            prodPrice.Text = (Convert.ToDouble(prodPrice.Text) * ser.ServiceCoefficient).ToString();
        }

        private void fullName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }



}
