using Project.Database.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Database.Repositories;

internal partial class CommandRepository : BaseRepository, ICommandRepository
{
    public async Task<ProductModel> CreateProductAsync(ProductModel input, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(input, cancellationToken);

        // await _context.Set<ProductModel>().AddAsync(input, cancellationToken);

        return input;
    }

    public ProductModel UpdateProduct(ProductModel input, CancellationToken cancellationToken)
    {
        _context.Products.Update(input);

        //await _context.Set<ProductModel>()
        //    .Where(x => x.Id == input.ID)
        //    .ExecuteUpdateAsync(x => x.SetProperty(y => y.Name, input.Name));
        //    .ExecuteUpdateAsync(x => x.SetProperty(y => y.Price, input.Price));

        return input;
    }

    public bool DeleteProduct(ProductModel input, CancellationToken cancellationToken)
    {
        _context.Products.Remove(input);
        //_context.Remove(new ProductModel(Id: id));
        //await _context.Products.Where(c => c.Id == id).ExecuteDeleteAsync();

        const string sql = "DELETE FROM xxxxxx WHERE xxx = @xxx";
        //await _context.Database.GetDbConnection().ExecuteAsync(sql, new { input.Id }, transaction: Transaction.GetDbTransaction());

        return true;
    }

    //public async Task AddNumIdentcPagdrAceito(PagadorModel pagadorModel)
    //{
    //    ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");
    //    ArgumentNullException.ThrowIfNull(pagadorModel.PagadorAceito, "PagadorAceito != null");

    //    var parameters = new DynamicParameters();
    //    parameters.AddDynamicParams(new { pNumCtrlReq = pagadorModel.NumCtrlReq });
    //    parameters.AddDynamicParams(new { pNumIdentcPagdr = Convert.ToDecimal(pagadorModel.PagadorAceito!.NumIdentcPagdr) });
    //    parameters.AddDynamicParams(new { pNumRefAtlCadCliPagdr = Convert.ToDecimal(pagadorModel.PagadorAceito!.NumRefAtlCadCliPagdr) });

    //    if (pagadorModel.PagadorNumIdentcPagdr.DtHrIniCadCliPagdrDda.HasValue)
    //        parameters.AddDynamicParams(new { pDtHrIniCadCliPagdrDda = pagadorModel.PagadorNumIdentcPagdr.DtHrIniCadCliPagdrDda });

    //    if (pagadorModel.PagadorNumIdentcPagdr.DtHrFimCadCliPagdrDda.HasValue)
    //        parameters.AddDynamicParams(new { pDtHrFimCadCliPagdrDda = pagadorModel.PagadorNumIdentcPagdr.DtHrFimCadCliPagdrDda });

    //    await DataContext.Database.GetDbConnection().ExecuteAsync("PRJDNPC_PAG_PAGADOR_ACTO", parameters,
    //        commandType: CommandType.StoredProcedure, transaction: Transaction?.GetDbTransaction());
    //}
}
