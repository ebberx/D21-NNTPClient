using NNTPClient.ViewModel;
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
