using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadgesSystem.Models;

namespace BadgesSystem.Web.Models
{
    public class UserBadgeViewModel
    {
        public UserBadgeViewModel()
        {

        }

        public UserBadgeViewModel(string name, string reason, int badgesGiftID)
        {
            this.Name = name;
            this.Reason = reason;
            this.BadgesGiftID = badgesGiftID;
        }

        public string Name { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string GiftsBadges { get; set; }

        public int BadgesGiftID { get; set; }

        public File File { get; set; }

        public List<Tag> Tags { get; set; }

        public bool FlagType { get; set; }
    }
}