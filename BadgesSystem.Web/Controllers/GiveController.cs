using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using BadgesSystem.Models;
using BadgesSystem.Web.Models;
using BadgesSystem.Web.Services;

namespace BadgesSystem.Web.Controllers
{
    public class GiveController : BaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<BadgesGift>> Get()
        {
            return await this.Data.BadgesGifts.All().ToListAsync();
        }

        [HttpPost]
        public async Task<UserBadgeViewModel> Post(UserBadgeViewModel userBadgeVM)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = HttpContext.Current.User.Identity.Name;
                var data = this.Data;
                var currentUser = await data.Users.All()
                    .Where(x => x.UserName == currentUserName)
                    .FirstOrDefaultAsync();
                var recipient = await data.Users.All()
                    .Where(x => x.Name == userBadgeVM.UserName)
                    .FirstOrDefaultAsync();
                var userBadge = await (from ub in data.UserBadges.All()
                                       where ub.BadgesGift.Name == userBadgeVM.GiftsBadges
                                       select new
                                       {
                                           BadgeGift = ub.BadgesGift,
                                           Image = ub.BadgesGift.File,
                                           Tags = ub.BadgesGift.BadgeTags.Select(bg => bg.Tag).ToList()
                                       }).FirstOrDefaultAsync();

                var hasUserBadge = await this.Data.UserBadges.All()
                        .AnyAsync(ub => ub.RecipientID == recipient.ID && ub.BadgesGiftID == userBadge.BadgeGift.ID);

                if (hasUserBadge)
                {
                    return null;
                }

                var userBadgeGift = new UserBadge()
                {
                    BadgesGiftID = userBadge.BadgeGift.ID,
                    BadgesGift = userBadge.BadgeGift,
                    GiveDate = DateTime.Now,
                    Reason = userBadgeVM.Reason,
                    RecipientID = recipient.ID,
                    SenderID = currentUser.ID
                };

                data.UserBadges.Add(userBadgeGift);
                await data.SaveChangesAsync();

                if (recipient.Email != null)
                {
                    SmtpService.Send(recipient, userBadgeGift, currentUser, userBadge.Tags);
                }
                userBadgeVM.Tags = userBadge.Tags;
                userBadgeVM.File = userBadge.Image;
                userBadgeVM.FlagType = userBadge.BadgeGift.FlagType;

                return userBadgeVM;
            }

            return null;
        }
    }
}
