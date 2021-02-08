using Commerce.Domain.Entities;

namespace Commerce.Application.Invoices
{
    public interface IDeliveryToInvoiceGenerator
    {
        Invoice GenerateInvoiceFromDelivery(Delivery delivery);
    }
}