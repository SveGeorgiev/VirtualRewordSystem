namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        private ICollection<BadgeTag> badgeTags;

        public Tag()
        {
            this.badgeTags = new HashSet<BadgeTag>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        [RegularExpression(@"^[a-z\-.#+]+$")]
        public string Name { get; set; }

        public virtual ICollection<BadgeTag> BadgeTags
        {
            get { return badgeTags; }
            set { badgeTags = value; }
        }
    }
}
