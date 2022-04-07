using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        TestContext db;

        public TestController(TestContext context)
        {
            db = context;
            if (!db.Tests.Any())
            {
                db.Tests.Add(new Test
                {
                    ID = 1,
                    Name = "Br",
                    Other = "One"
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> Get()
        {
            return await db.Tests.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> Get(int id)
        {
            Test test = await db.Tests.FirstOrDefaultAsync(x => x.ID == id);
            if (test == null)
                return NotFound();
            return new ObjectResult(test);
        }

    }
}
