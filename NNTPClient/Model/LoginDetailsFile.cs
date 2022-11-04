using System.IO;

namespace NNTPClient.Model
{
    public class LoginDetailsFile : ILoginPersistenceAdapter
    {
        // lines in order:
        // Server\nUser\nPass
        public LoginDetails RetriveLoginDetails() {
            if (!File.Exists("login.conf"))
                return new LoginDetails("", "", "");

            string[] content = File.ReadAllText("login.conf").Split('\n');
            return new LoginDetails(content[0], content[1], Base64.Base64Decode(content[2]));
        }

        public void SaveLoginDetails(LoginDetails ld) {
            string details = ld.Server + "\n" + ld.User + "\n" + Base64.Base64Encode(ld.Pass) + "\n";
            File.WriteAllText("login.conf", details);
        }
    }
}
