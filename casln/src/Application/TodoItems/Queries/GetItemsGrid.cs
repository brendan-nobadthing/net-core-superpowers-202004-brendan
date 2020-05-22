using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using casln.Application.Common.Interfaces;
using casln.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace casln.Application.TodoItems.Queries
{
    public class GetItemsGrid: IRequest<IEnumerable<ItemsGridDto>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }


    public class GetItemsGridHandler: IRequestHandler<GetItemsGrid, IEnumerable<ItemsGridDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetItemsGridHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemsGridDto>> Handle(GetItemsGrid request, CancellationToken cancellationToken)
        {

            var query = _context.TodoItems.AsQueryable();
            query = ApplyPaging(query, request);

            return await query.Select(i => new ItemsGridDto()
            {
                ItemId = i.Id,
                ListId = i.ListId,
                Title = i.Title,
                Done = i.Done,
                ListName = i.List.Title
            }).ToListAsync(cancellationToken);
        }

        private IQueryable<TodoItem> ApplyPaging(IQueryable<TodoItem> query, GetItemsGrid request)
        {
            if (request.PageSize > 0)
            {
                return query.Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .OrderBy(x => x.Created);
            }

            return query;
        }
    }


    public class ItemsGridDto {

        public int ItemId { get; set; }
        public int ListId { get; set; }

        public string Title { get; set; }

        public bool  Done { get; set; }

        public string ListName { get; set; }
    }


}
