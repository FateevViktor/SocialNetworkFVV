using Microsoft.EntityFrameworkCore;
using SocialNetworkFVV.Data;
using SocialNetworkFVV.Models;
using System.Collections.Generic;

namespace SocialNetworkFVV.Data.Repos
{
    public class FriendsRepository : IFriendsRepository
    {
        private ApplicationDbContext _db;
        public FriendsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public int AddFriend(Friend item)
        {
            _db.Friends.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public void DeleteFriend(User target, User Friend)
        {
            Friend friends = _db.Friends.FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);

            if (friends != null)
            {
                _db.Friends.Remove(friends);
                _db.SaveChanges();
            }
        }

        public Friend Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetFriendsByUser(User target)
        {
            List<Friend> friends = new List<Friend>();
            friends = _db.Friends.Include(s => s.CurrentFriend).Where(f => f.UserId == target.Id).ToList();
            return friends;
        }

        public void Update(Friend item)
        {
            throw new NotImplementedException();
        }
    }
}
