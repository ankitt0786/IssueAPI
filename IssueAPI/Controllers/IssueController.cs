using IssueAPI.Data;
using IssueAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public readonly IssueContext _issueContext;
        public IssueController(IssueContext issueContext) 
        {
            _issueContext = issueContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _issueContext.Issues.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result= await _issueContext.Issues.FindAsync(id);
            if(result==null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Issue issue)
        {
            await _issueContext.AddAsync(issue);
            await _issueContext.SaveChangesAsync(); 
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Issue issue)
        {
            if(id!=issue.Id)
            {
                return BadRequest(id);
            }
            _issueContext.Entry(issue).State = EntityState.Modified;    
            await _issueContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var DeleteId = await _issueContext.Issues.FindAsync(id);
            if(DeleteId== null)
                return NotFound(id);

            _issueContext.Issues.Remove(DeleteId);
            await _issueContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
