using System;

namespace NNTPClient.Model
{
	public class NewsGroup
	{

		public string Group {
			get {
				if (group is null)
					throw new ArgumentNullException();
				return group;
			}
			private set {
				group = value;
			}
		}
		private string? group;
		public int EstArticles {
			get {
				return estArticles;
			}
			private set {
				estArticles = value;
			}
		}
		private int estArticles;
		public int FirstArticle {
			get {
				return firstArticle;
			}
			private set {
				firstArticle = value;
			}
		}
		private int firstArticle;
		public int LastArticle {
			get {
				return lastArticle;
			}
			private set {
				lastArticle = value;
			}
		}
		private int lastArticle;

		public bool HasMetaData {
			get {
				return hasMetaData;
			}
			private set {
				hasMetaData = value;
			}
		}
		private bool hasMetaData = false;


		public NewsGroup(string _group) {
			Group = _group;

		}

		public void SetGroupInfo(int est, int first, int last) {
			EstArticles = est;
			FirstArticle = first;
			LastArticle = last;
			HasMetaData = true;
		}
	}
}
