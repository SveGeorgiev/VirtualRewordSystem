using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using BadgesSystem.Web.Infrastructure;

namespace BadgesSystem.Web.Controllers
{
    public class ViewController : BaseApiController
    {
        public async Task<IEnumerable<object>> GetGifts()
        {
            var currentUserName = HttpContext.Current.User.Identity.Name;
            var currentUser = await this.Data.Users.All()
                 .Where(x => x.UserName == currentUserName)
                 .FirstOrDefaultAsync();

            var gifts = await (from bg in this.Data.BadgesGifts.All()
                               where bg.FlagType && bg.UserBadges.Any(x => x.RecipientID == currentUser.ID)
                               select new
                               {
                                   ID = bg.ID,
                                   Name = bg.Name,
                                   Reason = bg.UserBadges.Where(x => x.RecipientID == currentUser.ID).FirstOrDefault().Reason,
                                   Tags = bg.BadgeTags.Select(x => x.Tag),
                                   File = bg.File
                               }).ToListAsync();

            return gifts;
        }

        public async Task<IEnumerable<object>> GetBadges()
        {
            var currentUserName = HttpContext.Current.User.Identity.Name;
            var currentUser = await this.Data.Users.All()
                 .Where(x => x.UserName == currentUserName)
                 .FirstOrDefaultAsync();

            var badges = await (from bg in this.Data.BadgesGifts.All()
                                where !bg.FlagType && bg.UserBadges.Any(ub => ub.RecipientID == currentUser.ID)
                                select new
                                {
                                    ID = bg.ID,
                                    Name = bg.Name,
                                    Reason = bg.UserBadges.Where(ub => ub.RecipientID == currentUser.ID).FirstOrDefault().Reason,
                                    Tags = bg.BadgeTags.Select(bt => bt.Tag),
                                    File = bg.File
                                }).ToListAsync();

            return badges;
        }
    }
}
