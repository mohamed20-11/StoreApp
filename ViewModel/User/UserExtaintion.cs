using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModel
{
    public static class UserExtaintion
    {
        public static User ToModel(this UserSignUpViewModel viewModel)
        {
            return new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Picture = viewModel.Picture,
                PhoneNumber = viewModel.PhoneNumber,
                NationalID = viewModel.NationalID,

            };
        }
    }
}
