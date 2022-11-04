using NNTPClient.Model;

namespace NNTPClient.ViewModel
{
    public class PostArticleViewModel : Bindable
    {

        public string PostingGroup { get { return postingGroup; } set { postingGroup = value; propertyIsChanged(); } }
        private string postingGroup;

        public string Subject { get { return subject; } set { subject = value; propertyIsChanged(); } }
        private string subject;

        public string Body { get { return body; } set { body = value; propertyIsChanged(); } }
        private string body;


        public PostArticleViewModel(string group) {
            PostingGroup = group;
        }

        public int PostArticle() {
            return NNTPSession.PostArticle(PostingGroup, Subject, "esbe4783@easv.dk", Body);
        }
    }
}
