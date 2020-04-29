using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using casln.Application.TodoItems.Commands.UpdateTodoItem;
using Microsoft.AspNetCore.SignalR;

namespace casln.WebUI.SignalR
{
    public class TodoHub: Hub
    {

        public async Task UpdateItem(UpdateTodoItemCommand updateCommand)
        {
            await Clients.All.SendAsync("UpdateItem", updateCommand);
        }


    }
}
