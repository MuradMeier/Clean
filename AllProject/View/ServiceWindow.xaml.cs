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
    public partial class ServiceWindow : UserControl
    {
        public Service service { get; set; }
        public NewServiceWindow newServForm{ get; set; }
        public ServiceWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            dgServ.ItemsSource = App.db.Services.ToList();

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewServiceWindow newServForm = new NewServiceWindow();

            newServForm.ShowDialog();

            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (Service)dgServ.SelectedItem;
            if (item == null) return;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            try
            {
                App.db.Services.Remove(item);
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

            printDlg.PrintVisual(dgServ, "Grid Printing.");
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                LoadDataGrid();

            }
            var filterlist = App.db.Services.Where(x => x.ServiceName.StartsWith(textBox.Text));
            dgServ.ItemsSource = filterlist.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();
            LoadDataGrid();
        }
    }
}
