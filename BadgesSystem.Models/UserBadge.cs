namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserBadge
    {
        public UserBadge(DateTime? dateTime, int senderID)
        {
            this.GiveDate = dateTime;
            this.SenderID = senderID;
        }

        public UserBadge()
        {

        }

        public int ID { get; set; }
        public int SenderID { get; set; }
        public Nullable<int> RecipientID { get; set; }
        public int BadgesGiftID { get; set; }
        public Nullable<DateTime> GiveDate { get; set; }

        [MaxLength(1000)]
        public string Reason { get; set; }

        public virtual BadgesGift BadgesGift { get; set; }
    }
}
