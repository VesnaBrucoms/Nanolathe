using Lombok.NET;

namespace Nanolathe.Models.Hpi
{
    [AllArgsConstructor]
    internal partial class HpiEntry
    {
        public enum EntryType
        {
            File = 0,
            Subdirectory = 1
        }

        [Property]
        private readonly int _nameOffset;
        [Property]
        private readonly int _dataOffset;
        [Property]
        private readonly EntryType _type;
    }
}
