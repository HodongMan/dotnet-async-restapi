using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApplication.Data;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskRepository _taskRepository;

        public TaskController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _taskRepository.GetAllTasksAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Task task)
        {
            var result = await _taskRepository.SaveAsync(task);

            return Ok(result);
        }
    }
}
