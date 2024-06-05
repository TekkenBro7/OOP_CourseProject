using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication.BookUseCases.Commands
{
    public sealed record AddBookCommand(string Name, string Genre, int Year, double Rating, int? AuthorId) : IRequest<Book>
    { }
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(
        AddBookCommand request,
        CancellationToken cancellationToken)
        {
            Book newTrainee = new Book(
            request.Name,
            request.Rating,
            request.Genre,
            request.Year
           // request.AuthorId
           );
            if (request.AuthorId.HasValue)
            {
                newTrainee.AddToAuthor(request.AuthorId.Value);
            }
            await _unitOfWork.BookRepository.AddAsync(newTrainee, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return newTrainee;
        }
    }
}
