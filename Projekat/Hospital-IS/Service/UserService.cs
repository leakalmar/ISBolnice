using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService
    {
        public List<User> AllUsers { get; set; }

        private static UserService instance = null;
        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
        }

        private UserService()
        {

        }
    }
}
