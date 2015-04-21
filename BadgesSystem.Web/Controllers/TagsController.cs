using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using BadgesSystem.Models;
using BadgesSystem.Web.Infrastructure;

namespace BadgesSystem.Web.Controllers
{
    public class TagsController : BaseApiController
    {
        public async Task<IEnumerable<Tag>> Get()
        {
            return await this.Data.Tags.All().ToListAsync();
        }

        public async Task<Tag> Post(Tag tag)
        {
            var currentUserName = HttpContext.Current.User.Identity.Name;
            var currentUser = await this.Data.Users.All()
                    .Where(x => x.UserName == currentUserName).FirstOrDefaultAsync();
            if (currentUser.UserRole == UserRole.Programmer || currentUser.UserRole == UserRole.Tester)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                tag.Name = tag.Name.Trim();
                var appTag = await this.Data.Tags.All().Where(t => t.Name == tag.Name).FirstOrDefaultAsync();

                if (appTag == null)
                {
                    this.Data.Tags.Add(tag);
                    await this.Data.SaveChangesAsync();

                    return tag;
                }
                else
                {
                    return new Tag();
                }
            }

            return null;
        }
    }
}
