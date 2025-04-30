using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs.RolUserDTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class RolUserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolUserData> _logger;

        public RolUserData(ApplicationDbContext context, ILogger<RolUserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los RolUser almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<RolUserDTO>> GetAllAsyncSQL()
        {
            string query = @"
                SELECT 
                    ru.Id, 
                    ru.Active as Status,
                    ru.RoleId, 
                    r.Name AS RoleName, 
                    ru.UserId, 
                    u.UserName AS UserName
                FROM RolUser ru
                INNER JOIN Rol r 
                ON ru.RoleId = r.Id
                INNER JOIN [User] u 
                ON ru.UserId = u.Id
                WHERE ru.Active = 1";

            return (IEnumerable<RolUserDTO>) await _context.QueryAsync<RolUserDTO>(query);
            //return await _context.Set<RolUser>().ToListAsync(); 
        }

        /// <summary>
        /// Obtiene todos los RolUser almacenados en la base de datos LINQ
        /// </summary>
        public async Task<IEnumerable<RolUser>> GetAllAsync()
        {
            return await _context.Set<RolUser>().ToListAsync();
        }



        /// <summary>
        /// Obtiene un RolUser especifico por su identificacion SQL
        /// </summary
        public async Task<RolUserDTO?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = @"
                SELECT 
                    ru.Id, 
                    ru.Active as Status,
                    ru.RoleId, 
                    r.Name as RoleName, 
                    ru.UserId, 
                    u.UserName as UserName
                FROM RolUser ru
                INNER JOIN Rol r 
                ON ru.RoleId = r.Id
                INNER JOIN [User] u 
                ON ru.UserId = u.Id
                WHERE ru.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<RolUserDTO>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el RolUser con ID {id}");
                throw;
            }

        }

        /// <summary>
        /// Obtiene un RolUser específico por su identificación LINQ
        /// </summary>
        public async Task<RolUser?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolUser>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un RolUser con ID {RolUserId}", id);
                throw;
            }
        }



        /// <summary>
        /// Crear un nuevo RolUser en la base de datos SQL
        /// </summary>
        public async Task<RolUser> CreateAsyncSQL(RolUser rolUser)
        {
            try
            {
                string query = @"
                    INSERT INTO RolUser (UserId, RoleId) 
                    VALUES (@UserId, @RoleId);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    rolUser.UserId,
                    rolUser.RoleId
                });

                rolUser.Id = newId;
                return rolUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolUser: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crea un nuevo RolUser en la base de datos LINQ
        /// </summary>
        public async Task<RolUser> CreateAsync(RolUser rolUser)
        {
            try
            {
                await _context.Set<RolUser>().AddAsync(rolUser);
                await _context.SaveChangesAsync();
                return rolUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolUser: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualiza un RolUser existente en la base de datos SQL
        /// </summary>
        public async Task<bool> UpdateAsyncSQL(RolUser rolUser)
        {
            try
            {
                string query = @"
                    UPDATE RolUser 
                    SET Active = @Active, 
                        UserId = @UserId, 
                        RoleId = @RoleId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    rolUser.Id,
                    rolUser.Active,
                    rolUser.UserId,
                    rolUser.RoleId
                });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolUser : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza un RolUser existente en la base de datos LINQ
        /// </summary>
        public async Task<bool> UpdateAsync(RolUser rolUser)
        {
            try
            {
                _context.Set<RolUser>().Update(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolUser: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un RolUser de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM RolUser WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolUser {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un RolUser de la base de datos LINQ
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolUser = await _context.Set<RolUser>().FindAsync(id);
                if (rolUser == null)
                    return false;

                _context.Set<RolUser>().Remove(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolUser: {ex.Message}");
                return false;
            }
        }
        


        /// <summary>
        /// Elimina un RolUser de manera logica de la base  de datos SQL
        /// </summary>
        public async Task<bool> DeleteLogicAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    UPDATE RolUser 
                    SET Active = 0
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar logicamente RolUser: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un rolUser de manera logica de la base  de datos LINQ
        /// </summary>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var rolUser = await GetByIdAsync(id);
                if (rolUser == null)
                {
                    return false;
                }

                rolUser.Active = false;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar de manera logica el RolUser: {ex.Message}");
                return false;
            }
        }
    }
}
    