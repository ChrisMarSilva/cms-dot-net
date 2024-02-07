namespace CMS_DotNet_Teste_Object_Mapping.Models;

public class Customer
{
    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public List<Order> Orders { get; set; }
    public CustomerDemographic[] CustomerDemographics { get; set; }
}

public class Order
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public int? EmployeeID { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? ShipVia { get; set; }
    public decimal? Freight { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }
    public Employee Employee { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public Shipper Shipper { get; set; }
}

public class OrderDetail
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
    public Product Product { get; set; }
}

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
}

public class CustomerDemographic
{
    public string CustomerTypeID { get; set; }
    public string CustomerDesc { get; set; }
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Title { get; set; }
    public string TitleOfCourtesy { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string HomePhone { get; set; }
    public string Extension { get; set; }
    public byte[] Photo { get; set; }
    public string Notes { get; set; }
    public int? ReportsTo { get; set; }
    public string PhotoPath { get; set; }
    public Territory[] Territories { get; set; }
}

public class Territory
{
    public string TerritoryID { get; set; }
    public string TerritoryDescription { get; set; }
    public int RegionID { get; set; }
    public Region Region { get; set; }
}

public class Region
{
    public int RegionID { get; set; }
    public string RegionDescription { get; set; }
}

public class Shipper
{
    public int ShipperID { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
}

public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public byte[] Picture { get; set; }
}

public class Supplier
{
    public int SupplierID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string HomePage { get; set; }
}
