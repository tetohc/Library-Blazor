using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    /// <summary>
    /// Clase encargada de poblar la base de datos con datos iniciales.
    /// </summary>
    /// <param name="context"></param>
    public class SeedDB(ContextDb context)
    {
        private readonly ContextDb _context = context;

        /// <summary>
        /// Ejecuta el proceso de inicialización de la base de datos.
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckUserAsync(name: "UserTest", email: "usertest@email.com", password: "User.Test", role: "Administrado");
        }

        /// <summary>
        /// Verifica si existen categorías en la base de datos; si no, crea algunas por defecto.
        /// </summary>
        /// <returns></returns>
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Category.Any())
            {
                _context.Category.Add(new Category { Id = Guid.NewGuid(), Name = "Cuento" });
                _context.Category.Add(new Category { Id = Guid.NewGuid(), Name = "Novela" });
                _context.Category.Add(new Category { Id = Guid.NewGuid(), Name = "Ensayo" });
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el correo especificado. Si no existe, lo crea y lo guarda.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="email">orreo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario (texto plano, será cifrada).</param>
        /// <param name="role">Rol del usuario.</param>
        /// <returns>El usuario existente o el nuevo usuario creado.</returns>
        private async Task<User> CheckUserAsync(string name, string email, string password, string role)
        {
            var userOnDatabase = await _context.User.FirstOrDefaultAsync(x => x.Email.Trim() == email.Trim());
            if (userOnDatabase != null)
            {
                return userOnDatabase!;
            }

            User user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = role
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}