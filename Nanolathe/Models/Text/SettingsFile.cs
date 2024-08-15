using Lombok.NET;

namespace Nanolathe.Models.Text
{
    [AllArgsConstructor]
    internal partial class SettingsFile
    {
        public enum SettingsFileType
        {
            Tdf = 0,
            Fbi = 1,
            Ota = 2
        }

        public enum SettingType
        {
            String = 0,
            Int = 1,
            Float = 2,
            Bool = 3,
            List = 4,
            Enum = 5
        }

        [Property]
        private string _name;
        [Property]
        private readonly SettingsFileType _type;
        [Property]
        private readonly Dictionary<string, Section> sections;
    }
}
