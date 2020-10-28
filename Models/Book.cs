using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public Book() { }
        public Book(int _id, String _title, String _authorName, DateTime _issueDate, float _price)
        {
            Id = _id;
            Title = _title;
            AuthorName = _authorName;
            IssueDate = _issueDate;
            Price = _price;
        }
        public Book(String _title, String _authorName, DateTime _issueDate, float _price)
        {
            Id = 0;
            Title = _title;
            AuthorName = _authorName;
            IssueDate = _issueDate;
            Price = _price;
        }
        public int Id { get; set; }
        public String Title { get; set; }
        public String AuthorName { get; set; }
        public DateTime IssueDate { get; set; }
        public float Price { get; set; }

        [InverseProperty("Book")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
