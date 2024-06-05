using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Aplication
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            // Создать базу данных заново
            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();
            // Добавить авторов
            IReadOnlyList<Author> authors = new List<Author>()
            {
                new Author { Name = "Лев Толстой", Country = "Россия", Id = 1 },
                new Author { Name = "Якуб Колос", Country = "Беларусь", Id = 2 },
                new Author { Name = "Уильям Шекспир", Country = "Англия", Id = 3 },
            };
            foreach (var author in authors)
                await unitOfWork.AuthorRepository.AddAsync(author);
            await unitOfWork.SaveAllAsync();
            IReadOnlyList<Book> books = new List<Book>()
            {
                new Book { Id = 1, Name = "Война и мир", Year = 1867, Genre = "Роман", AuthorId = 1, Rating = 5.0},
                new Book { Id = 2, Name = "Воскресенье", Year = 1899, Genre = "Роман", AuthorId = 1, Rating = 4.9 },
                new Book { Id = 3, Name = "Анна Карелина", Year = 1878, Genre = "Роман", AuthorId = 1, Rating = 4.8 },
                new Book { Id = 4, Name = "После бала", Year = 1911, Genre = "Фикшн", AuthorId = 1, Rating = 4.7 },
                new Book { Id = 5, Name = "Смерть Ивана Ильича", Year = 1886, Genre = "Новелла", AuthorId = 1, Rating = 4.6 },

                new Book { Id = 6, Name = "Новая земля", Year = 1955, Genre = "Поэма", AuthorId = 2, Rating = 4.5},
                new Book { Id = 7, Name = "На росстанях", Year = 1955, Genre = "Роман", AuthorId = 2, Rating = 4.4},
                new Book { Id = 8, Name = "Сымон-музыка", Year = 1925, Genre = "Поэзия", AuthorId = 2, Rating = 4.3},
                new Book { Id = 9, Name = "Трясина", Year = 1965, Genre = "Проза", AuthorId = 2, Rating = 4.2},
                new Book { Id = 10, Name = "В глубине Полесья", Year = 1927, Genre = "Роман", AuthorId = 2, Rating = 4.1},

                new Book { Id = 11, Name = "Гамлет", Year = 1603, Genre = "Трагедия", AuthorId = 3, Rating = 4.0},
                new Book { Id = 12, Name = "Ромео и Джульетта", Year = 1597, Genre = "Трагедия", AuthorId = 3, Rating = 3.9},
                new Book { Id = 13, Name = "Отелло", Year = 1603, Genre = "Трагедия", AuthorId = 3, Rating = 3.8},
                new Book { Id = 14, Name = "Макбет", Year = 1606, Genre = "Трагедия", AuthorId = 3, Rating = 3.7},
                new Book { Id = 15, Name = "Король Лир", Year = 1606, Genre = "Трагедия", AuthorId = 3, Rating = 3.6}
            };
            foreach (var book in books)
                await unitOfWork.BookRepository.AddAsync(book);
            await unitOfWork.SaveAllAsync();
        }
    }
}
