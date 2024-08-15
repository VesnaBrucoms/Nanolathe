using Lombok.NET;

namespace Nanolathe.Models.Text
{
    [AllArgsConstructor]
    [ToString]
    internal partial class Section
    {
        [Property]
        private readonly Dictionary<string, string> _variables;
        [Property]
        private readonly Dictionary<string, Section> _subsections;
        [Property]
        private string _name;
    }
}
