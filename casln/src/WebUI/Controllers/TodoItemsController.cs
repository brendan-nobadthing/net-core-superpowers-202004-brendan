using casln.Application.TodoItems.Commands.CreateTodoItem;
using casln.Application.TodoItems.Commands.DeleteTodoItem;
using casln.Application.TodoItems.Commands.UpdateTodoItem;
using casln.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using casln.WebUI.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace casln.WebUI.Controllers
{
    [Authorize]
    public class TodoItemsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command, [FromServices] IHubContext<TodoHub> hubctx)

    {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            await hubctx.Clients.All.SendAsync("UpdateItem", command);



            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
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
            await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
