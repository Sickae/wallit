﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WallIT.Shared.Enums;

namespace WallIT.Logic.Identity
{
    public class AppIdentityUserClaim : IdentityUserClaim<int>
    {
        public AppIdentityUserClaim()
        { }

        public AppIdentityUserClaim(AppIdentityUser identityUser, Role role)
        {
            UserId = identityUser.Id;
            ClaimType = ClaimTypes.Role;
            ClaimValue = role.ToString();
        }
    }
}
