using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Hesira.Controllers;
using Hesira.Helpers.General;
using Hesira.Services.Services.Interfaces;

namespace Hesira.Areas.Doctor.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    [SessionState(SessionStateBehavior.ReadOnly)]
    [DoctorAccess]
    public partial class UsersController : Common.Controllers.UsersController
    {

        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IUserProfileService _userProfileService;


        public UsersController(IUserService userService, IAddressService addressService, IUserProfileService userProfileService) : base(userService, addressService, userProfileService)
        {
        }
    }
}