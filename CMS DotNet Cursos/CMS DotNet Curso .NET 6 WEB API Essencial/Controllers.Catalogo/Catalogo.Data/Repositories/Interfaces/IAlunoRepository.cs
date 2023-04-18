using Catalogo.Data.Pagination;
using Catalogo.Domain.Models;

namespace Catalogo.Data.Repositories.Interfaces;

public interface IAlunoRepository : IBaseRepository<Aluno>
{
    Task<PagedList<Aluno>> GetAlunosAsync(AlunosParameters alunoParams);
    Task<IEnumerable<Aluno>> GetAllAsync();
    //Task<Aluno> GetByIdAsync(Guid id);
    //Task<Aluno> AddAsync(Aluno input);
    //Aluno Update(Aluno input);
    //bool Remove(Aluno input);
}
