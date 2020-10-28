using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class User
    {
        public User() { }
        public User(int _id, String _firstName, String _seconName, String _thirdName, DateTime _birthDate, String _phone, String _homeAddress, String _ticketNumber)
        {
            Id = _id;
            FirstName = _firstName;
            SecondName = _seconName;
            ThirdName = _thirdName;
            BirthDate = _birthDate;
            Phone = _phone;
            HomeAddress = _homeAddress;
            TicketNumber = _ticketNumber;
        }
        public User(String _firstName, String _seconName, String _thirdName, DateTime _birthDate, String _phone, String _homeAddress, String _ticketNumber)
        {
            Id = 0;
            FirstName = _firstName;
            SecondName = _seconName;
            ThirdName = _thirdName;
            BirthDate = _birthDate;
            Phone = _phone;
            HomeAddress = _homeAddress;
            TicketNumber = _ticketNumber;
        }
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public String Phone { get; set; }
        public String HomeAddress { get; set; }
        public String TicketNumber { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
