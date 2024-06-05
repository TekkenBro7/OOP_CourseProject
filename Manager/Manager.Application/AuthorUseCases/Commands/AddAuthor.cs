using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication.AuthorUseCases.Commands
{
    public sealed record AddAuthorCommand(string Name, string Country) : IRequest<Author>
    { }
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async System.Threading.Tasks.Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            Author newAuthor = new Author(
            request.Name,
            request.Country
           );
            await _unitOfWork.AuthorRepository.AddAsync(newAuthor, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return newAuthor;
        }
    }
}
