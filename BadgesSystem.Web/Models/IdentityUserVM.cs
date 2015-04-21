using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadgesSystem.Web.Models
{
    public class IdentityUserVM : IdentityUser
    {
        public string FullName { get; set; }

        public bool IsActive { get; set; }
    }
}