using NNTPClient.ViewModel;
using System;
using System.IO;
using System.Windows;
using NNTPClient.Model;

namespace NNTPClient.View
{
    /// <summary>
    /// Interaction logic for PostArticleView.xaml
    /// </summary>
    public partial class PostArticleView : Window
    {

        PostArticleViewModel viewModel;

        public PostArticleView(Action onClose, NewsGroup group) {
            InitializeComponent();

            // Setup viewmodel as data context
            viewModel = new(group);
            this.DataContext = viewModel;

            // Lambda expression to run when window closes
            Closing += (o, e) => { onClose(); };
        }

        private void onCancelClick(object sender, RoutedEventArgs e) {
            Close();
        }

        private void onPostClick(object sender, RoutedEventArgs e) {
            int result = viewModel.PostArticle();

            if (result == 240)
                MessageBox.Show("Article was successfully posted.", "INFO");
            else
                MessageBox.Show("Posting failed.", "ERROR");
            Close();
        }
    }
}
