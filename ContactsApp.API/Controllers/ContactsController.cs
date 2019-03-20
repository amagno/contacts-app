using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ContactsAPI.Models;
using ContactsAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsDbContext _db;
        public ContactsController(ContactsDbContext db)
        {
            _db = db;
        }
     
        /// <summary>
        /// Gera contatos aleartoriamente. (Máximo 400)
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("generate/{qtd?}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GenerateContacts([FromRoute] int qtd = 1)
        {
            
            var all = await _db.Contacts.ToListAsync();

            if (all.Count > 400)
            {
                return BadRequest("reached the maximum number of contacts");
            }

            var fakeInfo = new Faker<ContactInfo>()
                .RuleFor(c => c.Avatar, (f, c) => f.Internet.Avatar())
                .RuleFor(c => c.Company, (f, c) => f.Company.CompanyName())
                .RuleFor(c => c.Address, (f, c) => f.Address.FullAddress())
                .RuleFor(c => c.Phone, (f, c) => f.Phone.PhoneNumber("(##) #####-####"))
                .RuleFor(c => c.Comments, (f, c) => f.Random.Words(f.Random.Int(0, 100)));


            var fakeContact = new Faker<Contact>()
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
                .RuleFor(c => c.LastName, (f, c) => f.Name.FirstName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email())
                .RuleFor(c => c.Gender, (f, c) => f.PickRandom(new [] { "f", "m"}))
                .RuleFor(c => c.IsFavorite, (f, c) => f.Random.Bool())
                .RuleFor(c => c.Info, (f, c) => fakeInfo.Generate());

            var contacts = fakeContact.Generate(qtd);
            await _db.Contacts.AddRangeAsync(contacts);
            await _db.SaveChangesAsync();

            return contacts;
        }
        /// <summary>
        /// Obter todos os contatos.
        /// </summary>
        /// <param name="limit">Limite de resultado (items por página)</param>
        /// <param name="skip">Pular resultados</param>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts([FromQuery] int? limit, [FromQuery] int? skip)
        {
            var query = _db.Contacts.Include(c => c.Info);

            if (limit != null && skip == null)
            {
                return await query
                .Take((int)limit)
                .ToListAsync();
            }
            if (limit != null && skip != null)
            {
                return await query
                .Skip((int)skip)
                .Take((int)limit)
                .ToListAsync();
            }
            return await query
                .ToListAsync();
        }
        /// <summary>
        /// Obter um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [HttpGet("{contactId:int}")]
        public async Task<ActionResult<Contact>> GetContact([FromRoute] int contactId)
        {
            return await _db.Contacts
                .Where(c => c.Id == contactId)
                .Include(c => c.Info)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Criar um novo contato.
        /// </summary>
        /// <param name="model">Dados</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddContact([FromBody] ContactViewModel model)
        {
            var exists = await _db.Contacts.FirstOrDefaultAsync(c => c.Email == model.Email);

            if (exists != null)
            {
                return BadRequest(new {
                    Email = new List<string> { $"E-mail: {model.Email} already exists" }
                });
            }
            

            var contact = new Contact(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Gender,
                model.IsFavorite,
                model.Company,
                VerifyUrlIsValid(model.Avatar) ? model.Avatar : null,
                model.Address,
                model.Phone,
                model.Comments
            );
          
            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();

            return StatusCode(201);
        }
        /// <summary>
        /// Atualizar um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        /// <param name="model">Dados</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut("{contactId:int}")]
        public async Task<ActionResult<Contact>> UpdateContact([FromRoute] int contactId, [FromBody] ContactViewModel model)
        {
            var contact = await _db
                .Contacts
                .Include(c => c.Info)
                .FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact == null)
            {
                return BadRequest($"ContactId: {contactId} not exists");
            }

            if (contact.Email != model.Email)
            {
                var verifyEmail = await _db.Contacts.FirstOrDefaultAsync(c => c.Email == model.Email);

                if (verifyEmail != null)
                {
                    return BadRequest(new {
                        Email = new List<string> { $"E-mail: {model.Email} already exists" }
                    });
                }
            }
            
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Gender = model.Gender;
            contact.IsFavorite = model.IsFavorite;

            contact.Info.Address = model.Address;
            contact.Info.Avatar = VerifyUrlIsValid(model.Avatar) ? model.Avatar : contact.DefaultAvatar;
            contact.Info.Company = model.Company;
            contact.Info.Comments = model.Comments;
            contact.Info.Phone = model.Phone;
      
            _db.Contacts.Update(contact);
            await _db.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// Remove um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete("{contactId:int}")]
        public async Task<ActionResult> RemoveContact([FromRoute] int contactId)
        {
            var contact = await _db.Contacts.FindAsync(contactId);

            if (contact == null)
            {
                return BadRequest($"ContactId: {contactId} not exists");
            }

            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();

            return Ok();
        }
        private bool VerifyUrlIsValid(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out result);
        }
    }
}
