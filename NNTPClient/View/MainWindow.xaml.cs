using NNTPClient.Model;
using NNTPClient.ViewModel;
using System.Windows;

namespace NNTPClient.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		MainWindowViewModel viewModel;

		public MainWindow() {
			InitializeComponent();

			viewModel = new MainWindowViewModel();
			this.DataContext = viewModel;
		}

		private void onGetNewsGroupsClick(object sender, RoutedEventArgs e) {
			viewModel.GetGroups();
		}

		private void onGetArticlesClick(object sender, RoutedEventArgs e) {
			NewsGroup ng = (NewsGroup)ListNewsGroup.SelectedItem;
			if (ng is null) {
				MessageBox.Show("Please select a News Group.", "INFO");
				return;
			}
			viewModel.GetArticles(ng);
		}

		private void onGetArticleClick(object sender, RoutedEventArgs e) {
			Article a = (Article)ListArticles.SelectedItem;
			if (a is null) {
				MessageBox.Show("Please select an Article.", "INFO");
				return;
			}
			viewModel.GetArticle(a);

		}

		private void onPostArticleClick(object sender, RoutedEventArgs e) {
			NewsGroup ng = (NewsGroup)ListNewsGroup.SelectedItem;
			if (ng is null) {
				MessageBox.Show("Please select a News Group.", "INFO");
				return;
			}
			viewModel.PostArticleWindow(ng);
		}
	}
}
