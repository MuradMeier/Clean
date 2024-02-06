using AllProject.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllProject.ui;

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CategoryWindow categoryWindow {  get; set; }
        public ProductWindow productWindow { get; set; }
        public ServiceWindow serviceWindow { get; set; }
        public OrderWindow orderWindow { get; set; }
        public ClientWindow clientWindow { get; set; }
        public StatWindow statWindow { get; set; }
        public OrderCompositionWindow orderCompositionWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCat_Click(object sender, RoutedEventArgs e)
        {
              categoryWindow = new CategoryWindow();
              content.Content = categoryWindow;
        }

        private void BtnProd_Click(object sender, RoutedEventArgs e)
        {
            productWindow = new ProductWindow();
            content.Content = productWindow;
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            serviceWindow = new ServiceWindow();

            content.Content = serviceWindow;
        }

        private void BtnOrderComposition_Click(object sender, RoutedEventArgs e)
        {
            orderCompositionWindow = new OrderCompositionWindow();

            content.Content = orderCompositionWindow;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            clientWindow = new ClientWindow();

            content.Content = clientWindow;
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            orderWindow = new OrderWindow();

            content.Content = orderWindow;
        }

        private void BtnStat_Click(object sender, RoutedEventArgs e)
        {
            statWindow = new StatWindow();

            content.Content = statWindow;
        }
    }
}
