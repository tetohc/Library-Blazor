using System.ComponentModel.DataAnnotations;

namespace Library.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
