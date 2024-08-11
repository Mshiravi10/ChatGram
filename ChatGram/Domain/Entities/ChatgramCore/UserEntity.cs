using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatgramCore
{
    public class UserEntity : IdentityUser
    {
        public Guid? ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
