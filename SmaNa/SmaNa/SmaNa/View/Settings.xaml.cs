using SmaNa.Model;
using SmaNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Xamarin.Forms;

namespace SmaNa.View
{
    /// <summary>
    /// On This page the user can edit the apps settings
    /// </summary>
    public partial class Settings : ContentPage
    {
        private ViewModelSettings _viewModel;
        private Dictionary<string, Schema> SchemaDictionary;
        private Dictionary<string, CultureInfo> LanguageDictionary;
        public Settings()
        {
            InitializeComponent();
            _viewModel = App.ViewModelSettings;

            MidataSwitch.IsVisible = true;

            ToolbarItems.Add(new ToolbarItem(Multilanguage.TranslateExtension.getString("SaveSettings"), "", () =>
            {
                var saveSettings = ViewModelSettings.SmaNaSettings;

                Schema schema;
                SchemaDictionary.TryGetValue(SettingsSchema.Items[SettingsSchema.SelectedIndex], out schema);

                saveSettings.Schema = schema.filename;
                saveSettings.OperationDate = OperationDate.Date;
                saveSettings.StageingComplete = StageingComplete.IsToggled;
                var languageChanged = false;
                if (saveSettings.LanguageString != Language.Items[Language.SelectedIndex])
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
            SettingsSchema.SelectedIndex = SettingsSchema.Items.IndexOf(SchemaDictionary.FirstOrDefault(x => x.Value.filename == settings.Schema).Key);
            if (SettingsSchema.SelectedIndex == -1) SettingsSchema.SelectedIndex = 0;
            Language.SelectedIndex = Language.Items.IndexOf(LanguageDictionary.FirstOrDefault(x => x.Value.Name == settings.LanguageString).Key);
            OperationDate.Date = settings.OperationDate;
            StageingComplete.IsToggled = settings.StageingComplete;
            MiData.IsToggled = false;
            MidataLayout.IsVisible = false;

        }

        private void InitializeDropdowns()
        {
            LanguageDictionary = new Dictionary<string, CultureInfo>();
            LanguageDictionary.Add("Deutsch", new CultureInfo("de-CH"));
            LanguageDictionary.Add("Français", new CultureInfo("fr-CH"));

            foreach (string language in LanguageDictionary.Keys)
            {
                Language.Items.Add(language);
            }
            // Fill Dropdown for schema
            SchemaDictionary = _viewModel.getSchemas();
            foreach (string s in SchemaDictionary.Keys)
            {
                SettingsSchema.Items.Add(s);
            }
        }

        public async void OnMidataGetClicked(object sender, EventArgs e)
        {
            var weight = await _viewModel.GetLastWeight();
            MidataData.Text = weight;
        }
        public void OnMidataSendClicked(object sender, EventArgs e)
        {
            _viewModel.SaveWeight(MidataWeight.Text);
            MidataWeight.Text = "";
        }

        public void OnMidataToggled(object sender, EventArgs e)
        {
            MidataLayout.IsVisible = MiData.IsToggled;
        }
        public void onStageingCompleteInfoClicked(object sender, EventArgs e)
        {
            DisplayAlert(Multilanguage.TranslateExtension.getString("SettingsStageingComplete"), Multilanguage.TranslateExtension.getString("SettingsStageingCompleteInfo"), "OK");
        }
        public void onSchemaInfoClicked(object sender, EventArgs e)
        {
            DisplayAlert(Multilanguage.TranslateExtension.getString("SettingsSchema"), Multilanguage.TranslateExtension.getString("SettingsSchemaInfo"), "OK");
        }
        public void onOperationDateInfoClicked(object sender, EventArgs e)
        {
            DisplayAlert(Multilanguage.TranslateExtension.getString("SettingsOPDate"), Multilanguage.TranslateExtension.getString("SettingsOPDateInfo"), "OK");
        }
    }
}
