using System;
using Commerce.Domain.Entities;

namespace Commerce.Application.Orders
{
    public class BillingDetailsDto
    {
        public NameDto Name { get; set; } = null!;

        public AddressDto Address { get; set; } = null!;

        public BillingDetailsDto(BillingDetails details)
        {
            Name = new NameDto
            {
                FirstName = details.Name.FirstName,
                LastName = details.Name.LastName
            };
            Address = new AddressDto
            {
                Thoroughfare = details.Address.Thoroughfare,
                Premises = details.Address.Premises,
                SubPremises = details.Address.SubPremises,
                PostalCode = details.Address.PostalCode,
                Locality = details.Address.Locality,
                SubAdministrativeArea = details.Address.SubAdministrativeArea,
                AdministrativeArea = details.Address.AdministrativeArea,
                Country = details.Address.Country
            };
        }
    }

    public class DeliveryDetailsDto
    {
        public NameDto Name { get; set; } = null!;

        public AddressDto Address { get; set; } = null!;

        public DeliveryDetailsDto(DeliveryDetails details)
        {
            Name = new NameDto
            {
                FirstName = details.Name.FirstName,
                LastName = details.Name.LastName
            };
        }
    }

    public class NameDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }

    public class AddressDto
    {
        // Street
        public string Thoroughfare { get; set; } = null!;

        // Street number
        public string? Premises { get; set; } = null!;

        // Suite
        public string? SubPremises { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        // Town or City
        public string Locality { get; set; } = null!;

        // County
        public string SubAdministrativeArea { get; set; } = null!;

        // State
        public string AdministrativeArea { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
