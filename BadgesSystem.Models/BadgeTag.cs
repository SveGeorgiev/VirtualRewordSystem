namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class BadgeTag
    {
        public BadgeTag()
        {

        }

        public BadgeTag(int tagID)
        {
            this.TagID = tagID;
        }

        public int ID { get; set; }
        public int TagID { get; set; }
        public int BadgesGiftID { get; set; }

        public virtual BadgesGift BadgesGift { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
