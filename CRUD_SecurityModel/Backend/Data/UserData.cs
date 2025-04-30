using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargador de la gestion de la entidad User en la base de datos
    /// </summary>
    public class UserData : IData<User>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserData> _logger;

        ///<summary>
        ///Constructor que recibe el contexto de la base de datos
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/> Para la conexion con la base de datos.</param>
        public UserData(ApplicationDbContext context, ILogger<UserData> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene todos los Users almacenados en la base de datos LINQ
        /// </summary>
        ///<returns>Lista de Users</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>()
                .Where(user => user.Active)
                .Include(user => user.Person)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsyncJWT(bool includeInactive = false)
        {
            var query = _context.Set<User>().Include(u => u.Person).AsQueryable();

            if (!includeInactive)
                query = query.Where(user => user.Active);

            return await query.ToListAsync();
        }


        /// <summary>
        /// Obtiene un User específico por su identificación LINQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>El User Obtenido</returns>
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Set<User>()
                .Include(user => user.Person)
                .FirstOrDefaultAsync(user => user.Id == id);
        }


        /// <summary>
        /// Crea un nuevo User en la base de datos LINQ
        /// </summary>
        /// <param name="user"></param>
        /// <returns>El User Creado</returns>
        public async Task<User> CreateAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }


        /// <summary>
        /// Actualiza un User existente en la base de datos LINQ
        /// </summary>
        /// <param name="user">Objeto con la informacion actualizada.</param>
        /// <returns>El User Actualizado.</returns>
        public async Task<User> UpdateAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


        /// <summary>
        /// Elimina un User de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del User a eliminar </param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.
        public async Task<bool> DeletePersistenceAsync(int id)
        {
            var user = await _context.Set<User>().FindAsync(id);
            if (user == null)
                return false;
            
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Elimina un User de manera logica de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del User a eliminar de manera logica</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            var user = await _context.Set<User>().FindAsync(id);
            if (user == null)
                return false;

            user.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
