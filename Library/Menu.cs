namespace Library
{
    public static class Menu
    {
        public static void Start()
        {
            var books = new List<Book>();
            var users = new List<User>();
            var loans = new List<Loan>();
            var book = new Book(string.Empty, string.Empty, string.Empty, DateTime.Now.Year);
            var user = new User(string.Empty, string.Empty);
            var loan = new Loan(user.Id, book.Id, DateTime.Now);

            Console.WriteLine("Bem vindo ao sistema de gerenciamento de biblioteca!");
            while (true)
            {
                MainMenu();

                var option = Console.ReadLine();

                switch (option) 
                {
                    case "1":
                        var isBookRegistered = BookRegister(book, books);
                        Console.Clear();
                        if (isBookRegistered)
                            Console.WriteLine("Livro cadastrado com sucesso!");
                        else
                            Console.WriteLine("Ocorreu um erro ao tentar cadastrar o livro, tente novamente!");
                        break;

                    case "2":
                        book = SearchBook(books).FirstOrDefault();
                        Console.Clear();
                        if (book != null)
                        {
                            Console.WriteLine("Livro encontrado com sucesso!");
                            ShowBook(book);
                        }
                        else 
                            Console.WriteLine("Livro não encontrado!");
                        break;

                    case "3":
                        Console.WriteLine("Lista de livros cadastrados: ");
                        ShowBooks(books);
                        break;

                    case "4":
                        books = RemoveBook(books);
                        break;

                    case "5":
                        var isUserRegistered = UserRegister(user, users);
                        if (isUserRegistered)
                            Console.WriteLine("Usuário cadastrado com sucesso!");
                        else
                            Console.WriteLine("Ocorreu um erro ao tentar cadastrar o usuário, tente novamente!");
                        break;

                    case "6":
                        var isLoanRegistered = LoanRegister(loan,loans);
                        if (isLoanRegistered)
                            Console.WriteLine("Empréstimo cadastrado com sucesso!");
                        else
                            Console.WriteLine("Ocorreu um erro ao tentar cadastrar o empréstimo, tente novamente!");

                        break;

                    case "7":
                        ShowBookReturn(book, books, loans);
                        break;

                    case "8":
                        return;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida, tente novamente!");
                        break;
                }
            }
        }

        public static bool BookRegister(Book book, List<Book> books)
        {
            Console.WriteLine("Insira o título do livro:");
            var title = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Insira o nome do autor:");
            var author = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Insira o nome do ISBN:");
            var isbn = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Insira o ano do livro:");
            var year = Int32.TryParse(Console.ReadLine(), out int numb) ? numb : 1901;

            book.Id = Guid.NewGuid();
            book.Title = title;
            book.Author = author;
            book.ISBN = isbn;
            book.Year = year;
            book.Active = true;

            books.Add(book);

            if (books.Any())
                return true;
            return false;
        }

        public static List<Book> SearchBook(List<Book> books)
        {
            var book = new Book(String.Empty, string.Empty, string.Empty, 0001);
            var newBookList = new List<Book>();
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 - Consultar pelo título");
            Console.WriteLine("2 - Consultar pelo autor");
            Console.WriteLine("3 - Consultar pelo ano");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Digite o título do livro:");
                    var title = Console.ReadLine();
                    book = SearchByTitle(books, title);
                    if(book == null)
                    {
                        Console.WriteLine("Esse livro não está contido na biblioteca!");
                    }
                    else
                    {
                        newBookList.Clear();
                        newBookList.Add(book);
                        return newBookList;
                    }
                    break;

                case "2":
                    Console.WriteLine("Digite o nome do autor:");
                    var author = Console.ReadLine();
                    newBookList.Clear();
                    newBookList = SearchByAuthor(books, author);

                    if(newBookList != null || newBookList.Any())
                    {
                        Console.WriteLine($"Livros do autor {author} encontrados:\n");
                        foreach (var item in newBookList)
                        {
                            ShowBook(item);
                            Console.WriteLine("----------------------------------");
                        }
                    }
                    break;

                case "3":
                    Console.WriteLine("Digite o ano:");
                    var year = Int32.TryParse(Console.ReadLine(), out int yr) ? yr :0;
                    newBookList.Clear();
                    newBookList = SearchByYear(books, year);

                    if (newBookList != null || newBookList.Any())
                    {
                        Console.WriteLine($"Livros do ano {year} encontrados:\n");
                        foreach (var item in newBookList)
                        {
                            ShowBook(item);
                            Console.WriteLine("----------------------------------");
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            return newBookList;
        }

        public static List<Book> RemoveBook(List<Book> books)
        {
            var book = new Book(String.Empty, string.Empty, string.Empty, 0001);
            var newBookList = books;
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 - Remover pelo ID");
            Console.WriteLine("2 - Remover pelo título");
            Console.WriteLine("3 - Remover pelo autor");
            Console.WriteLine("4 - Remover pelo ano");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Digite o ID do livro:");
                    var idString = Console.ReadLine();
                    Guid.TryParse(idString, out Guid id);
                    book = SearchById(books, id);
                    if (book is null)
                    {
                        Console.WriteLine("Livro não encontrado no cadastro!");
                        break;
                    }

                    newBookList = RemoveBookById(books, id);

                    if (books.Count > newBookList.Count)
                    {
                        Console.WriteLine("Livro removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possível remover o livro!");
                        return newBookList;
                    }
                    break;
                case "2":
                    Console.WriteLine("Digite o título do livro:");
                    var title = Console.ReadLine();

                    book = SearchByTitle(books, title);
                    if (book is null)
                    {
                        Console.WriteLine("Livro não encontrado no cadastro!");
                        break;
                    }

                    newBookList = RemoveBookByTitle(books, title);

                    if (books.Count > newBookList.Count)
                    {
                        Console.WriteLine("Livro removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possível remover o livro!");
                        return newBookList;
                    }
                    break;

                case "3":
                    Console.WriteLine("Digite o nome do autor:");
                    var author = Console.ReadLine();
                    newBookList.Clear();
                    newBookList = SearchByAuthor(books, author);

                    if (!newBookList.Any())
                    {
                        Console.WriteLine("Nenhum livro encontrado no cadastro!");
                        break;
                    }
                    else
                    {
                        newBookList = books;
                        newBookList = RemoveBookByAuthor(books, author);

                        if (books.Count > newBookList.Count)
                        {
                            Console.WriteLine("Livro(s) removido(s) com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível remover o(s) livro(s)!");
                            return newBookList;
                        }
                    }
                   
                    break;

                case "4":
                    Console.WriteLine("Digite o ano:");
                    var year = Int32.TryParse(Console.ReadLine(), out int yr) ? yr : 0;
                    newBookList.Clear();
                    newBookList = SearchByYear(books, year);

                    if (!newBookList.Any())
                    {
                        Console.WriteLine("Nenhum livro encontrado no cadastro!");
                        break;
                    }
                    else
                    {
                        newBookList = books;
                        newBookList = RemoveBookByYear(books, year);

                        if (books.Count > newBookList.Count)
                        {
                            Console.WriteLine("Livro(s) removido(s) com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível remover o(s) livro(s)!");
                            return newBookList;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            return newBookList;
        }

        public static bool UserRegister(User user, List<User> users)
        {
            var isRegistred = users.Select(s => s.Id.Equals(user.Id)).FirstOrDefault();

            if (isRegistred)
                Console.WriteLine("Usuário já cadastrado!");
            else
            {
                Console.WriteLine("Insira o nome do usuário:");
                var name = Console.ReadLine() ?? String.Empty;
                Console.WriteLine("Insira o email do usuário:");
                var email = Console.ReadLine() ?? String.Empty;

                user = new User(name, email)
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                };

                users.Add(user);
                return true;
            }
            return false;
        }

        public static bool LoanRegister(Loan loan, List<Loan> loans)
        {
            var isRegistred = loans.Select(s => s.Id.Equals(loan.Id)).FirstOrDefault();

            if (isRegistred)
                Console.WriteLine("Empréstimo já cadastrado!");
            else
            {
                Console.WriteLine("Insira o ID do usuário:");
                var idUser = Console.ReadLine() ?? String.Empty;
                Console.WriteLine("Insira o ID do livro:");
                var idBook = Console.ReadLine() ?? String.Empty;
                var data = DateTimeOffset.Now.AddDays(7);

                loan = new Loan(
                    Guid.TryParse(idUser, out Guid userResult) ? userResult : Guid.Empty, 
                    Guid.TryParse(idBook, out Guid bookResult) ? bookResult : Guid.Empty, 
                    data)
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                };

                loans.Add(loan);
                return true;
            }
            return false;
        }

        #region Private methods

        #region Presentation

        private static void MainMenu()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 - Cadastrar um livro");
            Console.WriteLine("2 - Consultar um livro");
            Console.WriteLine("3 - Consultar todos os livros");
            Console.WriteLine("4 - Remover um livro");
            Console.WriteLine("5 - Cadastrar um usuário");
            Console.WriteLine("6 - Cadastrar um Empréstimo");
            Console.WriteLine("7 - Devolver um livro");
            Console.WriteLine("8 - Sair");
        }

        private static void ShowBook(Book book)
        {
            Console.WriteLine("ID = " + book.Id);
            Console.WriteLine("Título = " + book.Title);
            Console.WriteLine("Autor = " + book.Author);
            Console.WriteLine("ISBN = " + book.ISBN);
        }

        private static void ShowBooks(List<Book> books)
        {
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 - Listar os livros ativos");
            Console.WriteLine("2 - Listar todos os livros cadastrados");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    ShowAllActiveBooks(books);
                    break;
                case "2":
                    ShowAllBooks(books);
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

        private static void ShowAllBooks(List<Book> books)
        {
            if (books.Any())
            {
                foreach (var book in books)
                {
                    Console.WriteLine();
                    Console.WriteLine("ID = " + book.Id);
                    Console.WriteLine("Título = " + book.Title);
                    Console.WriteLine("Autor = " + book.Author);
                    Console.WriteLine("ISBN = " + book.ISBN);
                }
            }
            else
                Console.WriteLine("Não existem livros cadastrados!");
        }

        private static void ShowAllActiveBooks(List<Book> books)
        {
            books = books.Where(b => b.Active == true).ToList();
            if (books.Any())
            {
                foreach (var book in books)
                {
                    Console.WriteLine();
                    Console.WriteLine("ID = " + book.Id);
                    Console.WriteLine("Título = " + book.Title);
                    Console.WriteLine("Autor = " + book.Author);
                    Console.WriteLine("ISBN = " + book.ISBN);
                }
            }
            else
                Console.WriteLine("Não existem livros cadastrados!");
        }

        private static void ShowBookReturn(Book book, List<Book> books, List<Loan> loans)
        {
            Console.WriteLine("Insira o ID do livro:");
            var idBook = Console.ReadLine() ?? String.Empty;

            book = SearchById(books, Guid.TryParse(idBook, out Guid bookResult) ? bookResult : Guid.Empty);
            
            if(book is null)
                Console.WriteLine("Livro não encontrado na base!");
            
            var isReturned = BookReturn(book, loans);

            if(isReturned)
                Console.WriteLine("Livro devolvido com sucesso!");
            else
                Console.WriteLine("Houve um erro ao tentar devolver o livro!");
        }

        #endregion

        #region Data Access

        private static Book SearchById(List<Book> books, Guid id)
        {
            if (books.Any())
            {
                var book = books.Where(x => x.Id.Equals(id)).FirstOrDefault();
                return book;
            }
            else
                return null;
        }

        private static Book SearchByTitle(List<Book> books, string title)
        {
            if (books.Any())
            {
                var book = books.Where(x => x.Title.ToLower().Equals(title.ToLower())).FirstOrDefault();
                return book;
            }
            else
                return null;
        }

        private static List<Book> SearchByAuthor(List<Book> books, string author)
        {
            if (books.Any())
            {
                var bookList = books.Where(x => x.Author.ToLower().Equals(author.ToLower())).ToList();
                return bookList;
            }
            else
                return null;
        }

        private static List<Book> SearchByYear(List<Book> books, int year)
        {
            if (books.Any())
            {
                var bookList = books.Where(x => x.Year.ToString().ToLower().Equals(year.ToString().ToLower())).ToList();
                return bookList;
            }
            else
                return null;
        }

        private static List<Book> RemoveBookById(List<Book> books, Guid id)
        {
            foreach (var book in from book in books
                                 where book.Id.Equals(id)
                                 select book)
            {
                book.Active = false;
            }

            return books;
        }

        private static List<Book> RemoveBookByTitle(List<Book> books, string title)
        {
            foreach (var book in from book in books
                                 where book.Title.ToLower().Equals(title.ToLower())
                                 select book)
            {
                book.Active = false;
            }


            return books;
        }

        private static List<Book> RemoveBookByAuthor(List<Book> books, string author)
        {
            foreach (var book in from book in books
                                 where book.Author.ToLower().Equals(author.ToLower())
                                 select book)
            {
                book.Active = false;
            }

            return books;
            // return books = books.Where(b => b.Author.ToLower().Equals(author.ToLower())).ToList();
        }

        private static List<Book> RemoveBookByYear(List<Book> books, int year)
        {
            foreach (var book in from book in books
                                 where book.Year.Equals(year)
                                 select book)
            {
                book.Active = false;
            }

            return books;
            //return books = books.Where(b => b.Year.Equals(year)).ToList();
        }

        private static bool BookReturn(Book book, List<Loan> loans)
        {
            var isRegistred = loans.Select(s => s.BookId.Equals(book.Id)).FirstOrDefault();

            if (!isRegistred)
            {
                Console.WriteLine("Livro não encontrado na base!");
                return false;
            }
            else
            {
                loans.Where(loan => loan.BookId.Equals(book.Id))
                   .ToList()
                   .ForEach(loan => loan.Active = false);
                return true;
            }
        }

        #endregion
        public static bool ConfirmAction()
        {
            Console.WriteLine("Confirma essa ação?");
            Console.WriteLine("Digite S para SIM ou N para NÃO:");
            var option = Console.ReadLine();

            switch (option.ToLower())
            {
                case "s":
                    return true;
                    break;
                case "n":
                    return false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    return false;
                    break;
            }

            return false;
        }
       
        #endregion
    }
}