using Hashed_Id.Data;
using Hashed_Id.Data.Models;
using Hashed_Id.Models.Dtos;
using Hashed_Id.Models.ViewModels;
using Hashed_Id.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hashed_Id.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IIntIdHahser _intIdHasher;
        public MainController(IIntIdHahser intIdHasher, Context context)
        {
            _intIdHasher = intIdHasher;
            _dbContext = context;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddPersonDto addPersonDto)
        {
            Person person = new Person()
            {
                FirstName = addPersonDto.FirstName,
                LastName = addPersonDto.LastName,
                Id = _nextId()
            };
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();
            return Ok("new person added.");
        }

        [HttpGet("Get")]
        public IActionResult Get(string id)
        {
            Person? person = _dbContext.Persons.Find(_intIdHasher.Decode(id));
            if (person is null) return NotFound($"Person with id '{id}' not found.");
            var result = new PersonViewModelToShow()
            {
                Id = _intIdHasher.Code(person.Id),
                FullName = person.FullName
            };
            return Ok(result);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int? count = null)
        {
            IEnumerable<PersonViewModelToShow> persons = _dbContext.Persons.Select(p => new PersonViewModelToShow()
            {
                Id = _intIdHasher.Code(p.Id),
                FullName = p.FullName
            }).AsEnumerable();
            return Ok(persons.Take(count == null ? persons.Count() : count.Value));
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] UpdatePersonDto updatePersonDto)
        {
            var person = _dbContext.Persons.Find(_intIdHasher.Decode(updatePersonDto.Id));
            if (person is null) return NotFound($"Person with id '{updatePersonDto.Id}' not found.");
            person.FirstName = updatePersonDto.FirstName;
            person.LastName = updatePersonDto.LastName;
            _dbContext.SaveChanges();
            return Ok("Person Updated.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            var person = _dbContext.Persons.Find(_intIdHasher.Decode(id));
            if (person is null) return NotFound($"Person with id '{id}' not found.");
            _dbContext.Persons.Remove(person);
            _dbContext.SaveChanges();
            return Ok("Person Deleted.");
        }

        // private methods ---
        private int _nextId()
        {
            var personsCount = _dbContext.Persons.Count();
            return personsCount + 1;
        }


    }
}
