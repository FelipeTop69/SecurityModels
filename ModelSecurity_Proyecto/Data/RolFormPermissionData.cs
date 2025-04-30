using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs.RolFormPermissionDTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class RolFormPermissionData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolFormPermissionData> _logger;

        public RolFormPermissionData(ApplicationDbContext context, ILogger<RolFormPermissionData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los RolFormPermission almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<RolFormPermissionDTO>> GetAllAsyncSQL()
        {
            string query = @"
                SELECT 
                    rfp.Id, 
                    rfp.Active as Status,
                    rfp.RolId, 
                    r.Name as RolName, 
                    rfp.PermissionId, 
                    p.Name as PermissionName, 
                    rfp.FormId, 
                    f.Name as FormName
                FROM RolFormPermission rfp
                INNER JOIN Rol r 
                ON rfp.RolId = r.Id
                INNER JOIN Permission p 
                ON rfp.PermissionId = p.Id
                INNER JOIN Form f 
                ON rfp.FormId = f.Id
                WHERE rfp.Active = 1";

            return (IEnumerable<RolFormPermissionDTO>) await _context.QueryAsync<RolFormPermissionDTO>(query);
        }

        /// <summary>
        /// Obtiene todos los RolFormPermission almacenados en la base de datos LINQ
        /// </summary>
        public async Task<IEnumerable<RolFormPermission>> GetAllAsync()
        {
            return await _context.Set<RolFormPermission>().ToListAsync();
        }



        /// <summary>
        /// Obtiene un RolFormPermission especifico por su identificacion SQL
        /// </summary
        public async Task<RolFormPermissionDTO?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        rfp.Id, 
                        rfp.Active as Status,
                        rfp.RolId, 
                        r.Name AS RolName, 
                        rfp.PermissionId, 
                        p.Name AS PermissionName, 
                        rfp.FormId, 
                        f.Name AS FormName
                    FROM RolFormPermission rfp
                    INNER JOIN Rol r ON rfp.RolId = r.Id
                    INNER JOIN Permission p ON rfp.PermissionId = p.Id
                    INNER JOIN Form f ON rfp.FormId = f.Id
                    WHERE rfp.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<RolFormPermissionDTO>(query, new { Id = id });
                //return await _context.Set<RolFormPermission>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el RolFormPermission con ID {id}");
                throw;
            }

        }

        /// <summary>
        /// Obtiene un RolFormPermission específico por su identificación LINQ
        /// </summary>
        public async Task<RolFormPermission?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolFormPermission>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un RolFormPermission con ID {RolFormPermissionId}", id);
                throw;
            }
        }



        /// <summary>
        /// Crear un nuevo RolFormPermission en la base de datos SQL
        /// </summary>
        public async Task<RolFormPermission> CreateAsyncSQL(RolFormPermission rolFormPermission)
        {
            try
            {
                string query = @"
                    INSERT INTO RolFormPermission (RolId, FormId, PermissionId) 
                    VALUES (@RolId, @FormId, @PermissionId);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    rolFormPermission.RolId,
                    rolFormPermission.FormId,
                    rolFormPermission.PermissionId
                });

                rolFormPermission.Id = newId;
                return rolFormPermission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolFormPermission: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crea un nuevo RolFormPermission en la base de datos LINQ
        /// </summary>
        public async Task<RolFormPermission> CreateAsync(RolFormPermission rolFormPermission)
        {
            try
            {
                await _context.Set<RolFormPermission>().AddAsync(rolFormPermission);
                await _context.SaveChangesAsync();
                return rolFormPermission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolFormPermission: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualiza un RolFormPermission existente en la base de datos SQL
        /// </summary>
        public async Task<bool> UpdateAsyncSQL(RolFormPermission rolFormPermission)
        {
            try
            {
                string query = @"
                    UPDATE RolFormPermission 
                    SET RolId = @RolId, @Active, FormId = @FormId, PermissionId = @PermissionId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    rolFormPermission.Id,
                    rolFormPermission.Active,
                    rolFormPermission.RolId,
                    rolFormPermission.FormId,
                    rolFormPermission.PermissionId
                });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolFormPermission : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza un RolFormPermission existente en la base de datos LINQ
        /// </summary>
        public async Task<bool> UpdateAsync(RolFormPermission rolFormPermission)
        {
            try
            {
                _context.Set<RolFormPermission>().Update(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolFormPermission: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un RolFormPermission de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM RolFormPermission WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolFormPermission {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un RolFormPermission de la base de datos LINQ
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolFormPermission = await _context.Set<RolFormPermission>().FindAsync(id);
                if (rolFormPermission == null)
                    return false;

                _context.Set<RolFormPermission>().Remove(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolFormPermission: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un RolFormPermission de manera logica de la base  de datos SQL
        /// </summary>
        public async Task<bool> DeleteLogicAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    UPDATE RolFormPermission
                    SET Active = 0
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar logicamente RolFormPermission: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un FormModuleData de manera logica de la base  de datos LINQ
        /// </summary>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var rolFormPermission = await GetByIdAsync(id);
                if (rolFormPermission == null)
                {
                    return false;
                }

                rolFormPermission.Active = false;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar de manera logica el RolFormPermission: {ex.Message}");
                return false;
            }
        }
    }
}
