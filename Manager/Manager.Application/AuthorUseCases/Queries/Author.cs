using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication.AuthorUseCases.Queries
{   
    public sealed record GetAllAuthorsRequest() : IRequest<IEnumerable<Author>>
    {
    }
    public class GetAllAuthorsRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllAuthorsRequest, IEnumerable<Author>>
    {
        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.AuthorRepository.ListAllAsync(cancellationToken);
        }
    }
}
