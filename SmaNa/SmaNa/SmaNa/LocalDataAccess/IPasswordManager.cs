using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.LocalDataAccess
{
    public interface IPasswordManager
    {
        void SavePassword(string password);
        string GetPassword();
    }
}
