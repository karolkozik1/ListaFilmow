using Api.Database;
using Api.Database.Entities;
using Api.Extensions;
using Api.Mapping;
using Common.CommonData;
using Common.Extensions;
using Common.Students;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Api.Students.GetStudentsPaged
{
    public class GetStudentsPagedQueryHandler(SanContext context) : IRequestHandler<GetStudentsPagedQuery, PagedList<StudentBasicDto>>
    {
        private readonly SanContext _context = context;

        public async Task<PagedList<StudentBasicDto>> Handle(GetStudentsPagedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Students.Include(s => s.Status) as IQueryable<Student>;

            if (!string.IsNullOrEmpty(request.Request.LastNameOrAlbum))
            {
                if (request.Request.LastNameOrAlbum.IsNumeric())
                    query = query.Where(s => s.AlbumNumber.StartsWith(request.Request.LastNameOrAlbum));
                else
                    query = query.Where(s => s.LastName.ToLower().StartsWith(request.Request.LastNameOrAlbum.ToLower()));
            }

            if (request.Request.HideInactive.HasValue && request.Request.HideInactive.Value)
                query = query.Where(s => s.StatusId == 1);

            var data = query
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ThenBy(s => s.AlbumNumber)
                .Select(s => s.ToStudentBasicDto());

            var result = await data.ToPagedListAsync(request.Request.PageNumber, request.Request.PageSize);
            return result;
        }
    }
}
