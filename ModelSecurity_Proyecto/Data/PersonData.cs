using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class PersonData
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<PersonData> _looger;

        public PersonData(ApplicationDbContext context, ILogger<PersonData> logger)
        {
            _context = context;
            _looger = logger;
        }

        /// <summary>
        /// Obtiene todos los Person almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<Person>> GetAllAsyncSQL()
        {
            string query = @"SELECT * FROM Person WHERE Active = 1;";

            return await _context.QueryAsync<Person>(query);
        }

        /// <summary>
        /// Obtiene todos los Person almacenados en la base de datos LINQ
        /// </summary>
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Set<Person>().ToListAsync();
        }



        /// <summary>
        /// Obtiene un Person especifico por su identificacion SQL
        /// </summary
        public async Task<Person?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = " SELECT * FROM Person WHERE Id = @Id ";

                return await _context.QueryFirstOrDefaultAsync<Person>(query, new { Id = id });
            }
            catch(Exception ex) {
                _looger.LogError(ex, $"Error al obtener la Person con id {id}");
                throw;
            }
        }
        
        /// <summary>
        /// Obtiene un Person especifico por su identificacion LINQ
        /// </summary
        public async Task<Person?> GetByIdAsync(int id)
        {
            try
            {

                return await _context.Set<Person>().FindAsync(id);
            }
            catch(Exception ex) {
                _looger.LogError(ex, $"Error al obtener Person con id {id}");
                throw;
            }
        }



        /// <summary>
        /// Crear un nuevo Person en la base de datos SQL
        /// </summary>
        public async Task<Person> CreateAsyncSQL(Person person)
        {
            try
            {
                string query = @"
                    INSERT INTO Person 
                    (Name, LastName, Email, DocumentNumber, Phone, 
                     Address, DocumentType, BlodType) 
                    VALUES 
                    (@Name, @LastName, @Email, @DocumentNumber, @Phone, 
                     @Address, @DocumentType, @BlodType);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    person.Name,
                    person.LastName,
                    person.Email,
                    person.DocumentNumber,
                    person.Phone,
                    person.Address,
                    person.DocumentType,
                    person.BlodType,
                });

                person.Id = newId;
                return person;
            }
            catch (Exception ex) 
            {
                _looger.LogError($"Error al crear Person{ex.Message}");
                throw;
            }

        }
        
        /// <summary>
        /// Crear un nuevo Person en la base de datos LINQ
        /// </summary>
        public async Task<Person> CreateAsync(Person person)
        {
            try
            {
                await _context.Set<Person>().AddAsync(person);
                await _context.SaveChangesAsync();
                return person;
            }
            catch (Exception ex) 
            {
                _looger.LogError($"Error al crear Person {ex.Message}");
                throw;
            }

        }



        /// <summary>
        /// Actualiza un Person existente en la base de datos SQL
        /// </summary>
        public async Task<bool> UpdateAsyncSQL(Person person)
        {
            try
            {
                string query = @"
                    UPDATE Person 
                    SET Name = @Name, LastName = @LastName, Email = @Email, DocumentNumber = @DocumentNumber, 
                        Phone = @Phone, Address = @Address, DocumentType = @DocumentType, BlodType = @BlodType,
                        Active = @Active
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    person.Id,
                    person.Name,
                    person.LastName,
                    person.Email,
                    person.DocumentNumber,
                    person.Phone,
                    person.Address,
                    person.DocumentType,
                    person.BlodType,
                    person.Active
                });

                return rowsAffected > 0;
            }
            catch (Exception ex) {
                _looger.LogError($"Error al actualizar Person {ex.Message}");
                throw;
            }
            
        } 

        /// <summary>
        /// Actualiza un Person existente en la base de datos LINQ
        /// </summary>
        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                _context.Set<Person>().Update(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                _looger.LogError($"Error al actualizar Person {ex.Message}");
                throw;
            }
            
        }



        /// <summary>
        /// Elimina un Person de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Person WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _looger.LogError($"Error al elimianar Person {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Elimina un Person de la base de datos LINQ
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var person = await _context.Set<Person>().FindAsync(id);
                if (person == null)
                    return false;
                _context.Set<Person>().Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _looger.LogError($"Error al eliminar Person {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un Person de manera logica de la base  de datos SQL
        /// </summary>
        public async Task<bool> DeleteLogicAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    UPDATE Person 
                    SET Active = 0
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar logicamente Person: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un Person de manera logica de la base  de datos LINQ
        /// </summary>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var person = await GetByIdAsync(id);
                if (person == null)
                {
                    return false;
                }

                person.Active = false;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _looger.LogError($"Error al eliminar de manera logica Person: {ex.Message}");
                return false;
            }
        }

    }
}
