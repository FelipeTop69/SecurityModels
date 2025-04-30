using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    ///<summary>
    ///Repositorio encargador de la gestion de la entidad Form en la base de datos
    ///</summary>
    public class FormData : IData<Form>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormData> _logger;

        ///<summary>
        ///Constructor que recibe el contexto de la base de datos
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/> Para la conexion con la base de datos.</param> 
        public FormData(ApplicationDbContext context, ILogger<FormData> logger)
        {
            _context = context;
            _logger = logger;
        }
  

        /// <summary>
        /// Obtiene todos los Forms almacenados en la base de datos LINQ
        /// </summary>
        ///<returns>Lista de Forms</returns>
        public async Task<IEnumerable<Form>> GetAllAsync()
{
            return await _context.Set<Form>()
                         .Where(f => f.Active) 
                         .ToListAsync();
        }


        /// <summary>
        /// Obtiene un Form específico por su identificación LINQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>El Form Obtenido</returns>
        public async Task<Form?> GetByIdAsync(int id)
        {
            return await _context.Set<Form>().FindAsync(id);
        }


        /// <summary>
        /// Crea un nuevo Form en la base de datos LINQ
        /// </summary>
        /// <param name="form"></param>
        /// <returns>El Form Creado</returns>
        public async Task<Form> CreateAsync(Form form)
        {
            await _context.Set<Form>().AddAsync(form);
            await _context.SaveChangesAsync();
            return form;
        }


        /// <summary>
        /// Actualiza un Form existente en la base de datos LINQ
        /// </summary>
        /// <param name="form">Objeto con la información actualizada.</param>
        /// <returns>El Form Actualizado.</returns>
        public async Task<Form> UpdateAsync(Form form)
        {
            _context.Set<Form>().Update(form);
            await _context.SaveChangesAsync();
            return form;
        }


        /// <summary>
        /// Elimina un Form de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del Form a eliminar </param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.
        public async Task<bool> DeletePersistenceAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            if (form == null)
                return false;

            _context.Set<Form>().Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Elimina un Form de manera logica de la base de datos LINQ
        /// </summary>
        /// <param name="id">Identificador unico del Form a eliminar de manera logica</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            if (form == null)
                return false;

            form.Active = false; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
