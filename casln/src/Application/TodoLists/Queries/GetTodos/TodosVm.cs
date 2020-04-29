using System.Collections.Generic;

namespace casln.Application.TodoLists.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; set; }

        public IList<TodoListDto> Lists { get; set; }
    }
}
