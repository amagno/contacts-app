using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ContactsApp.API.ViewModels;
using ContactsApp.Application.Exceptions;
using ContactsApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.API.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IContactRepository _contactRepository;
        public ContactsController(IContactService contactService, IContactRepository contactRepository)
        {
            _contactService = contactService;
            _contactRepository = contactRepository;
        }
     
        /// <summary>
        /// Gera contatos aleartoriamente. (Máximo 400)
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("generate/{qtd?}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GenerateContacts([FromRoute] int qtd = 1)
        {
            if (qtd > 10)
            {
                return BadRequest("Quantity not excced 10");
            }
            for (int i = 0; i < qtd; i++)
            {
                var faker = new Faker();
                try
                {
                    await _contactService.CreateNewContact(
                        faker.Name.FirstName(),
                        faker.Name.LastName(),
                        faker.Internet.Email(),
                        faker.Date.Past(faker.Random.Int(8, 80)),
                        faker.PickRandom(new [] { "f", "m"}),
                        false,
                        faker.Company.CompanyName(),
                        faker.Internet.Avatar(),
                        faker.Address.FullAddress(),
                        faker.Phone.PhoneNumber("(##) #####-####"),
                        faker.Random.Words(faker.Random.Int(0, 100))
                    );
                }
                catch (BadRequestException e)
                {
                    return BadRequest(e.Message);
                }
                
            }

            return await _contactRepository.GetAll();
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
            return await _contactRepository.GetAll(limit, skip);
        }
        /// <summary>
        /// Obter um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{contactId:int}")]
        public async Task<ActionResult<Contact>> GetContact([FromRoute] int contactId)
        {
            var contact =  await _contactRepository.GetById(contactId);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        /// <summary>
        /// Criar um novo contato.
        /// </summary>
        /// <param name="model">Dados</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> AddContact([FromBody] CreateContactViewModel model)
        {
            try
            {
                await _contactService.CreateNewContact(
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.Birthday,
                    model.Gender,
                    model.IsFavorite,
                    model.Company,
                    model.Avatar,
                    model.Address,
                    model.Phone,
                    model.Comments
                );
    
                return StatusCode(201);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Atualizar um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        /// <param name="model">Dados</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpPut("{contactId:int}")]
        public async Task<ActionResult<Contact>> UpdateContact([FromRoute] int contactId, [FromBody] EditContactViewModel model)
        {
            try
            {
                await _contactService.EditContact(
                    contactId,
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.Birthday,
                    model.Gender,
                    model.IsFavorite,
                    model.Company,
                    model.Avatar,
                    model.Address,
                    model.Phone,
                    model.Comments
                );

                return Ok();
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }

        }
        /// <summary>
        /// Remove um contato por id.
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpDelete("{contactId:int}")]
        public async Task<ActionResult> RemoveContact([FromRoute] int contactId)
        {
            try
            {
                await _contactService.DeleteContact(contactId);
                return Ok();
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
