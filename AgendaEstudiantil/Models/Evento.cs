using System.ComponentModel.DataAnnotations;

namespace AgendaEstudiantil.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required] 
        public required string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public string? Descripcion { get; set; }
    }
}
