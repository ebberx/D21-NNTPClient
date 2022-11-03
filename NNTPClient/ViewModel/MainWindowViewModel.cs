using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NNTPClient.Model;

namespace NNTPClient.ViewModel {
	public class MainWindowViewModel : Bindable {

		NNTPSession session;
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
				if(article is null)
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
			session = new NNTPSession("news.dotsrc.org", "esbe4783@easv365.dk", "ed0189");

		}

		public void GetGroups() {
			NewsGroups = new ObservableCollection<NewsGroup>(session.GetNewsGroups());
		}

		public void GetGroupMeta(NewsGroup ng) {
			session.GetGroupMetaData(ng);
		}

		public void GetArticles(NewsGroup ng) {
			if (!ng.HasMetaData)
				GetGroupMeta(ng);
			Articles = new ObservableCollection<Article>(session.GetArticlesInGroup(ng));
		}

		public void GetArticle(Article a) {
			Article = session.GetArticle(a);
			MessageBox.Show(Article.Body, "Article " + Article.Number);
		}

		public void Auth() {
			session.Authenticate("esbe4783@easv365.dk", "ed0189");
		}
	}
}
