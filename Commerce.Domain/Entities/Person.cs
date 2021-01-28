namespace Commerce.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        //public DateTime? BirthDate { get; set; }

        public string Ssn { get; set; } = null!;

    }
}
