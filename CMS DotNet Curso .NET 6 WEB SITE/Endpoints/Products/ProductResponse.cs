namespace IWantApp.Endpoints.Products;

public record ProductResponse(Guid Id, string Name, string CategoryName, string Description, bool IsStock, decimal Price, bool Active);
