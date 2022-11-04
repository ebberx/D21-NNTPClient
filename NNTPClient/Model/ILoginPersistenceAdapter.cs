namespace NNTPClient.Model
{
    public interface ILoginPersistenceAdapter
    {

        void SaveLoginDetails(LoginDetails loginDetails);
        LoginDetails RetriveLoginDetails();
    }
}
