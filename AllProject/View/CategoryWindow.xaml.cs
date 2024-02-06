using AllProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace AllProject.ui
{
    /// <summary>
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class CategoryWindow : UserControl
    {
        public NewCategoryWindow newCategoryWindow {  get; set; }
        public Category category { get; set; }
        public CategoryWindow()
        {
            InitializeComponent();
            LoadDataGrid();


        }

        public void LoadDataGrid()
        {
            dgCategory.ItemsSource = App.db.Categories.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            newCategoryWindow = new NewCategoryWindow();

            newCategoryWindow.ShowDialog();

            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (Category)dgCategory.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            try
            {
                App.db.Categories.Remove(item);
                App.db.SaveChanges();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            printDlg.PrintVisual(dgCategory, "Grid Printing.");
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                LoadDataGrid();
            }
            var filterlist = App.db.Categories.Where(x => x.Name.StartsWith(textBox.Text));
            dgCategory.ItemsSource = filterlist.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();
            LoadDataGrid();
        }
    }
}
