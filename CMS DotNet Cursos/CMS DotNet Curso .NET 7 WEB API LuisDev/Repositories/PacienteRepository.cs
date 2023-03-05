using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Repositories.Interfaces;

namespace AwesomeDevEvents.API.Repositories
{
    public class PacienteRepository : BaseRepository, IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        //{
        //    return await _context.Pacientes
        //        .Select(x => new PacienteDto { Id = x.Id, Nome = x.Nome })
        //        .ToListAsync();
        //}

        //public async Task<Paciente> GetPacientesByIdAsync(int id)
        //{
        //    return await _context.Pacientes.Include(x => x.Consultas)
        //                 .ThenInclude(c => c.Especialidade)
        //                 .ThenInclude(c => c.Profissionais)
        //                .Where(x => x.Id == id).FirstOrDefaultAsync();
        //}
    }
}
