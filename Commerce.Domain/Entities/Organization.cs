namespace Commerce.Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string OrganizationNo { get; set; } = null!;

        public string VatNo { get; set; } = null!;
    }
}