using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmaNa.LocalDataAccess;
using Xamarin.Auth;
using Xamarin.Forms;
using SmaNa.Droid.PlatformDependent;

[assembly: Dependency(typeof(PasswordManager))]
namespace SmaNa.Droid.PlatformDependent
{
    /// <summary>
    /// Android specific class to load and save a password inside the local Key Store. 
    /// @created: Marwin Philips
    /// </summary>
    public class PasswordManager : IPasswordManager
    {
        // The AppName identifies the App inside the KeyStore
        private static readonly string appName = "SmaNa";

        /// <summary>
        /// Returns a previously saved password from the local AccountStore
        /// </summary>
        /// <returns>The password or null if none was set</returns>
        public string GetPassword()
        {
            var account = AccountStore.Create(Forms.Context).FindAccountsForService(appName).FirstOrDefault();
            return (account != null) ? account.Properties["Password"] : null;
        }

        /// <summary>
        /// Saves the password in the local AccountStore.
        /// </summary>
        /// <param name="password">The password to be saved.</param>
        public void SavePassword(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account
                {
                    Username = "default"
                };
                account.Properties.Add("Password", password);
                AccountStore.Create(Forms.Context).Save(account, appName);
            }
        }
    }
}