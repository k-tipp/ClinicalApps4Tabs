using SmaNa.ViewModel;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmaNa.Multilanguage
{
    /// <summary>
    /// downloaded and modified from https://github.com/xamarin/xamarin-forms-samples/blob/master/UsingResxLocalization/UsingResxLocalization/ILocalize.cs 
    /// @created Marwin
    /// </summary>
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public static CultureInfo ci {
            get;
            set; }
        const string ResourceId = "SmaNa.Multilanguage.AppResources";
        public static ResourceManager resManager { get; set; }
        public TranslateExtension()
        {
        }
        public static String getString(String textName)
        {
            return resManager.GetString(textName, ci);
        }
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";
            if (resManager == null)
            {
                resManager = new ResourceManager(ResourceId
                                   , typeof(TranslateExtension).GetTypeInfo().Assembly);
            }
            var translation = resManager.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException (
                    String.Format ("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;  
        }
    }
}