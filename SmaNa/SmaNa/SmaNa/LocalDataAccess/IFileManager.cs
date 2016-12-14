using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.LocalDataAccess
{
    /// <summary>
    /// Interface used to save and load data on any environment.
    /// @created: Marwin Philips
    /// </summary>
    public interface IFileManager
    {
        void SaveFile(string filename, string document);
        string LoadFile(string filename);

        string LoadAsset(string filename);

        Dictionary<string, string> LoadSchemas(string language);
    }
}
