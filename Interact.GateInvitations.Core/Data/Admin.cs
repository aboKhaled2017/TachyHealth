using Interact.GateInvitations.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Data
{
    public class Admin:BaseEntity<Guid>
    {
        public Admin()
        {
           
        }
        public Admin(string username, string password)
        {
            User = new User();
            User.Username = username;
            User.Password = password;
            
            User.UserType = UserType.Admin;
            Id = User.Id;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        [ForeignKey(nameof(Id))]
        public User User { get; set; }
        public string Name { get; set; }
    }
}
