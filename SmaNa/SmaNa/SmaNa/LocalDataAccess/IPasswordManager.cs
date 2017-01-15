namespace SmaNa.LocalDataAccess
{
    public interface IPasswordManager
    {
        /// <summary>
        /// Interface used to save and load Passwords on any environment.
        /// @created: Marwin Philips
        /// </summary>
        void SavePassword(string password);
        string GetPassword();
    }
}
