using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    /// <summary>
    ///  Servicio responsable de autenticar usuarios mediante sus credenciales.
    /// </summary>
    /// <param name="context"></param>
    public class LoginService(ContextDb context)
    {
        private readonly ContextDb _context = context;

        public bool IsAuthenticated { get; private set; } = false;

        /// <summary>
        /// Intenta autenticar al usuario.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña proporcionada por el usuario.</param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Trim() == email.Trim());

            if (user == null)
            {
                IsAuthenticated = false;
                return IsAuthenticated;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user!.Password))
                IsAuthenticated = true;
            else
                IsAuthenticated = false;
            return IsAuthenticated;
        }
    }
}