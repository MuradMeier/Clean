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
    public partial class OrderCompositionWindow : UserControl
    {
        public OrderComposition orderComposition { get; set; }
        
        public OrderCompositionWindow()
        {
            InitializeComponent();
            LoadDataGrid();


        }

        public void LoadDataGrid()
        {
            dgOrderComposition.ItemsSource = App.db.OrderCompositions.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (OrderComposition)dgOrderComposition.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            try
            {
                App.db.OrderCompositions.Remove(item);
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

            printDlg.PrintVisual(dgOrderComposition, "Grid Printing.");
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                LoadDataGrid();
            }
            var filterlist = App.db.OrderCompositions.Where(x => x.Order.Client.FullName.ToString().StartsWith(textBox.Text));
            var filtlist1 = App.db.OrderCompositions.Where(x => x.Order.ID.ToString().StartsWith(textBox.Text));
            filterlist.ToList().AddRange(filtlist1.ToList());
            dgOrderComposition.ItemsSource = filterlist.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();
            LoadDataGrid();
        }
    }
}
