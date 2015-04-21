using BadgesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgesSystem.Data
{
    public interface IUowData
    {
        IRepository<User> Users { get; }

        IRepository<UserBadge> UserBadges { get; }

        IRepository<BadgesGift> BadgesGifts { get; }

        IRepository<BadgeTag> BadgeTags { get; }

        IRepository<File> Files { get; }

        IRepository<Tag> Tags { get; }

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
