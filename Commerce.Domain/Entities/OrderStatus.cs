namespace Commerce.Domain.Entities
{
    public class OrderStatus
    {
        public OrderStatus()
        {

        }

        public OrderStatus(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
