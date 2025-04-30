using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    ///<summary>
    ///Repositorio encargador de la gestion de la entidad Rol en la base de datos
    ///</summary>
    public  class RolData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolData> _logger;


        ///<summary>
        ///Constructor que recibe el contexto de la base de datos
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/> Para la conexion con la base de datos.</param>
        public RolData(ApplicationDbContext context, ILogger<RolData> logger)
        {
            _context = context;
            _logger = logger;
        }


        ///<summary>
        ///Obtiene todos los Rols almacenados en la base de datos SQL
        ///</summary>
        ///<returns>Lista de Rols</returns>
        public async Task<IEnumerable<Rol>> GetAllAsyncSQL()
        {
            string query = @"SELECT * FROM Rol WHERE Active = 1";
            return await _context.QueryAsync<Rol>(query);
        }

        /// <summary>
        /// Obtiene todos los Rols almacenados en la base de datos LINQ
        /// </summary>
        ///<returns>Lista de Rols</returns>
        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Set<Rol>()
             .Where(r => r.Active)
             .ToListAsync();
        }



        ///<summary>
        ///Obtiene un Rol especifico por su identificacion SQL
        ///</summary> 
        public async Task<Rol?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = @"SELECT * FROM Rol WHERE Id = @Id AND Active = 1";
                return await _context.QueryFirstOrDefaultAsync<Rol>(query, new { Id = id });
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "Error al obtener Rol con ID {RolId}", id);
                throw; ///Re-lanza la exepcion para que sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un Rol específico por su identificacion LINQ
        /// </summary>
        public async Task<Rol?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Rol>()
                    .AsNoTracking()
                    .Where(r => r.Id == id && r.Active)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener Rol con ID {RolId}", id);
                throw;
            }
        }



        ///<summary>
        ///Crear un nuevo Rol en la base de datos SQL
        ///</summary>
        ///<param name="rol">Instancia del Rol al crear</param>
        ///<returns>El Rol creado</returns>
        public async Task<Rol> CreateAsyncSQL(Rol rol)
        {
            try
            {
                string query = @"
                    INSERT INTO Rol (Name, Description, Active) 
                    VALUES (@Name, @Description, @Active);
                    SELECT CAST(SCOPE_IDENTITY() as int);"; 

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    rol.Name,
                    rol.Description,
                    rol.Active,
                });

                rol.Id = newId; 
                return rol; 
            }
            catch (Exception ex) {
                _logger.LogError($"Error al crear Rol: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crea un nuevo Rol en la base de datos LINQ
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>El Rol Creado</returns>
        public async Task<Rol> CreateAsync(Rol rol)
        {
            try
            {
                await _context.Set<Rol>().AddAsync(rol);
                await _context.SaveChangesAsync();
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear Rol: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualiza un Rol existente en la base de datos SQL
        /// </summary>
        /// <param name="rol">Objeto con la información actualizada.</param>
        /// <returns>True si la actualizacion fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsyncSQL(Rol rol)
        {
            try
            {
                string query = @"
                    UPDATE Rol 
                    SET Name = @Name, Active = @Active, Description = @Description
                    WHERE Id = @Id;
            
                    SELECT CAST(@@ROWCOUNT AS int);"; 

                int rowsAffected = await _context.QueryFirstOrDefaultAsync<int>(query, new
                {
                    rol.Id,
                    rol.Name,
                    rol.Active,
                    rol.Description
                });

                return rowsAffected > 0;
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Error al actualizar Rol : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza un Rol existente en la base de datos LINQ
        /// </summary>
        /// <param name="rol">Objeto con la información actualizada.</param>
        /// <returns>True si la actualizacion fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Rol rol)
        {
            try
            {
                _context.Set<Rol>().Update(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar Rol: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un Rol de la base de datos SQL
        /// </summary>
        /// <param name="id">Identificador unico del Rol a eliminar </param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Rol WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);"; 

                int rowsAffected = await _context.QueryFirstOrDefaultAsync<int>(query, new { Id = id });

                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar Rol {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un Rol de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del Rol a eliminar </param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rol = await _context.Set<Rol>().FindAsync(id);
                if (rol == null)
                    return false;

                _context.Set<Rol>().Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar Rol: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un Rol de manera logica un Rol de la base de datos SQL
        /// </summary>
        /// <param name="id">Identificador unico del Rol a eliminar de manera logica</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> SoftDeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    UPDATE Rol
                    SET Active = 0
                    WHERE Id = @Id";

                int rowsAffected = await _context.QueryFirstOrDefaultAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al elimianar de manera logica Rol {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un Rol de manera logica un Rol de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del Rol a eliminar de manera logica</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var rol = await _context.Set<Rol>().FindAsync(id);
            if (rol == null)
                return false;

            rol.Active = false; 
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
