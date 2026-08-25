using SocialNetworkFVV.Models;
using SocialNetworkFVV.ViewModels;

namespace SocialNetworkFVV.Services
{
    public class MyMapping
    {
        public MyMapping()
        {

        }
        //Получить User
        public User GetUserFromRegisterViewModel(RegisterViewModel registerViewModel)
        {
            User user = new User();

            user.FirstName = registerViewModel.FirstName;
            user.LastName = registerViewModel.LastName;
            user.MiddleName = string.Empty;
            user.BirthDate = registerViewModel.BirthdayDate;
            user.UserName = registerViewModel.Login;
            user.Email = registerViewModel.EmailReg;
            user.PasswordHash = registerViewModel.PasswordReg;

            return user;
        }

        public User GetUserFromLoginViewModel(LoginViewModel loginViewModel)
        {
            User user = new User();

            //user.UserName = registerViewModel.Login;
            user.UserName = loginViewModel.Login;
            user.PasswordHash = loginViewModel.Password;

            return user;
        }

        public void GetUserFromUserEditViewModel(ref User user, UserEditViewModel userEditViewModel)
        {
            user.Image = userEditViewModel.Image;
            user.LastName = userEditViewModel.LastName;
            user.MiddleName = userEditViewModel.MiddleName;
            user.FirstName = userEditViewModel.FirstName;
            user.Email = userEditViewModel.Email;
            user.BirthDate = userEditViewModel.BirthdayDate;
            //user.UserName = userEditViewModel.Email;
            user.Status = userEditViewModel.Status;
            user.About = userEditViewModel.About;

            //return user;
        }

        //--------------------------------------------------------------------
        public UserEditViewModel GetUserEditViewModelFromUser(User user)
        {
            UserEditViewModel userEditViewModel = new UserEditViewModel();

            userEditViewModel.Id = user.Id;
            userEditViewModel.FirstName = user.FirstName;
            userEditViewModel.LastName = user.LastName;
            userEditViewModel.MiddleName = user.MiddleName;
            userEditViewModel.BirthdayDate = user.BirthDate;
            userEditViewModel.Email = user.Email;
            userEditViewModel.Status = user.Status;
            userEditViewModel.Image = user.Image;
            userEditViewModel.About = user.About;

            return userEditViewModel;
        }
        //-------------------------------------------
        public Friend GetFriendFromUsers(User user, User friend)
        {
            Friend _friend = new Friend();
            _friend.UserId = user.Id;
            _friend.User = user;
            _friend.CurrentFriend = friend;
            _friend.CurrentFriendId = friend.Id;

            return _friend;
        }
        //--------------------------------------------------------------------
        public MyPageViewModel GetMyPageViewModelFromUser(List<Friend> friends, User user)
        {
            MyPageViewModel myPageViewModel = new MyPageViewModel();
            List<User> userF = new List<User>();
            myPageViewModel.Id = user.Id;
            myPageViewModel.Login = user.UserName;
            myPageViewModel.FirstName = user.FirstName;
            myPageViewModel.LastName = user.LastName;
            myPageViewModel.MiddleName = user.MiddleName;
            myPageViewModel.BirthdayDate = user.BirthDate;
            myPageViewModel.Email = user.Email;
            myPageViewModel.Status = user.Status;
            myPageViewModel.Image = user.Image;
            myPageViewModel.About = user.About;

            foreach (Friend friend in friends) 
            {
                userF.Add(friend.CurrentFriend);
            }
            myPageViewModel.Friends = userF;
            return myPageViewModel;
        }
    }
}
