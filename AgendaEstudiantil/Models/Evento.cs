using System.ComponentModel.DataAnnotations;

namespace AgendaEstudiantil.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")] 
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public string? Descripcion { get; set; }
    }
}
