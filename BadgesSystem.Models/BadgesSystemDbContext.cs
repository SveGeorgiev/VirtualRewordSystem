using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgesSystem.Models
{
    public class BadgesSystemDbContext : DbContext
    {
        public BadgesSystemDbContext()
            : base("BadgesSystemDb")
        {
#if DEBUG
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
            this.Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<File> Files { get; set; }
        public IDbSet<BadgesGift> BadgesGifts { get; set; }
        public IDbSet<BadgeTag> BadgeTags { get; set; }
        public IDbSet<UserBadge> UserBadges { get; set; }
    }
}
