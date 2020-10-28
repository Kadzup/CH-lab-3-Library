using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using MySql.Data.MySqlClient;

namespace Library
{
    public class DBManager
    {
        private static String Host = "localhost";
        private static String Database = "library";
        private static String User = "root";
        private static String Password = "";

        static readonly string[] Tables = new[] { "user", "book", "book_order" };
        private static MySqlConnection connection;
        private static void CreateConnection()
        {
            connection = new MySqlConnection($"Database={Database};Datasource={Host};User={User};Password={Password}");
        }
        private static DBManager instance;
        private DBManager() { }
        public static DBManager GetInstance()
        {
            if (instance == null)
                instance = new DBManager();
            return instance;
        }

        // get data by parameters
        public static User GetUserByTicket(String ticketNumber)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[0]} WHERE 'ticket_number'='{ticketNumber}'";
            var reader = command.ExecuteReader();

            try
            {
                User user = new User();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _firstName, String _seconName, String _thirdName, DateTime _birthDate, String _phone, String _homeAddress, String _ticketNumber
                    User u = new User(int.Parse(par[0]), par[1], par[2], par[3], DateTime.Parse(par[4]), par[5], par[6], par[7]);
                    user = u;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[0]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }
        public static User GetUserById(int id)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[0]} WHERE id={id}";
            var reader = command.ExecuteReader();

            try
            {
                User user = new User();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _firstName, String _seconName, String _thirdName, DateTime _birthDate, String _phone, String _homeAddress, String _ticketNumber
                    User u = new User(int.Parse(par[0]), par[1], par[2], par[3], DateTime.Parse(par[4]), par[5], par[6], par[7]);
                    user = u;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[0]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }
        public static Book GetBookById(int id)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[1]} WHERE id={id}";
            var reader = command.ExecuteReader();

            try
            {
                Book book = new Book();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _title, String _authorName, DateTime _issueDate, float _price
                    Book b = new Book(int.Parse(par[0]), par[1], par[2], DateTime.Parse(par[3]), float.Parse(par[4]));
                    book = b;
                }
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[1]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }
        public static Order GetOrderById(int id)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[2]} WHERE id={id}";
            var reader = command.ExecuteReader();

            try
            {
                Order order = new Order();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _title, String _authorName, DateTime _issueDate, float _price
                    Order o = new Order(int.Parse(par[0]), par[1], GetUserByTicket(par[1]), int.Parse(par[2]), GetBookById(int.Parse(par[2])), DateTime.Parse(par[3]), DateTime.Parse(par[4]));
                    order = o;
                }
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[1]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }

        // get all data
        public static List<User> GetUsers()
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[0]}";
            var reader = command.ExecuteReader();

            try
            {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _firstName, String _seconName, String _thirdName, DateTime _birthDate, String _phone, String _homeAddress, String _ticketNumber
                    User u = new User(int.Parse(par[0]), par[1], par[2], par[3], DateTime.Parse(par[4]), par[5], par[6], par[7]);
                    users.Add(u);
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[0]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }
        public static List<Book> GetBooks()
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[1]}";
            var reader = command.ExecuteReader();

            try
            {
                List<Book> books = new List<Book>();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _title, String _authorName, DateTime _issueDate, float _price
                    Book b = new Book(int.Parse(par[0]), par[1], par[2], DateTime.Parse(par[3]), float.Parse(par[4]));
                    books.Add(b);
                }
                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[1]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }
        public static List<Order> GetOrders()
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Tables[2]}";
            var reader = command.ExecuteReader();

            try
            {
                List<Order> orders = new List<Order>();
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    string str = values.Aggregate((v1, v2) => $"{v1} | {v2}").ToString();
                    Console.WriteLine(str);
                    string[] par = str.Split('|');
                    // int _id, String _userTicket, User _user, int _bookId, Book _book, DateTime _pickDate, DateTime _returnDate
                    Order o = new Order(int.Parse(par[0]), par[1], GetUserByTicket(par[1]), int.Parse(par[2]), GetBookById(int.Parse(par[2])), DateTime.Parse(par[3]), DateTime.Parse(par[4]));
                    orders.Add(o);
                }
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get data from table {Tables[2]}\nError details: {ex.Message}");
                return null;
            }
            finally
            {
                reader?.Close();
            }
        }

        public static void AddUser(User user)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
            }
            String sql;
            if (user.Id == 0)
            {
                sql = $"INSERT INTO {Tables[0]} (`first_name`, `second_name`, `third_name`, `birth_date`, `phone`, `home_address`, `ticket_number`) VALUES ('{user.FirstName}', '{user.SecondName}', '{user.ThirdName}', '{user.BirthDate.Date.ToString("yyyy-MM-dd")}', '{user.Phone}', '{user.HomeAddress}', '{user.TicketNumber}')";
            }
            else
            {
                sql = $"UPDATE {Tables[0]} SET `first_name`='{user.FirstName}', `second_name`='{user.SecondName}', `third_name`='{user.ThirdName}', `birth_date`='{user.BirthDate.Date.ToString("yyyy-MM-dd")}', `phone`='{user.Phone}', `home_address`='{user.HomeAddress}', `ticket_number`='{user.TicketNumber}' WHERE id={user.Id}";
            }
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
        public static void AddBook(Book book)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
            }

            String sql;
            if (book.Id == 0)
            {
                sql = $"INSERT INTO {Tables[1]} (`title`, `author_name`, `date_issue`, `price`) VALUES ('{book.Title}', '{book.AuthorName}', '{book.IssueDate.Date.ToString("yyyy-MM-dd")}', {book.Price})";
            }
            else
            {
                sql = $"UPDATE {Tables[1]} SET `title`='{book.Title}', `author_name`='{book.AuthorName}', `date_issue`='{book.IssueDate.Date.ToString("yyyy-MM-dd")}', `price`={book.Price} WHERE id={book.Id}";
            }
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
        public static void AddOrder(Order order)
        {
            CreateConnection();

            try
            {
                connection.Open();
                Console.WriteLine($"Connected: {connection.Ping()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't connect to database!\nError details: {ex.Message}");
            }

            String sql;
            if (order.Id == 0)
            {
                sql = $"INSERT INTO {Tables[2]} (`user_ticket`, `id_book`, `date_return`, `date_pick`) VALUES ('{order.UserTicket}', {order.BookId}, '{order.ReturnDate.Date.ToString("yyyy-MM-dd")}', '{order.PickDate.Date.ToString("yyyy-MM-dd")}')";
            }
            else
            {
                sql = $"UPDATE {Tables[2]} SET `user_ticket`='{order.UserTicket}', `id_book`={order.BookId}, `date_return`='{order.ReturnDate.Date.ToString("yyyy-MM-dd")}', `date_pick`='{order.PickDate.Date.ToString("yyyy-MM-dd")}' WHERE id={order.Id} )";
            }
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

    }
}
