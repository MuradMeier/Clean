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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllProject.Model;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace AllProject.ui
{
    /// <summary>
    /// Логика взаимодействия для OrderForm.xaml
    /// </summary>
    public partial class OrderWindow : UserControl
    {
        public Order order { get; set; }
        public NewOrderWindow newOrderWindow { get; set; }
        public OrderWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        public async void LoadDataGrid()
        {
            List<DateTime> readn_dates = new List<DateTime>();
            foreach (var i in App.db.Orders)
            {
                readn_dates.Add(i.CompilationDate.AddDays(i.TotalDuration));
            }
            foreach (var i in readn_dates)
            { 
                foreach (var ord in App.db.Orders.Where(x => i < DateTime.Now))
                {
                    ord.Readiness = false;
                }
            };
            dgOrder.ItemsSource = App.db.Orders.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            newOrderWindow = new NewOrderWindow();

            newOrderWindow.ShowDialog();

            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (Order)dgOrder.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            App.db.Orders.Remove(item);
            App.db.SaveChanges();
            LoadDataGrid();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                LoadDataGrid();
            }
            var filterlist = App.db.Orders.Where(x => x.Client.FullName.StartsWith(textBox.Text));
            dgOrder.ItemsSource = filterlist.ToList();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDlg = new PrintDialog();

            printDlg.PrintVisual(dgOrder, "Grid Printing.");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();

            MessageBox.Show("Изменения успешно сохранены");
        }
    }
}
