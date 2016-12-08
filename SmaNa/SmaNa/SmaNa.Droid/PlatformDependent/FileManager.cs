using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using SmaNa.LocalDataAccess;
using System.IO;
using SmaNa.Droid.PlatformDependent;
using Android.Content.Res;

[assembly: Dependency(typeof(FileManager))]
namespace SmaNa.Droid.PlatformDependent
{
    /// <summary>
    /// Platform dependent implementation to get Acces to Strings stored in any file in the local Storage area of the app.
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Saves the document in the filename's file.
        /// </summary>
        /// <param name="filename">Name of the File</param>
        /// <param name="document">will be written into the file</param>
        public void SaveFile(string filename, string document)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, document);
        }
        /// <summary>
        /// Loads the String stored in the Filename's file.
        /// </summary>
        /// <param name="filename">the filename of the file which will be loaded</param>
        /// <returns>if the file exists it returns its content, otherwhise an empty string.</returns>
        public string LoadFile(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }else
            {
                return "";
            }
        }

        public string LoadAsset(string filename)
        {
            string content;
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(filename)))
            {
                content = sr.ReadToEnd();
            }
            return content;
        }
    }
}
