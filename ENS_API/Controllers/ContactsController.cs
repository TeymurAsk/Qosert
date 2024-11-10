using ENS_API.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ENS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ENSDbContext _context;
        public ContactsController(ENSDbContext context)
        {

            _context = context;

        }
        // GET: api/<ContactsController>
        [HttpGet]
        public List<Contact> Get()
        {
            return _context.Contacts.ToList();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public Contact Get(string id)
        {
            return _context.Contacts.Find(Guid.Parse(id));
        }

        // POST api/<ContactsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _context.Contacts.Remove(_context.Contacts.Find(Guid.Parse(id)));
        }
    }
}
