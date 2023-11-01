using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoTODO.Core.Entities;
using MongoTODO.Core.Services;
using MongoTODO.Repository;
using System.Runtime.CompilerServices;

namespace MongoTODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> Index()
        {
            return Ok(await _todoService.GetList());
        }

        [HttpPost]
        public async Task<ActionResult<Response<TodoTask>>> Create(TodoTask todoTask)
        {
            var response = await _todoService.Insert(todoTask);
            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response<TodoTask>>> Edit(TodoTask todoTask)
        {
            var response = await _todoService.Update(todoTask);
            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response<TodoTask>>> Delete(TodoTask todoTask)
        {
            Response<TodoTask> response;
            if (string.IsNullOrEmpty(todoTask.Id))
            {
                response = new Response<TodoTask>();
                response.Object = todoTask;
                return BadRequest(response);
            }
            response = await _todoService.Delete(todoTask.Id);
            response.Object = todoTask;
            return Ok(response);
        }
    }
}
