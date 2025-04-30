using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs.UserDTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class UserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserData> _logger;

        public UserData(ApplicationDbContext context, ILogger<UserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los UserData almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<UserDTO>> GetAllAsyncSQL()
        {
            string query = @"
                SELECT 
                    u.Id, 
                    u.Username, 
                    u.Active AS Status, 
                    u.PersonId, 
                    p.Name + ' ' + p.LastName as PersonName
                FROM [User] u
                INNER JOIN Person p 
                ON u.PersonId = p.Id 
                WHERE u.Active = 1;";

            return await _context.QueryAsync<UserDTO>(query);
        }

        /// <summary>
        /// Obtiene todos los UserData almacenados en la base de datos LINQ
        /// </summary>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }



        /// <summary>
        /// Obtiene un UserData especifico por su identificacion SQL
        /// </summary
        public async Task<UserDTO?> GetByIdAsyncSQL(int id)

        {
            try
            {
                string query = @"
                    SELECT 
                        u.Id, 
                        u.Username, 
                        u.Active as Status, 
                        u.PersonId, 
                        p.Name + ' ' + p.LastName as PersonName
                    FROM [User] u
                    INNER JOIN Person p 
                    ON u.PersonId = p.Id
                    WHERE u.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<UserDTO>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un User con ID {UserId}", id);
                throw;
            }

        }

        /// <summary>
        /// Obtiene un UserData especifico por su identificacion LINQ
        /// </summary
        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<User>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener una usuario con ID {UserId}", id);
                throw;
            }

        }



        /// <summary>
        /// Crear un nuevo UserData en la base de datos SQL
        /// </summary>
        public async Task<User> CreateAsyncSQL(User user)
        {
            try
            {
                string query = @"
                    INSERT INTO [User] 
                    (Username, Password, PersonId) 
                    VALUES 
                    (@Username, @Password, @PersonId);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    user.Username,
                    user.Password,
                    user.PersonId
                });

                user.Id = newId;
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crear un nuevo UserData en la base de datos LINQ
        /// </summary>
        public async Task<User> CreateAsync(User user)
        {
            try
            {
                await _context.Set<User>().AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualiza un UserData existente en la base de datos SQL
        /// </summary>
        public async Task<bool> UpdateAsyncSQL(User user)
        {
            try
            {
                string query = @"
                    UPDATE [User] 
                    SET Username = @Username, Password = @Password, Active = @Active, PersonId = @PersonId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    user.Id,
                    user.Username,
                    user.Password,
                    user.Active,
                    user.PersonId
                });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario : {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Actualiza un UserData existente en la base de datos LINQ
        /// </summary>
        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                _context.Set<User>().Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario : {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un UserData de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try { 
                string query = @"
                    DELETE FROM [User] WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario {ex.Message}");
                return false;
            }
        } 
        
        /// <summary>
        /// Elimina un UserData de la base de datos SQL LINQ
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try 
            { 
                var user = await _context.Set<User>().FindAsync(id);
                if (user == null)
                    return false;
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un User de manera logica de la base  de datos SQL
        /// </summary>
        public async Task<bool> DeleteLogicAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    UPDATE [User] 
                    SET Active = 0
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar logicamente user: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un FormData de manera logica de la base  de datos LINQ
        /// </summary>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var user = await GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                user.Active = false;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar de manera logica el User: {ex.Message}");
                return false;
            }
        }
    }
}
