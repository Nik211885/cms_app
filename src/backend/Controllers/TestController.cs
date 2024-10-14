using backend.Infrastructure.Data.DbContext.master;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public TestController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("test-mapping")]
        public IActionResult TestMapping()
        {
            var o1 = new Object1("Na", "nana@gmai.com", "121323");
            var o2 = new Object2("Ne", "ne@gmail.com");
            ObjectHelpers.Mapping(o2, o1, ignoreProperties: ["Email"]);
            return Ok(o1);
        }
        [HttpGet("test2")]
        public async Task<IActionResult> Test2()
        {
            Dictionary<int, Test1> test1 = new();
            var query = "SELECT test1.id, test1.name, test2.test1_id, test2.id, test2.email FROM test1 JOIN test2 ON test1.id = test2.test1_id";
            var result = await _unitOfWork.Repository._session.Connection.QueryAsync<Test1, Test2, Test1>(query, (t1, t2) =>
            {
                if (test1.TryGetValue(t1.id, out var exitingTest1))
                {
                    t1 = exitingTest1;
                }
                else
                {
                    test1.Add(t1.id, t1);
                }
                t1.test2.Add(t2);
                return t1;
            }, splitOn: "test1_id");
            return Ok(result);
        }
    }
    record Object1(string Name, string Email, String PhoneNumber);
    record Object2(string Name, string Email);
    class Test1
    {
        public int id { get; }
        public string name { get; set; }
        public List<Test2> test2 { get; set; } = [];
    }
    class Test2
    {
        public int id { get; set; }
        public string test1_id { get; set; }
        public Test1 test1 { get; set; }
    }
}
