namespace Commerce.Application.Common.Models
{
    public class PatchOperation
    {
        public string Op { get; set; } = null!;

        public string Path { get; set; } = null!;

        public object Value { get; set; } = null!;
    }
}
