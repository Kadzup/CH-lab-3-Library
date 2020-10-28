using Library.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
    public class HTMLBuilder
    {
        const string TemplatesPath = "./wwwroot/templates/";
        private static string GetTemplate(string filename)
        {
            return File.ReadAllText(Path.Combine(TemplatesPath, filename));
        }
        private static string RenderTemplate(string template, Dictionary<string, string> parameters)
        {
            return parameters.Aggregate(template, (temp, pair) => temp.Replace($"@{pair.Key}", pair.Value));
        }
        public static async Task WritePage(HttpContext httpContext, string title, string body)
        {
            string page = RenderTemplate(GetTemplate("index.html"), new Dictionary<string, string>()
            {
                { "title", title },
                { "body", body },
            });

            await httpContext.Response.WriteAsync(page);
        }

        public static async Task WriteUserssPage(HttpContext httpContext)
        {
            string userTemplate = GetTemplate("user_table_item.html");
            string userTable = string.Empty;

            List<User> users = DBManager.GetUsers();
            foreach (var user in users)
                {
                    userTable += RenderTemplate(userTemplate, new Dictionary<string, string>()
                    {
                        { "Id", user.Id.ToString() },
                        { "FirstName", user.FirstName },
                        { "LastName", user.SecondName },
                        { "ThirdName", user.ThirdName },
                        { "BirthDate", user.BirthDate.ToShortDateString() },
                        { "Phone", user.Phone },
                        { "HomeAddress", user.HomeAddress },
                        { "TicketNumber", user.TicketNumber },
                    });
                }
            

            string usersPage = RenderTemplate(GetTemplate("user_table.html"), new Dictionary<string, string>()
            {
                { "users", userTable }
            });

            await WritePage(httpContext, "Users", usersPage);
        }
        public static async Task WriteBooksPage(HttpContext httpContext)
        {
            string bookTemplate = GetTemplate("book_table_item.html");
            string bookTable = string.Empty;

            List<Book> books = DBManager.GetBooks();
            string usersTable = string.Empty;
            foreach (var book in books)
            {
                bookTable += RenderTemplate(bookTemplate, new Dictionary<string, string>()
                    {
                        { "Id", book.Id.ToString() },
                        { "Title", book.Title },
                        { "AuthorName", book.AuthorName },
                        { "IssueDate", book.IssueDate.ToShortDateString() },
                        { "Price", book.Price.ToString() },
                    });
            }


            string booksPage = RenderTemplate(GetTemplate("book_table.html"), new Dictionary<string, string>()
            {
                { "books", bookTable }
            });

            await WritePage(httpContext, "Books", booksPage);
        }
        public static async Task WriteOrdersPage(HttpContext httpContext)
        {
            string orderTemplate = GetTemplate("order_table_item.html");
            string orderTable = string.Empty;

            List<Order> orders = DBManager.GetOrders();
            foreach (var order in orders)
            {
                orderTable += RenderTemplate(orderTemplate, new Dictionary<string, string>()
                    {
                        { "Id", order.Id.ToString() },
                        { "UserTicket", order.UserTicket },
                        { "BookId", order.BookId.ToString() },
                        { "PickDate", order.PickDate.ToShortDateString() },
                        { "ReturnDate", order.ReturnDate.ToShortDateString()  },
                    });
            }


            string ordersPage = RenderTemplate(GetTemplate("order_table.html"), new Dictionary<string, string>()
            {
                { "orders", orderTable }
            });

            await WritePage(httpContext, "Orders", ordersPage);
        }
    }
}
