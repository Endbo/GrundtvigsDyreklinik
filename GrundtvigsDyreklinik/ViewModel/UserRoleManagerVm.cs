using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrundtvigsDyreklinik.ViewModel
{
    public class UserRoleManagerVm
    {
        public List<string> UsersName { get; set; }
        public List<string> UsersEmailName { get; set; }
        public List<string> UsersRole { get; set; }
    }
}