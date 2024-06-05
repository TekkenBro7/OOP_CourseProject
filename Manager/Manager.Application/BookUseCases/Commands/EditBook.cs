using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication.BookUseCases.Commands
{
    public sealed record EditBookCommand(Book book) : IRequest<Book>
    { }
    public class AddAuthorCommandHandler : IRequestHandler<EditBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.UpdateAsync(request.book, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return request.book;
        }
    }
}
