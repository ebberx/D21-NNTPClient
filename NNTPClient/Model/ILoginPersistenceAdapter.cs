using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTPClient.Model
{
    public interface ILoginPersistenceAdapter {

        void SaveLoginDetails(LoginDetails loginDetails);
        LoginDetails RetriveLoginDetails();
    }
}
