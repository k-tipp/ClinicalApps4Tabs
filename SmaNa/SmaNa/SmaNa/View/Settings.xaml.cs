using SmaNa.Model;
using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmaNa.View
{
    public partial class Settings : ContentPage
    {
        private ViewModelSettings _viewModel;
        private Dictionary<string,Enumerations.CancerType> CancerTypeDictionary;
        private Dictionary<string, Enumerations.TnmT> TnmTDictionary;
        private Dictionary<string, Enumerations.TnmN> TnmNDictionary;
        private Dictionary<string, Enumerations.TnmM> TnmMDictionary;
        private Dictionary<string, Enumerations.Schema> SchemaDictionary;
        private Dictionary<string, CultureInfo> LanguageDictionary;
        public Settings()
        {
            InitializeComponent();
            _viewModel = App.ViewModelSettings;
            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("SaveSettings"), "", () =>
            {
                var saveSettings = ViewModelSettings.SmaNaSettings;
                Enumerations.CancerType type;
                CancerTypeDictionary.TryGetValue(CarcinomType.Items[CarcinomType.SelectedIndex], out type);
                saveSettings.CarcinomType = type;
                Enumerations.TnmT tnmT;
                TnmTDictionary.TryGetValue(SettingsTnmT.Items[SettingsTnmT.SelectedIndex], out tnmT);
                saveSettings.TnmT = tnmT;
                Enumerations.TnmN tnmN;
                TnmNDictionary.TryGetValue(SettingsTnmN.Items[SettingsTnmN.SelectedIndex], out tnmN);
                saveSettings.TnmN = tnmN;
                Enumerations.TnmM tnmM;
                TnmMDictionary.TryGetValue(SettingsTnmM.Items[SettingsTnmM.SelectedIndex], out tnmM);
                saveSettings.TnmM = tnmM;
                Enumerations.Schema schema;
                SchemaDictionary.TryGetValue(SettingsSchema.Items[SettingsSchema.SelectedIndex], out schema);
                saveSettings.Schema = schema;
                saveSettings.OperationDate = OperationDate.Date;
                saveSettings.StageingComplete = StageingComplete.IsToggled;
                var languageChanged = false;
                if(saveSettings.LanguageString != Language.Items[Language.SelectedIndex])
                {
                    CultureInfo ci;
                    LanguageDictionary.TryGetValue(Language.Items[Language.SelectedIndex], out ci);
                    saveSettings.Language = ci;
                    languageChanged = true;
                }
                _viewModel.SaveSettings();
                if (languageChanged)
                {
                    App.SetCulture(saveSettings.Language);
                }
                //await Navigation.PopAsync();
            }));
            InitializeDropdowns();
            var settings = ViewModelSettings.SmaNaSettings;
            CarcinomType.SelectedIndex = CarcinomType.Items.IndexOf(CancerTypeDictionary.FirstOrDefault(x => x.Value == settings.CarcinomType).Key);
            SettingsTnmT.SelectedIndex = SettingsTnmT.Items.IndexOf(TnmTDictionary.FirstOrDefault(x => x.Value == settings.TnmT).Key);
            SettingsTnmN.SelectedIndex = SettingsTnmN.Items.IndexOf(TnmNDictionary.FirstOrDefault(x => x.Value == settings.TnmN).Key);
            SettingsTnmM.SelectedIndex = SettingsTnmM.Items.IndexOf(TnmMDictionary.FirstOrDefault(x => x.Value == settings.TnmM).Key);
            SettingsSchema.SelectedIndex = SettingsSchema.Items.IndexOf(SchemaDictionary.FirstOrDefault(x => x.Value == settings.Schema).Key);
            Language.SelectedIndex = Language.Items.IndexOf(LanguageDictionary.FirstOrDefault(x => x.Value.Name == settings.LanguageString).Key);
            OperationDate.Date = settings.OperationDate;
            StageingComplete.IsToggled = settings.StageingComplete;

        }

        private void InitializeDropdowns()
        {
            LanguageDictionary = new Dictionary<string, CultureInfo>();
            LanguageDictionary.Add("Deutsch", new CultureInfo("de-CH"));
            LanguageDictionary.Add("Français", new CultureInfo("fr-CH"));

            foreach(string language in LanguageDictionary.Keys)
            {
                Language.Items.Add(language);
            }
            // Handling different Cancer-Types, which have Multi-Language text within
            var cancerTypes = Enum.GetValues(typeof(Enumerations.CancerType)).Cast<Enumerations.CancerType>();
            CancerTypeDictionary = new Dictionary<string, Enumerations.CancerType>();
            foreach (Enumerations.CancerType t in cancerTypes)
            {
                CancerTypeDictionary.Add(Multilanguage.TranslateExtension.getString(t.ToString("F")), t);
            }
            foreach (string s in CancerTypeDictionary.Keys)
            {
                CarcinomType.Items.Add(s);
            }

            // TNM is in all Languages always the same.
            // Fill Dropdown for T
            var tnmTs = Enum.GetValues(typeof(Enumerations.TnmT)).Cast<Enumerations.TnmT>();
            TnmTDictionary = new Dictionary<string, Enumerations.TnmT>();
            foreach (Enumerations.TnmT t in tnmTs)
            {
                TnmTDictionary.Add(t.ToString("F"), t);
            }
            foreach (string s in TnmTDictionary.Keys)
            {
                SettingsTnmT.Items.Add(s);
            }
            // Fill Dropdown for N
            var tnmNs = Enum.GetValues(typeof(Enumerations.TnmN)).Cast<Enumerations.TnmN>();
            TnmNDictionary = new Dictionary<string, Enumerations.TnmN>();
            foreach (Enumerations.TnmN t in tnmNs)
            {
                TnmNDictionary.Add(t.ToString("F"), t);
            }
            foreach (string s in TnmNDictionary.Keys)
            {
                SettingsTnmN.Items.Add(s);
            }
            // Fill Dropdown for M
            var tnmMs = Enum.GetValues(typeof(Enumerations.TnmM)).Cast<Enumerations.TnmM>();
            TnmMDictionary = new Dictionary<string, Enumerations.TnmM>();
            foreach (Enumerations.TnmM t in tnmMs)
            {
                TnmMDictionary.Add(t.ToString("F"), t);
            }
            foreach (string s in TnmMDictionary.Keys)
            {
                SettingsTnmM.Items.Add(s);
            }

            // Fill Dropdown for schema
            var schemas = Enum.GetValues(typeof(Enumerations.Schema)).Cast<Enumerations.Schema>();
            SchemaDictionary = new Dictionary<string, Enumerations.Schema>();
            foreach (Enumerations.Schema t in schemas)
            {
                SchemaDictionary.Add(t.ToString("F").Replace('_', ' '), t);
            }
            foreach (string s in SchemaDictionary.Keys)
            {
                SettingsSchema.Items.Add(s);
            }
        }
        
    }
}
