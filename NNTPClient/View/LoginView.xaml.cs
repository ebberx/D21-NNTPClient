using NNTPClient.ViewModel;
using System.Windows;

namespace NNTPClient.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        LoginViewModel viewModel;
        public LoginView() {
            InitializeComponent();

            viewModel = new();
            this.DataContext = viewModel;

            viewModel.LoadLoginDetails();
        }

        private void onLoginClick(object sender, RoutedEventArgs e) {
            if (viewModel.ConnectToServer()) {
                MainWindow mw = new();
                mw.Show();
                Close();
            }
            else
                MessageBox.Show("Failed to login to server.", "INFO");
        }
    }
}
