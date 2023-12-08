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
        private int _bookId;
        private int _userId;
        private string _bookTitle;
        private string _userName;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _isRented;

        public RentedBook(int id, int bookId, int userId, string bookTitle, string userName, DateTime startDate, DateTime endDate, bool isRented)
        {
            _id = id;
            _bookId = bookId;
            _userId = userId;
            _bookTitle = bookTitle;
            _userName = userName;
            _startDate = startDate;
            _endDate = endDate;
            _isRented = isRented;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public int BookId { get { return _bookId; } set { _bookId = value; } }
        public int UserId { get { return _userId; } set { _userId = value; } }
        public string BookTitle { get { return _bookTitle; } set { _bookTitle = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; } }
        public bool IsRented { get { return _isRented; } set { _isRented = value; } }

    }
}
