using Project.Domain.Dtos.Request;
using Project.Domain.Dtos.Response;
using Project.Domain.Models;
using Project.Service.Interfaces;

namespace Project.Service;

internal partial class Service : IService
{
    public async Task<ICollection<ProductResponseDto>> GetAllProductAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service.GetAllAsync");

        var models = await _queryRepository.FindAllProductAsync(cancellationToken);

        var response = models.Select(c => new ProductResponseDto(c.Id, c.Name, c.Price, c.CreatedAt, c.UpdatedAt)).ToArray();
        return response;
    }

    public async Task<ProductResponseDto?> GetByIdProductAsync(int id, CancellationToken cancellationToken)
    {
        var model = await _queryRepository.FindByIdProductAsync(id, cancellationToken);
        if (model == null)
            return null;

        var response = new ProductResponseDto(model.Id, model.Name, model.Price, model.CreatedAt, model.UpdatedAt);
        return response;

        //var commandResult = new CommandResult<object?>();
        //try
        //{
        //    commandResult.AddResult(response);
        //}
        //catch (Exception e)
        //{
        //    commandResult.AddError(e);
        //}
        //return commandResult;
    }

    public async Task<ProductResponseDto?> InsertProductAsync(ProductRequestDto request, CancellationToken cancellationToken)
    {
        var status = await _queryRepository.ExistByNameClientAsync(request.Name!, cancellationToken);
        if (status)
            return null;

        var model = new ProductModel(request.Name!, request.Price!.Value);

        var newModel = await _commandRepository.CreateProductAsync(model, cancellationToken);
        await _commandRepository.SaveChangesAsync(cancellationToken);

        var response = new ProductResponseDto(newModel.Id, newModel.Name, newModel.Price, newModel.CreatedAt, newModel.UpdatedAt);
        return response;
    }

    public async Task<ProductResponseDto?> UpdateProductAsync(int id, ProductRequestDto request, CancellationToken cancellationToken)
    {
        var model = await _queryRepository.FindByIdProductAsync(id, cancellationToken);
        if (model is null)
            return null;

        model.Update(name: request.Name!, price: request.Price!.Value);

        var newModel = _commandRepository.UpdateProduct(model, cancellationToken);
        if (newModel is null)
            return null;

        await _commandRepository.SaveChangesAsync(cancellationToken);

        var response = new ProductResponseDto(newModel.Id, newModel.Name, newModel.Price, newModel.CreatedAt, newModel.UpdatedAt);
        return response;
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var model = await _queryRepository.FindByIdProductAsync(id, cancellationToken);
            if (model is null)
                return false;

            var status = _commandRepository.DeleteProduct(model, cancellationToken);
            if (!status)
                return false;

            await _commandRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
