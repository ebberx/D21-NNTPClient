using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return new LoginDetails(content[0], content[1], content[2]);
        }

        public void SaveLoginDetails(LoginDetails ld) {
            string details = ld.Server + "\n" + ld.User + "\n" + ld.Pass + "\n";
            File.WriteAllText("login.conf", details);
        }
    }
}
