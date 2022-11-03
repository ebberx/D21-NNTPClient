using NNTPClient.Model;
using NNTPClient.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace NNTPClient.ViewModel
{
	public class MainWindowViewModel : Bindable
	{


		public ObservableCollection<NewsGroup> NewsGroups {
			get {
				if (newsGroups is null)
					throw new ArgumentNullException();
				return newsGroups;
			}
			set {
				newsGroups = value;
				propertyIsChanged();
			}
		}
		private ObservableCollection<NewsGroup>? newsGroups;
		public ObservableCollection<Article> Articles {
			get {
				if (articles is null)
					throw new ArgumentNullException();
				return articles;
			}
			set {
				articles = value;
				propertyIsChanged();
			}
		}
		private ObservableCollection<Article>? articles;

		public Article Article {
			get {
				if (article is null)
					throw new ArgumentNullException();
				return article;
			}
			set {
				article = value;
				propertyIsChanged();
			}
		}
		private Article? article;

		public MainWindowViewModel() {
			//NNTPSession.Connect("news.dotsrc.org", "esbe4783@easv365.dk", "ed0189");

		}

		public void GetGroups() {
			NewsGroups = new ObservableCollection<NewsGroup>(NNTPSession.GetNewsGroups());
		}

		public void GetGroupMeta(NewsGroup ng) {
			NNTPSession.GetGroupMetaData(ng);
		}

		public void GetArticles(NewsGroup ng) {
			if (!ng.HasMetaData)
				GetGroupMeta(ng);
			Articles = new ObservableCollection<Article>(NNTPSession.GetArticlesInGroup(ng));
		}

		public void GetArticle(Article a) {
			Article = NNTPSession.GetArticle(a);
			MessageBox.Show(Article.Body, "Article " + Article.Number);
		}

		public void Auth() {
			NNTPSession.Authenticate("esbe4783@easv365.dk", "ed0189");
		}

		public bool showingPostWindow = false;
		public void PostArticleWindow(NewsGroup ng) {
			if (!showingPostWindow) {
				PostArticleView postView = new(() => { showingPostWindow = false; }, ng);
				postView.Show();
				showingPostWindow = true;
			}
		}
	}
}
