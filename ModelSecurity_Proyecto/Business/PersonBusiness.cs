using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    public class PersonBusiness
    {
        private readonly PersonData _personData;
        private readonly ILogger<PersonBusiness> _logger;

        public PersonBusiness(PersonData personData, ILogger<PersonBusiness> logger)
        {
            _personData = personData;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las personas como DTOs
        /// </summary>
        public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
        {
            try
            {
                var persons = await _personData.GetAllAsyncSQL();
                return MapToDTOList(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Persons");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de Persons", ex);
            }
        }


        /// <summary>
        /// Obtiene una persona por su ID como DTO
        /// </summary>
        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un Person con ID inválido: {PersonId}", id);
                throw new ValidationException("id", "El ID de un Person debe ser mayor que cero");
            }

            var person = await _personData.GetByIdAsyncSQL(id);
            if (person == null)
            {
                _logger.LogInformation("No se encontró un Person con ID: {PersonId}", id);
                throw new EntityNotFoundException("Person", id);
            }

            try
            {
                return MapToDTO(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la persona con ID: {PersonId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar la persona con ID {id}", ex);
            }
        }


        /// <summary>
        /// Crea una nueva persona desde un DTO
        /// </summary>
        public async Task<PersonDTO> CreatePersonAsync(PersonDTO personDto)
        {
             ValidatePerson(personDto);

            try
            {
                var person = MapToEntity(personDto);

                var createdPerson = await _personData.CreateAsyncSQL(person);

                return MapToDTO(createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo Person: {PersonNombre}", personDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear Person", ex);
            }
        }


        /// <summary>
        /// Actualiza una persona existente
        /// </summary>
        public async Task<bool> UpdatePersonAsync(PersonDTO personDto)
        {
            if (personDto.Id <= 0)
            {
                _logger.LogWarning("Se intentó actualizar un Person con ID inválido: {PersonId}", personDto.Id);
                throw new ValidationException("id", "El ID de Person debe ser mayor que cero");
            }

            ValidatePerson(personDto);

            var person = await _personData.GetByIdAsyncSQL(personDto.Id);
            if (person == null)
            {
                _logger.LogWarning("No se encontró  Person con ID {PersonId} para actualizar", personDto.Id);
                throw new EntityNotFoundException("Person", personDto.Id);
            }

            try
            {
                person.Name = personDto.Name;
                person.LastName = personDto.LastName;
                person.Email = personDto.Email;
                person.DocumentType = personDto.DocumentType;
                person.DocumentNumber = personDto.DocumentNumber;
                person.Phone = personDto.Phone;
                person.Address = personDto.Address;
                person.BlodType = personDto.BlodType;
                person.Active = personDto.Status;

                return await _personData.UpdateAsyncSQL(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la persona con ID: {PersonId}", personDto.Id);
                throw new ExternalServiceException("Base de datos", $"Error al actualizar la persona con ID {personDto.Id}", ex);
            }
        }


        /// <summary>
        /// Elimina una persona por su ID
        /// </summary>
        public async Task<bool> DeletePersonAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó eliminar un Person con ID inválido: {PersonId}", id);
                throw new ValidationException("id", "El ID de Person debe ser mayor que cero");
            }

            var existingPerson = await _personData.GetByIdAsyncSQL(id);
            if (existingPerson == null)
            {
                throw new EntityNotFoundException("Person", id);
            }

            try
            {
                return await _personData.DeleteAsyncSQL(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar Person con ID: {PersonId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al eliminar Person con ID {id}", ex);
            }
        }


        /// <summary>
        /// Elimina un person de manera logica por ID
        /// </summary>
        public async Task<bool> DeletePersonLogicalAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("ID", "El ID de Person debe ser mayor que cero.");
            }

            var existingPerson = await _personData.GetByIdAsyncSQL(id);
            if (existingPerson == null)
            {
                throw new EntityNotFoundException("Person", id);
            }

            try
            {
                return await _personData.DeleteLogicAsyncSQL(id);

            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error en servicio externo al eliminar Person con ID: {PersonId}", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar Person de manera logica con ID: {PersonId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar Person de manera logica.", ex);
            }
        }


        /// <summary>
        /// Valida un objeto PersonDTO
        /// </summary>
        private void ValidatePerson(PersonDTO personDto)
        {
            if (personDto == null)
            {
                throw new ValidationException("El objeto Person no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(personDto.Name))
            {
                _logger.LogWarning("Se intentó crear/actualizar Person con Name vacío");
                throw new ValidationException("Name", "El nombre de la persona es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(personDto.Email))
            {
                _logger.LogWarning("Se intentó crear/actualizar Person con Email vacío");
                throw new ValidationException("Email", "El correo electrónico de Person es obligatorio");
            }
        }


        /// <summary>
        /// Mapea de Person a PersonDTO
        /// </summary>
        private PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                Email = person.Email,
                DocumentType = person.DocumentType,
                DocumentNumber = person.DocumentNumber,
                Phone = person.Phone,
                Address = person.Address,
                BlodType = person.BlodType,
                Status = person.Active,
            };
        }


        /// <summary>
        /// Mapea de PersonDTO a Person
        /// </summary>
        private Person MapToEntity(PersonDTO personDto)
        {
            return new Person
            {
                Id = personDto.Id,
                Name = personDto.Name,
                LastName = personDto.LastName,
                Email = personDto.Email,
                DocumentType = personDto.DocumentType,
                DocumentNumber = personDto.DocumentNumber,
                Phone = personDto.Phone,
                Address = personDto.Address,
                BlodType = personDto.BlodType,
                Active = personDto.Status,
            };
        }


        /// <summary>
        /// Mapea una lista de Person a una lista de PersonDTO
        /// </summary>
        /// <summary>
        /// Metodo para mapear una lista de Person a una lista de PersonDTO 
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        private IEnumerable<PersonDTO> MapToDTOList(IEnumerable<Person> persons)
        {
            var personsDTO = new List<PersonDTO>();
            foreach (var person in persons)
            {
                personsDTO.Add(MapToDTO(person));
            }
            return personsDTO;
        }
    }
}
