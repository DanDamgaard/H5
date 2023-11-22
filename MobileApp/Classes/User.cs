using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Classes
{
    public class User
    {
        private int _id;
        private string _name;
        private string _password;
        private string _email;
        private string _phone;
        private string _address;
        private int _role;

        public User(int id, string name, string password, string email, string phone, string address, int role)
        {
            _id = id;
            _name = name;
            _password = password;
            _email = email;
            _phone = phone;
            _address = address;
            _role = role;
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string Password { get { return _password; } }
        public string Email { get { return _email; } }
        public string Phone { get { return _phone; } }
        public string Address { get { return _address; } }
        public int Role { get { return _role;}
        }
    }
}
