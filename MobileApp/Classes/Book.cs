using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Classes
{
    public class Book
    {
        private int _id;
        private string _title;
        private string _category;
        private string _description;
        private string _image;
        private string _authorName;
        private string _publisher;
        private int _status;

        public Book(int id, string title, string category, string description, string image, string authorName, string publisher, int status)
        {
            _id = id;
            _title = title;
            _category = category;
            _description = description;
            _image = image;
            _publisher = publisher;
            _status = status;
            _authorName = authorName;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Image { get { return _image; } set { _image = value; } }
        public string AuthorName { get { return _authorName; } set { _authorName = value; } }
        public string Publisher { get { return _publisher; } set { _publisher = value; } }
        public int Status { get { return _status; } set { _status = value; } }


    }
}
