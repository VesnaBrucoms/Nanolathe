using Lombok.NET;

namespace Nanolathe.Models.Hpi
{
    [AllArgsConstructor]
    internal partial class ArchiveFile
    {
        [Property]
        private string _name;
        [Property]
        private readonly byte[] _data;
    }
}
