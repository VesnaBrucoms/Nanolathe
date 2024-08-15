using Lombok.NET;

namespace Nanolathe.Models.Hpi
{
    internal partial class ArchiveDirectory
    {
        [Property]
        private string _name;
        [Property]
        private readonly List<ArchiveDirectory> _subdirectories;
        [Property]
        private readonly List<ArchiveFile> _files;

        public ArchiveDirectory(string name)
        {
            _name = name;
            _subdirectories = [];
            _files = [];
        }
    }
}
