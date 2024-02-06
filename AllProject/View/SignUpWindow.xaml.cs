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
using System.Windows.Shapes;

namespace AllProject
{
    /// <summary>
    /// Логика взаимодействия для SignUpForm.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public static Login log;
        public static MainWindow mainWindow;

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Sign();

        }

        public void Sign()
        {
            try
            {
                log = App.db.Logins.FirstOrDefault(u => u.Username == login.Text && u.Password == pass.Password);
                pass.Password = "";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                pass.Password = "";
                return;
            }
            if (log == null) return;
 
            MessageBox.Show("Добро пожаловать - " + log.RealName);

            Hide();
            mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            Show();
        }

        private void pass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Sign();
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
