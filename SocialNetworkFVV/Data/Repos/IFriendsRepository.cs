using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.Data.Repos
{
    public interface IFriendsRepository
    {
        List<Friend> GetFriendsByUser(User target); //Получить всех друзей
        Friend Get(int id);
        int AddFriend(Friend item); //Добавить друга
        void Update(Friend item);
        void DeleteFriend(User target, User Friend);
    }
}
