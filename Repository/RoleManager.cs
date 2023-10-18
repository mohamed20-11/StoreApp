using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class RoleManager:MainManager<IdentityRole>
    {

        Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        public RoleManager(MyDBContext myDBContext,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager
            )
            :base(myDBContext) 
        {
            roleManager = _roleManager;
        }


        public async Task<IdentityResult> Add(AddRoleVeiwModel veiwModel)
        {
           return await roleManager.CreateAsync(veiwModel.ToModel());
        }

    }
}
