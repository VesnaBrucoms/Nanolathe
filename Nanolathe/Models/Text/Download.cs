using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanolathe.Models.Text
{
    internal class Download
    {
        private static List<string> _sections = new List<string>() { "MENUENTRY" };
        private static Dictionary<string, SettingsFile.SettingType> _variables = new Dictionary<string, SettingsFile.SettingType>()
        {
            { "UNITMENU", SettingsFile.SettingType.String }, // Maybe var names track the section? e.g. MENUENTRY\\UNITMENU, with [] as regex replacement
            { "MENU", SettingsFile.SettingType.Int },
            { "BUTTON", SettingsFile.SettingType.Int },
            { "UNITNAME", SettingsFile.SettingType.String }
        };
        private SettingsFile _settings;

        public Download(string name)
        {
            _settings = new SettingsFile(name, SettingsFile.SettingsFileType.Tdf, CreateDefaultSettings());
        }

        public string GetStringSetting(string section, string key)
        {
            return _settings.Sections[section].Variables[key];
        }

        public int GetIntSetting(string section, string key)
        {

            return Convert.ToInt32(_settings.Sections[section].Variables[key]);
        }

        private static Dictionary<string, Section> CreateDefaultSettings()
        {
            Dictionary<string, Section> sections = new Dictionary<string, Section>();
            Dictionary<string, string> variables = new Dictionary<string, string>();
            foreach (string settingKey in _variables.Keys)
            {
                variables.Add(settingKey, "");
            }
            sections.Add("", new Section(variables, new Dictionary<string, Section>(), ""));
            return sections;
        }
    }
}
