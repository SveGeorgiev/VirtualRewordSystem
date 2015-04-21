using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using BadgesSystem.Models;
using BadgesSystem.Web.Infrastructure;
using BadgesSystem.Web.Services;

namespace BadgesSystem.Web.Controllers
{
    public class SaveController : BaseApiController
    {
        [HttpPost]
        public async Task<BadgesGift> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return null;
            }

            var provider = new MultipartFormDataMSP();

            await Request.Content.ReadAsMultipartAsync(provider);

            if (!provider.FileStreams.Any())
            {
                return null;
            }

            const string Badge = "Badge";
            var isGift = true;
            var name = provider.FormData["name"];
            var flagType = provider.FormData["FlagType"];
            var selectedTags = provider.FormData["selectedTags"];
            var tags = await this.Data.Tags.All().Where(x => selectedTags.Contains(x.Name)).ToListAsync();
            var currentUserName = HttpContext.Current.User.Identity.Name;
            var currentUser = await this.Data.Users.All()
            .Where(x => x.UserName == currentUserName).FirstOrDefaultAsync();
            var currentUserRole = currentUser.UserRole;

            if (flagType == Badge)
            {
                isGift = false;
            }

            if (currentUserRole == UserRole.Programmer || currentUserRole == UserRole.Tester)
            {
                isGift = true;
            }

            var service = new ImageService();
            var image = new BadgesSystem.Models.File();

            foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
            {
                image = service.RenderFile(file);
                image = service.ResizeFile(image);
            }

            var bdgTags = new List<BadgeTag>();

            foreach (var tagID in tags)
            {
                bdgTags.Add(new BadgeTag(tagID.ID));
            }

            var userBadges = new List<UserBadge>()
                        {
                            new UserBadge(DateTime.Now, currentUser.ID)
                        };

            var badgesGift = new BadgesGift()
            {
                Name = name,
                User = currentUser,
                FlagType = isGift,
                UserID = currentUser.ID,
                BadgeTags = bdgTags,
                File = image,
                UserBadges = userBadges,
            };

            var appBadgesGift = await this.Data.BadgesGifts.All()
                .Where(bg => bg.Name == badgesGift.Name)
                .FirstOrDefaultAsync();

            if (appBadgesGift == null)
            {
                this.Data.BadgesGifts.Add(badgesGift);
                await this.Data.SaveChangesAsync();

                return badgesGift;
            }

            return new BadgesGift();
        }
    }
}
