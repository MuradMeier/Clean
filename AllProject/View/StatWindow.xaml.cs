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
    public partial class StatWindow : UserControl
    {
        public StatWindow()
        {
            InitializeComponent();
            foreach(var item in App.db.Orders)
            {
                if (year.Items.Contains(item.CompilationDate.Year))
                {

                }
                else
                {
                    year.Items.Add(item.CompilationDate.Year);
                }
            }
            month.Visibility = Visibility.Collapsed;
            day.Visibility = Visibility.Collapsed;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            printDlg.PrintVisual(dgStatProd, "Grid Printing.");
        }

        Stat s = new Stat();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
            }
            var filterlist = App.db.Categories.Where(x => x.Name.StartsWith(textBox.Text));
            dgStatProd.ItemsSource = filterlist.ToList();
        }

        private void year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month.Text = "";
            month.Items.Clear();
            year.Text = year.SelectedItem.ToString();
            var st = s.YearStat(Convert.ToInt32(year.Text));
            dgStatProd.ItemsSource = st.products;
            dgStatServ.ItemsSource = st.services;
            money.Text = st.money.ToString();

            foreach (var item in App.db.Orders.Where(x => x.CompilationDate.Year == Convert.ToInt32(year.Text)))
            {
                if (month.Items.Contains(item.CompilationDate.Year))
                {
                    month.Items.Add(item.CompilationDate.Year);
                }
            }
            month.Visibility = Visibility.Visible;
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            day.Text = "";
            day.Items.Clear();
            if (month.Text == "")
            {
                month.Text = month.Items.GetItemAt(0).ToString();
            }
            else
            {
                month.Text = month.SelectedItem.ToString();
            }
            var mn = s.stat[Convert.ToInt32(month.Text)];
            dgStatServ.ItemsSource = mn.services;
            dgStatServ.ItemsSource = mn.products;
            day.Visibility = Visibility.Visible;
            foreach (var item in App.db.Orders.Where(x => x.CompilationDate.Year.ToString() == year.Text & x.CompilationDate.Month.ToString() == month.Text))
            {
                if (month.Items.Contains(item.CompilationDate.Year))
                {
                    day.Items.Add(item.CompilationDate.Year);
                }
            }
        }

        private void day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (month.Text == "")
            {
                month.Text = month.Items.GetItemAt(0).ToString();
            }
            else
            {
                month.Text = month.SelectedItem.ToString();
            }
            var d = s.stat[Convert.ToInt32(month.Text)].stat[Convert.ToInt32(day.Text)];
            dgStatServ.ItemsSource = d.services;
            dgStatProd.ItemsSource = d.products;
        }
    }
}
