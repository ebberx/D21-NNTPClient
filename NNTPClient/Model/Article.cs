using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPClient.Model {
	public class Article {

		public int Number { get { return number; } set { number = value; } }
		private int number;
		public string Subject { get { return subject; } set { subject = value; } }
		private string subject;
		
		public string From { get { return from; } set { from = value; } }
		private string from;

		public string Body { get { return body; } set { body = value; } }
		private string body;


	}
}
