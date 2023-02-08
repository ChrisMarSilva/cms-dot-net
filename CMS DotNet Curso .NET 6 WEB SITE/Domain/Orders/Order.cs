using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IWantApp.Domain.Orders;

public class Order : Entity
{
    public string ClientId { get; private set; }
    public List<Product> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }

    private Order() { }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        this.ClientId = clientId;
        this.Products = products;
        this.DeliveryAddress = deliveryAddress;
        this.CreatedBy = clientName;
        this.CreatedOn = DateTime.UtcNow;
        this.EditedBy = clientName;
        this.EditedOn = DateTime.UtcNow;

        // 00:00:00.1450955 : Sum()
        // 00:00:00.0650430 : Foreach()
        // 00:00:00.0690510 : For()

        this.Total = 0.0M;
        if (this.Products != null && this.Products.Any())
        {
            foreach (var item in this.Products)
            {
                this.Total += item.Price;
            }
        }

        //this.Total = this.Products.Sum(p => p.Price);
        //this.Total = this.Products.Select(p => p.Price).Sum();

        this.Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsTrue(Products != null && Products.Any(), "Products")
            .IsNotNullOrEmpty(DeliveryAddress, "DeliveryAddress");

        this.AddNotifications(contract);
    }

}
