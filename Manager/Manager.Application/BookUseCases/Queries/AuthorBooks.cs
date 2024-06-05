using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication.BookUseCases.Queries
{
    public sealed record GetBooksByAuthorIdQuery(int AuthorId) : IRequest<IEnumerable<Book>>
    {

    }
    public class GetBooksByAuthorIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBooksByAuthorIdQuery, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.BookRepository.ListAsync(t => t.AuthorId.Equals(request.AuthorId), cancellationToken);
        }
    }
}
