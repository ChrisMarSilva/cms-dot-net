namespace AwesomeDevEvents.Domain.Dtos
{
    public class PacienteDetalhesDto : PacienteDto
    {
        public string Email { get; set; }
        public string Celular { get; set; }
        // public List<ConsultaDto> Consultas { get; set; }
    }
}
