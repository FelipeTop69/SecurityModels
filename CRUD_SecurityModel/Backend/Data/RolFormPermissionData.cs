using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargador de la gestion de la entidad RolFormPermission en la base de datos
    /// </summary>
    public class RolFormPermissionData : IData<RolFormPermission>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolFormPermissionData> _logger;

        ///<summary>
        ///Constructor que recibe el contexto de la base de datos
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/> Para la conexion con la base de datos.</param>
        public RolFormPermissionData(ApplicationDbContext context, ILogger<RolFormPermissionData> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene todos los RolFormPermissions almacenados en la base de datos LINQ
        /// </summary>
        ///<returns>Lista de RolFormPermissions</returns>
        public async Task<IEnumerable<RolFormPermission>> GetAllAsync()
        {
            return await _context.Set<RolFormPermission>()
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .Where(rfp => rfp.Active)
                .ToListAsync();
        }


        /// <summary>
        /// Obtiene un RolFormPermission específico por su identificación LINQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>El RolFormPermission Obtenido</returns>
        public async Task<RolFormPermission?> GetByIdAsync(int id)
        {
            return await _context.Set<RolFormPermission>()
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .FirstOrDefaultAsync(rfp => rfp.Id == id);
        }


        /// <summary>
        /// Crea un nuevo RolFormPermission en la base de datos LINQ
        /// </summary>
        /// <param name="rolFormPermission"></param>
        /// <returns>El RolFormPermission Creado</returns>
        public async Task<RolFormPermission> CreateAsync(RolFormPermission rolFormPermission)
        {
            await _context.Set<RolFormPermission>().AddAsync(rolFormPermission);
            await _context.SaveChangesAsync();
            return rolFormPermission;
        }


        /// <summary>
        /// Actualiza un RolFormPermission existente en la base de datos LINQ
        /// </summary>
        /// <param name="rolFormPermission">Objeto con la informacion actualizada.</param>
        /// <returns>El RolFormPermission Actualizado.</returns>
        public async Task<RolFormPermission> UpdateAsync(RolFormPermission rolFormPermission)
        {

            _context.Set<RolFormPermission>().Update(rolFormPermission);
            await _context.SaveChangesAsync();
            return rolFormPermission;

        }


        /// <summary>
        /// Elimina un RolFormPermission de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del RolFormPermission a eliminar </param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.
        public async Task<bool> DeletePersistenceAsync(int id)
        {
            var rolFormPermission = await _context.Set<RolFormPermission>().FindAsync(id);
            if (rolFormPermission == null)
                return false;

            _context.Set<RolFormPermission>().Remove(rolFormPermission);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Elimina un RolFormPermission de manera logica de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del RolFormPermission a eliminar de manera logica</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            var rolFormPermission = await _context.Set<RolFormPermission>().FindAsync(id);
            if (rolFormPermission == null)
                return false;

            rolFormPermission.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}