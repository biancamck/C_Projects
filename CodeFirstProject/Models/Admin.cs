using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstProject.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}