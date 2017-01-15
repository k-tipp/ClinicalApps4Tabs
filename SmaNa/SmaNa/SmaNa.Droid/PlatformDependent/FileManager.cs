using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SmaNa.LocalDataAccess;
using System.IO;
using SmaNa.Droid.PlatformDependent;
using Android.Content.Res;

[assembly: Dependency(typeof(FileManager))]
namespace SmaNa.Droid.PlatformDependent
{
    /// <summary>
    /// @created: Marwin Philips
    /// @created: Kevin Tippenhauer
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
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Loads the schemas based on the parameter language
        /// </summary>
        /// <param name="language">the language of the schemas to load</param>
        /// <returns>if a file for the language exists in the application assets it returns a Dictionary with the matching filenames and their content, otherwhise an empty Dictionary.</returns>
        public Dictionary<string, string> LoadSchemas(string language)
        {
            AssetManager assets = Android.App.Application.Context.Assets;
            Dictionary<string, string> contents = new Dictionary<string, string>();
            foreach (string s in assets.List(""))
            {
                if (s.StartsWith(language) && s.EndsWith(".csv"))
                {
                    contents.Add(s, LoadAsset(s));
                }
            }
            return contents;
        }

        /// <summary>
        /// Loads the String stored in the Filename's file. The file must be stored in the application assets and the submitted filename must be a simple filename.
        /// </summary>
        /// <param name="filename">the filename of the file which will be loaded</param>
        /// <returns>if the file exists it returns its content, otherwhise an empty string.</returns>
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
