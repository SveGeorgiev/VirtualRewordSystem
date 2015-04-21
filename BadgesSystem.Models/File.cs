namespace BadgesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class File
    {
        private ICollection<BadgesGift> badgesGift;

        public File()
        {
            this.badgesGift = new HashSet<BadgesGift>();
        }

        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        public virtual ICollection<BadgesGift> BadgesGifts
        {
            get { return badgesGift; }
            set { badgesGift = value; }
        }
    }
}
