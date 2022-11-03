using System;

namespace NNTPClient.Model
{
	public class Article
	{

		public int Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}
		private int number;
		public string Subject {
			get {
				if (subject is null)
					throw new ArgumentNullException();
				return subject;
			}
			set {
				subject = value;
			}
		}
		private string? subject;

		public string From {
			get {
				if (from is null)
					throw new ArgumentNullException();
				return from;
			}
			set {
				from = value;
			}
		}
		private string? from;

		public string Body {
			get {
				if (body is null)
					throw new ArgumentNullException();
				return body;
			}
			set {
				body = value;
			}
		}
		private string? body;


	}
}
