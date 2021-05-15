using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PhonebookApplication.Models;
using PhonebookApplication.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace PhonebookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly PhonebookRepository _phonebookRepository;
        public PhonebookController(ApplicationDbContext context)
        {
            _phonebookRepository = new PhonebookRepository(context);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets the records with sorting, filtering and paging")]
        public async Task<ActionResult<PhonebookResponseModel>> Gets(string sortOrder = null, string firstName = null, string city = null, string zipCode = null, string phoneNumber = null, int pageSize = 5, int pageNumber = 1)
        {
            var requestModel = new PhonebookRequestModel
            {
                SortOrder = sortOrder,
                FirstName = firstName,
                City = city,
                ZipCode = zipCode,
                PhoneNumber = phoneNumber,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            return await _phonebookRepository.GetsAsync(requestModel);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get the record by id")]
        public async Task<ActionResult<PhonebookRecordModel>> Get(int id)
        {
            var record = await _phonebookRepository.GetAsync(id);

            if (record == null)
                return NotFound();

            return record;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add the record")]
        public async Task<ActionResult<PhonebookRecordModel>> Post(PhonebookRecordModel record)
        {
            var res = await _phonebookRepository.AddAsync(record);
            if (res.Record == null)
                return ValidationProblem(JsonConvert.SerializeObject(res.ValidationErrors));
            return Ok(res.Record);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete the record by id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _phonebookRepository.DeleteAsync(id))
                return Ok("DELETED");

            return NotFound();
        }

    }
}
