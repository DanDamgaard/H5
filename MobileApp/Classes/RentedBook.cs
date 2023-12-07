using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Classes
{
    public class RentedBook
    {
        private int _id;
        private int _userId;
        private int _bookId;
        private DateTime _startDate;
        private DateTime _endDate;
        private User _user;
        private Book _book;

        public RentedBook(int id, int userId, int bookId, DateTime startDate, DateTime endDate, User user, Book book)
        {
            _id = id;
            _userId = userId;
            _bookId = bookId;
            _startDate = startDate;
            _endDate = endDate;
            _user = user;
            _book = book;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public int UserId { get { return _userId; } set { _userId = value; } }
        public int Bookid { get { return _bookId; } set { _bookId = value; } }
        public DateTime StartDate { get { return _startDate;} set { _startDate = value; } }
        public DateTime EndDate { get { return _endDate;} set { _endDate = value; } }
        public User User { get { return _user; } set { _user = value; } }
        public Book Book { get { return _book; } set { _book = value; } }
    }
}
