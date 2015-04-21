using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using BadgesSystem.Models;
using BadgesSystem.Web.Infrastructure;

namespace BadgesSystem.Web.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<UserRole> GetUserRole()
        {
            var currentUserName = HttpContext.Current.User.Identity.Name;

            return await this.Data.Users.All()
                    .Where(x => x.UserName == currentUserName)
                    .Select(x => x.UserRole)
                    .FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersName()
        {
            var currentUserName = HttpContext.Current.User.Identity.Name;
            var usersName = await this.Data.Users.All().ToListAsync();
            var currentUser = await this.Data.Users.All()
                .FirstAsync(x => x.UserName == currentUserName);

            if (currentUser.UserRole.Equals(UserRole.Administrator) || currentUser.UserRole.Equals(UserRole.Manager))
            {
                usersName = usersName.Where(x => x.UserName != currentUser.UserName).ToList();
            }
            else
            {
                usersName = usersName.Where(x => x.UserName != currentUser.UserName)
                                   .Where(x => x.UserRole == UserRole.Programmer || x.UserRole == UserRole.Tester)
                                   .ToList();
            }

            return usersName;
        }

        [HttpGet]
        [AllowAnonymous]
        public bool IsAuthenticated()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            return isAuthenticated;
        }
    }
}
