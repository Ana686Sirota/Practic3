using Microsoft.Win32;
using sirota.Classes;
using sirota.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sirota
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (PassBox1.Visibility == Visibility.Hidden)
            {
                PassHide.Visibility = Visibility.Hidden;
            }
        }

        private async void Voiti_Click(object sender, RoutedEventArgs e)
        {

            if (PassBox1.Visibility == Visibility.Visible)
            {
                PassBox.Password = PassBox1.Text;
            }


            var login = LoginBox.Text;
            var pass = PassBox.Password;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => (x.Login == login || x.Email == login) && x.Password == pass);
            if (user is null)
            {
                    ErrorBox.Text = "Неправильный логин или пароль!\nПопробуйте снова!";
                    logBox.BorderThickness = new Thickness(3);
                    logBox.BorderBrush = Brushes.Red;

                    PassBox.BorderThickness = new Thickness(3);
                    PassBox.BorderBrush = Brushes.Red;

                    PassBox1.BorderThickness = new Thickness(3);
                    PassBox1.BorderBrush = Brushes.Red;

                    Hidded.BorderThickness = new Thickness(3);
                    Hidded.BorderBrush = Brushes.Red;

                    ErrorBtn.Visibility = Visibility.Visible;

                    LoginBox.IsEnabled = false;
                    PassBox.IsEnabled = false;
                    PassBox1.IsEnabled = false;
                    Voiti.IsEnabled = false;
                    PassHide.IsEnabled = false;
                    PassVisib.IsEnabled = false;
                    RegBtn.IsEnabled = false;

                    //MessageBox.Show("Неправильный логин или пароль!\nПопробуйте снова!");
                    return;

            }
            else
            {
                MessageBox.Show("Вы успешно вошли в аккаунт!");
                int id = user.Id;
                Window1 Win1 = new Window1(id);
                Win1.Show();
                this.Hide();


            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Registr reg = new Registr();
            reg.Show();
            this.Hide();
        }

        private void PassHide_Click(object sender, RoutedEventArgs e)
        {
            PassBox1.Text = PassBox.Password;
            Hidded.Visibility = Visibility.Visible;
            PassBox1.Visibility = Visibility.Visible;
            PassBox.PasswordChar = '•'; // Скрываем пароль
            PassHide.Visibility = Visibility.Hidden;
            PassVisib.Visibility = Visibility.Visible;
        }

        private void PassVisib_Click(object sender, RoutedEventArgs e)
        {
            PassBox.Password = PassBox1.Text;
            Hidded.Visibility = Visibility.Hidden;
            PassBox1.Visibility = Visibility.Hidden; // Показываем пароль
            PassVisib.Visibility = Visibility.Hidden;
            PassHide.Visibility = Visibility.Visible;
            PassBox.Visibility = Visibility.Visible;
        }

        private void ErrorBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrorBox.Text = "";
            ErrorBtn.Visibility = Visibility.Hidden;
            Voiti.IsEnabled = true;

            logBox.BorderBrush = Brushes.Transparent;
            PassBox.BorderBrush = Brushes.Transparent;
            PassBox1.BorderBrush = Brushes.Transparent;
            Hidded.BorderBrush = Brushes.Transparent;

            LoginBox.IsEnabled = true;
            PassBox.IsEnabled = true;
            PassBox1.IsEnabled = true;
            Voiti.IsEnabled = true;
            PassHide.IsEnabled = true;
            PassVisib.IsEnabled = true;
            RegBtn.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DisableControls()
        {
            LoginBox.IsEnabled = false;
            PassBox.IsEnabled = false;
            PassBox1.IsEnabled = false;
            Voiti.IsEnabled = false;
            RegBtn.IsEnabled = false;
            PassHide.IsEnabled = false;
            PassVisib.IsEnabled = false;
        }

        private void EnableControls()
        {
            LoginBox.IsEnabled = true;
            PassBox.IsEnabled = true;
            PassBox1.IsEnabled = true;
            Voiti.IsEnabled = true;
            RegBtn.IsEnabled = true;
            PassHide.IsEnabled = true;
            PassVisib.IsEnabled = true;
        }
    }
}