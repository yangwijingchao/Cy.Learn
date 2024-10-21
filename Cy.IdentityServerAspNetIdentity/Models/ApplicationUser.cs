﻿using Microsoft.AspNetCore.Identity;

namespace Cy.IdentityServerAspNetIdentity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FavoriteColor { get; set; }
    }
}
