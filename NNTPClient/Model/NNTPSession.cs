using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Shapes;

namespace NNTPClient.Model {
	public class NNTPSession {

		TcpClient? tcpClient;
		NetworkStream? stream;

		public NNTPSession(string host, string user, string pass) {
			tcpClient = new TcpClient(host, 119);

			if (tcpClient.Connected)
				Debug.WriteLine("Connected to " + host);

			stream = tcpClient.GetStream();

			// Wait for 200 response
			StreamReader sr = new StreamReader(stream);
			while (!stream.DataAvailable) ;
			string? line = sr.ReadLine();
            if (line is null)
                throw new ArgumentNullException();
            if (!line.Contains("200"))
				throw new InvalidDataException("Expected '200 news.sunsite.dk NNRP Service Ready - staff@sunsite.dk (posting ok).'");
			Debug.WriteLine(line);

			// Then Authenticate
			Authenticate(user, pass);
		}

		/* DEPRECATED
		public void ReadToConsole() {
			StreamReader sr = new StreamReader(stream);
			while (tcpClient.Available > 0) {
				string? line = sr.ReadLine();
				if (line is null || String.IsNullOrEmpty(line))
					continue;
				Debug.WriteLine(line);
			}
		}
		*/

		public void Authenticate(string user, string pass) {

			if (stream is null)
				throw new Exception("Not connected to server");

			StreamWriter sw = new StreamWriter(stream);
			StreamReader sr = new StreamReader(stream);

			// Write user
			sw.Write("authinfo user " + user + "\r\n");
			sw.Flush();

			// Expect 381 response
			while (!stream.DataAvailable) ;
			string? line = sr.ReadLine();
			if (line is null)
				throw new ArgumentNullException();

			if (!line.Contains("381"))
				throw new InvalidDataException("Expected '381 PASS required'");
			Debug.WriteLine(line);

			// Write pass
			sw.Write("authinfo pass " + pass + "\r\n");
			sw.Flush();

			// Expect 281 response
			while (!stream.DataAvailable) ;
			line = sr.ReadLine();
            if (line is null)
                throw new ArgumentNullException();

            if (!line.Contains("281"))
				throw new InvalidDataException("Expected '281 Ok'");
			Debug.WriteLine(line);
		}

		public Article GetArticle(Article article) {
            if (stream is null)
                throw new Exception("Not connected to server");

            StreamWriter sw = new StreamWriter(stream);
			StreamReader sr = new StreamReader(stream);

			// Query the server for the body of the article
			sw.Write("body " + article.Number + "\r\n");
			sw.Flush();
			while (!stream.DataAvailable) ;

			// Retrieve body
			string body = "";
			while (true) {
				string? line = sr.ReadLine();

				if (line is null)
					continue;

                // Skip '222' line
                if (line.StartsWith("222"))
					continue;
				// Halt on end of body or '423' response
				if (line.StartsWith(".") || line.StartsWith("423"))
					break;

				body += line;
			}
			
			article.Body = body;
			return article;
		}
		
		public List<Article> GetArticlesInGroup(NewsGroup ng) {

            if (stream is null)
                throw new Exception("Not connected to server");

            StreamWriter sw = new StreamWriter(stream);
			StreamReader sr = new StreamReader(stream);

			List<Article> articles = new List<Article>();

			// Select group
			sw.Write("group "+ ng.Group +"\r\n");
			sw.Flush();

			// Clear '211' response from GROUP command
			while (!stream.DataAvailable) ;
			Debug.WriteLine(sr.ReadLine());

			for (int i = ng.FirstArticle; i < ng.LastArticle; i++) {
				sw.Write("head \r\n");
				sw.Write("next \r\n");
				sw.Flush();

				while (!stream.DataAvailable) ;

				Article a = new Article();
				while (true) {
					string? line = sr.ReadLine();
					if (line is null || String.IsNullOrEmpty(line))
						continue;
					if (line.StartsWith("."))
						break;

					if (line.StartsWith("221")) {
						a.Number = Int32.Parse(line.Split(' ')[1]);
					}
					if (line.StartsWith("Subject")) { 
						a.Subject = line.Substring(9);
					}
					if (line.StartsWith("From")) {
						a.From = line.Substring(6);
					}
				}

				// Clear '223' response from NEXT command
				sr.ReadLine();

				articles.Add(a);
			}
			Debug.WriteLine("articles: " + articles.Count);
			return articles;
		}
		

		public NewsGroup GetGroupMetaData(NewsGroup ng) {

            if (stream is null)
                throw new Exception("Not connected to server");

            // Send request
            StreamWriter sw = new StreamWriter(stream);
			sw.Write("group "+ ng.Group +"\r\n");
			sw.Flush();

			// Wait for response
			while (!stream.DataAvailable) ;
			StreamReader sr = new StreamReader(stream);
			
			// Handle response
			string[]? line = sr.ReadLine()?.Split(' ');
            if (line is null)
                throw new ArgumentNullException();
            int est = Int32.Parse(line[1]);
			int first = Int32.Parse(line[2]);
			int last = Int32.Parse(line[3]);

			ng.SetGroupInfo(est, first, last);
			Debug.WriteLine($"Got {est}, {first}, {last} for {ng.Group}");

			return ng;
		}

		public List<NewsGroup> GetNewsGroups() {
            if (stream is null)
                throw new Exception("Not connected to server");

            // Send request
            StreamWriter sw = new StreamWriter(stream);
			sw.Write("list\r\n");
			sw.Flush();

			// Wait for response
			while (!stream.DataAvailable) ;

			StreamReader sr = new StreamReader(stream);
			List<NewsGroup> groups = new List<NewsGroup>();

			while (true) {
				string? line = sr.ReadLine();
				if (line is null || String.IsNullOrEmpty(line) || line.StartsWith("215"))
					continue;
				if (line.StartsWith("."))
					break;

				NewsGroup ng = new NewsGroup(line.Split(' ')[0]);
				groups.Add(ng);
			}
			Debug.WriteLine("returned group list with size of " + groups.Count);
			return groups;
		}
	}
}
