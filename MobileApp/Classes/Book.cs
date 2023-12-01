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
        private int _authorId;
        private string _publisher;
        private int _status;
        private Author _author;

        public Book(int id, string title, string category, string description, string image, int authorId, string publisher, int status, Author author)
        {
            _id = id;
            _title = title;
            _category = category;
            _description = description;
            _image = image;
            _authorId = authorId;
            _publisher = publisher;
            _status = status;
            _author = author;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Image { get { return _image; } set { _image = value; } }
        public int AuthorId { get { return _authorId; } set { _authorId = value; } }
        public string Publisher { get { return _publisher; } set { _publisher = value; } }
        public int Status { get { return _status; } set { _status = value; } }
        public Author Author { get { return _author; } set { _author = value; } }

    }
}
