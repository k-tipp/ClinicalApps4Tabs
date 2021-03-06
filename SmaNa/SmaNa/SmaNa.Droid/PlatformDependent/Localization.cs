using Xamarin.Forms;
using System.Globalization;
using System.Threading;
using SmaNa.Multilanguage;

[assembly: Dependency(typeof(SmaNa.Droid.PlatformDebendent.Localize))]
/// <summary>
/// @created: Marwin Philips
/// See important comment on source in ILocalize!
/// Android-Specific implementation to get the netLanguage of the System which is the base of the multilanguage ability.
/// </summary>
namespace SmaNa.Droid.PlatformDebendent
{

    public class Localize : SmaNa.Multilanguage.ILocalize
    {
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public CultureInfo GetCurrentCultureInfo()
        {
            // currently hard-coded german language
            var netLanguage = "de-CH";
            //var androidLocale = Java.Util.Locale.Default;
            //netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));
            // this gets called a lot - try/catch can be expensive so consider caching or something
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch
                {
                    // iOS language not valid .NET culture, falling back to German
                    ci = new System.Globalization.CultureInfo("de-CH");
                }
            }
            return ci;
        }
        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "de-LU":   
                case "de-LI":   
                case "de-DE":
                case "DE":
                    netLanguage = "de-ch"; // closest supported
                    break;
                case "fr-CH":
                case "fr-MC":
                case "fr-LU":
                case "fr-FR":
                    netLanguage = "fr-CH"; // correct code for .NET
                    break;
                case "gsw-CH":  // "Schwiizert��tsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }
    }
}