using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class RoleExtaintions
    {
        public static IdentityRole ToModel (this AddRoleVeiwModel veiwModel)
        {
            return new IdentityRole
            {
                Name = veiwModel.Name,
            };
        }
    }
}
