using System.Collections.Generic;
using casln.Application.TodoLists.Commands.CreateTodoList;
using casln.Application.TodoLists.Commands.DeleteTodoList;
using casln.Application.TodoLists.Commands.UpdateTodoList;
using casln.Application.TodoLists.Queries.ExportTodos;
using casln.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using casln.Application.TodoItems.Queries;

namespace casln.WebUI.Controllers
{
    [Authorize]
    public class TodoListsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }



        [HttpGet("Grid")]
        public async Task<ActionResult<IEnumerable<ItemsGridDto>>> GetGrid([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await (Mediator.Send(new GetItemsGrid()
            {
                PageSize = pageSize,
                PageIndex = pageIndex
            } )));
        }


    }
}
