using System;
using System.Collections.Generic;
using System.Data;
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
using AllProject;
using AllProject.Model;

namespace AllProject.ui
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ProductWindow : UserControl
    {
        public NewProductWindow newProductWindow { get; set; }
        public Product product {  get; set; }
        public ProductWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            dgProduct.ItemsSource = App.db.Products.ToList();
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            newProductWindow = new NewProductWindow();

            newProductWindow.ShowDialog();

            LoadDataGrid();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                LoadDataGrid();

            }
            var filterlist = App.db.Products.Where(x => x.Name.StartsWith(textBox.Text));
            dgProduct.ItemsSource = filterlist.ToList();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            printDlg.PrintVisual(dgProduct, "Grid Printing.");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            App.db.SaveChanges();
            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (Product)dgProduct.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            App.db.Products.Remove(item);
            App.db.SaveChanges();
            LoadDataGrid();
        }
    }



}
