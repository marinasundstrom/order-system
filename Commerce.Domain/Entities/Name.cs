using Microsoft.EntityFrameworkCore;

namespace Commerce.Domain.Entities
{
    [Owned]
    public class Name
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
