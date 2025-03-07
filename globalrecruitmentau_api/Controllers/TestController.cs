using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using globalrecruitmentau_api.Data;
using globalrecruitmentau_api.Models;
using Microsoft.EntityFrameworkCore;

namespace globalrecruitmentau_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController (ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/test - List all samples
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Sample>>> GetSamples()
        {
            var samples = await _context.Sample.ToListAsync();
            return Ok(samples);
        }

        // GET: api/<ValuesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2", "hot reload" };
        //}

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public string Get(int id)
        {
            return $"You requested working na ayos delay TETE: {id}";

        }

        // POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        [HttpPost]
        public async Task<ActionResult<Sample>> CreateSample([FromBody] Sample sample)
        {
            if(sample == null || string.IsNullOrEmpty(sample.Name))
            {
                return BadRequest("Name is required");
            }

            //check if existing 
            bool ifDataExist = await _context.Sample.AnyAsync(data => data.Name == sample.Name);
            if (ifDataExist)
            {
                return Conflict($"{sample.Name} already exists");
            }

            _context.Sample.Add(sample);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSamples), new { id = sample.id }, sample);
        }

        //// PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSample (int id, [FromBody] Sample sample)
        {
            if (sample == null || string.IsNullOrEmpty(sample.Name))
            {
                return BadRequest("Name is required");
            }

  
            var existingData = await _context.Sample.FindAsync(id);

            if(existingData == null)
            {
                return NotFound();
            }

            existingData.Name = sample.Name;
            _context.Sample.Update(existingData);
            await _context.SaveChangesAsync();

            return Ok("Updated Successfully");
        }



        //// DELETE api/<ValuesController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            var sample = await _context.Sample.FindAsync(id);
            if(sample == null)
            {
                return NotFound();
            }

            _context.Sample.Remove(sample);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }
        
    }
}
