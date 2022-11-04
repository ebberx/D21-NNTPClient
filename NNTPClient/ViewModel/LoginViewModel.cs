using NNTPClient.Model;

namespace NNTPClient.ViewModel
{
    public class LoginViewModel : Bindable
    {

        public string Server { get { return server; } set { server = value; propertyIsChanged(); } }
        private string server;

        public string User { get { return user; } set { user = value; propertyIsChanged(); } }
        private string user;

        public string Pass { get { return pass; } set { pass = value; propertyIsChanged(); } }
        private string pass;


        public bool ConnectToServer() {
            ILoginPersistenceAdapter loginFile = new LoginDetailsFile();
            loginFile.SaveLoginDetails(new LoginDetails(Server, User, Pass));

            return NNTPSession.Connect(Server, User, Pass);
        }

        public void LoadLoginDetails() {
            ILoginPersistenceAdapter loginFile = new LoginDetailsFile();
            LoginDetails loginDetails = loginFile.RetriveLoginDetails();

            Server = loginDetails.Server;
            User = loginDetails.User;
            Pass = loginDetails.Pass;
        }
    }
}
