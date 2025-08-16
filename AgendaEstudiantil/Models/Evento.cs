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
        [FechaNoPasada(ErrorMessage = "La fecha no puede ser anterior a hoy.")]
        public DateTime Fecha { get; set; }

        public string? Descripcion { get; set; }

        public string? UserId { get; set; }

        public bool Completado { get; set; } = false; 
    }

    public class FechaNoPasadaAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime fecha)
            {
                return fecha.Date >= DateTime.Today;
            }
            return false;
        }
    }
}
