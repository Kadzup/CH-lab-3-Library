using Org.BouncyCastle.Crypto.Tls;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Order
    {
        public Order() { }
        public Order(int _id, String _userTicket, User _user, int _bookId, Book _book, DateTime _pickDate, DateTime _returnDate)
        {
            Id = _id;
            UserTicket = _userTicket;
            User = _user;
            BookId = _bookId;
            Book = _book;
            PickDate = _pickDate;
            ReturnDate = _returnDate;
        }
        public Order(String _userTicket, User _user, int _bookId, Book _book, DateTime _pickDate, DateTime _returnDate)
        {
            Id = 0;
            UserTicket = _userTicket;
            User = _user;
            BookId = _bookId;
            Book = _book;
            PickDate = _pickDate;
            ReturnDate = _returnDate;
        }
        public int Id { get; set; }
        public String UserTicket { get; set; }
        [ForeignKey("UserTicket")]
        public User User { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public DateTime PickDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
