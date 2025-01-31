using BudgetTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace BudgetTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;


        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetTests")]
        public ActionResult<Test> GetTests()
        {
            using (var db = new TestContext())
            {
                Console.WriteLine($"DB path = {db.dbPath}");
                var results = db.Test.ToList();
                

                foreach (var test in results)
                {
                    Console.WriteLine($"ID={test.testId} ; Name={test.name} ; Value={test.value} ; Type={test.type}");
                }
                
                return Ok(results);
            }
        }

        [HttpGet("GetTestById/{idTest}")]
        public ActionResult<Test> GetTestById(int idTest)
        {
            using (var db = new TestContext())
            {
                Console.WriteLine($"DB path = {db.dbPath}");
                var result = db.Test.Find(idTest);
                if (result == null)
                {
                    return NotFound();
                }
                Console.WriteLine($"ID={result.testId} ; Name={result.name} ; Value={result.value} ; Type={result.type}");
                return Ok(result);
            }
        }

        [HttpPost("AddTest")]
        public ActionResult<Test> AddTest(TestCreateDto newTestDto)
        {
            try
            {
                using (var db = new TestContext())
                {
                    var newTest = new Test
                    {
                        name = newTestDto.name,
                        value = newTestDto.value,
                        type = newTestDto.type,
                    };

                    var addedTest = db.Test.Add(newTest);
                    Console.WriteLine(addedTest.Entity);
                    db.SaveChanges();
                    return CreatedAtAction(nameof(GetTestById), new { idTest = addedTest.Entity.testId }, addedTest.Entity);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error adding test", details = e.Message });
            }
            
        }

        [HttpPut("UpdateTest")]
        public ActionResult<Test> UpdateTest(Test testToUpdate)
        {
            try
            {
                using (var db = new TestContext())
                {
                    var updatedTest = db.Test.Update(testToUpdate);
                    db.SaveChanges();
                    return Ok(updatedTest.Entity);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error updating test", detail = e.Message });
            }
        }

        [HttpDelete("DeleteTest/{idTest}")]
        public ActionResult<Test> DeleteTest(int idTest)
        {
            try
            {
                Test testToRemove = new Test()
                {
                    testId = idTest
                };

                using (var db = new TestContext())
                {
                    
                    var removedTest = db.Test.Remove(testToRemove); 
                    db.SaveChanges();
                    return Ok(removedTest.Entity);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new {message = "Error deleting test", detail = e.Message});
            }
        }
    }
}
