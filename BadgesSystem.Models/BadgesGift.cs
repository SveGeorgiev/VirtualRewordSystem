namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BadgesGift
    {
        private ICollection<UserBadge> userBadges;
        private ICollection<BadgeTag> badgeTags;

        public BadgesGift()
        {
            this.badgeTags = new HashSet<BadgeTag>();
            this.userBadges = new HashSet<UserBadge>();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Name:")]
        [MaxLength(25)]
        public string Name { get; set; }
        public bool FlagType { get; set; }
        public int FileID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual File File { get; set; }

        public virtual ICollection<BadgeTag> BadgeTags
        {
            get { return badgeTags; }
            set { badgeTags = value; }
        }

        public virtual ICollection<UserBadge> UserBadges
        {
            get { return userBadges; }
            set { userBadges = value; }
        }
    }
}
