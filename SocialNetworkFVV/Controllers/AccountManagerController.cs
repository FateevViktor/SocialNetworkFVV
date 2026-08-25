using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetworkFVV.Data;
using SocialNetworkFVV.Data.Repos;
using SocialNetworkFVV.Models;
using SocialNetworkFVV.Services;
using SocialNetworkFVV.ViewModels;
using System.Collections.Generic;

namespace SocialNetworkFVV.Controllers
{
    [Route("AccountManager")]
    public class AccountManagerController : Controller
    {
        MyMapping myMapping = new MyMapping();

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IFriendsRepository _friendsRepository;
        private IMessageRepository _messageRepository;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IFriendsRepository friendsRepository, IMessageRepository messageRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _friendsRepository = friendsRepository;
            _messageRepository = messageRepository;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Home/Login");
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View("Views/Home/Index.cshtml");
        }
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var s = User;
            MyPageViewModel myPageViewModel = new MyPageViewModel();
            List<Friend> friends = new List<Friend>();
            User? user = await _userManager.FindByNameAsync(s.Identity.Name); //Ищем пользователя

            if (user!=null)
            {
                friends= _friendsRepository.GetFriendsByUser(user);
                myPageViewModel = myMapping.GetMyPageViewModelFromUser(friends, user);
                return View(myPageViewModel);
            }
            else
            {
                return View("Views/Home/Index.cshtml");
            }
        }

        [Authorize]
        [Route("UserEdit")]
        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            var s = User;
            User? user = await _userManager.FindByNameAsync(s.Identity.Name); //Ищем пользователя

            if (user != null)
            {
                UserEditViewModel userEditViewModel = new UserEditViewModel();
                userEditViewModel = myMapping.GetUserEditViewModelFromUser(user);
                return View(userEditViewModel);
            }
            else
            {
                return RedirectToAction("Edit", "AccountManager");
            }
        }

        [Authorize]
        [Route("UpdateUserEdit")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserEdit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                //user = MyMapping.GetUserFromUserEditViewModel(user, model);
                myMapping.GetUserFromUserEditViewModel(ref user, model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    return RedirectToAction("Edit", "AccountManager");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }

        [Route("UserList")]
        [HttpGet]
        public IActionResult UserList()
        {
            var model = new SearchViewModel
            {
                UserList = _userManager.Users.ToList()
            };
            return View("UserList", model);
        }
        [Route("UserList")]
        [HttpPost]
        public IActionResult UserList(string search)
        {
            var model = new SearchViewModel
            {
                //Не чувствителен к регистру
                UserList = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList()
            };
            return View("UserList", model);
        }

        [Route("AddFriend")]
        [HttpPost]
        public async Task<IActionResult> AddFriend()
        {
            string id = Request.Query["id"];
            User currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("MyPage", "AccountManager"); // Перенаправить на страницу пользователя
            }

            User friend = await _userManager.FindByIdAsync(id);

            if (friend == null)
            {
                return RedirectToAction("MyPage", "AccountManager"); // Перенаправить на страницу пользователя
            }
            Friend _friend = new Friend();

            _friend = myMapping.GetFriendFromUsers(currentUser, friend);
            _friendsRepository.AddFriend(_friend);

            return RedirectToAction("MyPage", "AccountManager");

        }

        [Route("DeleteFriend")]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var friend = await _userManager.FindByIdAsync(id);

            _friendsRepository.DeleteFriend(result, friend);

            return RedirectToAction("MyPage", "AccountManager");

        }

        [Route("DeleteMessage")]
        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            string _id = String.Empty;

            ChatViewModel model = new ChatViewModel();
            //Получить сообщение по Id
            Message? message = _messageRepository.GetMessages(id);
            if (message != null)
            {
                _messageRepository.DeleteMessage(message);


                _id = message.RecipientId;
                model = await GenerateChat(message.RecipientId);
                return View("Chat", model);
            }
            return View("Chat");
        }

        private async Task<ChatViewModel> GenerateChat(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);
            var friend = await _userManager.FindByIdAsync(id);

            var mess = _messageRepository.GetMessages(result, friend);

            var model = new ChatViewModel()
            {
                You = result,
                ToWhom = friend,
                History = mess.OrderBy(x => x.MessageId).ToList(),
            };

            return model;
        }

        [Route("Chat")]
        [HttpGet]
        public async Task<IActionResult> Chat()
        {

            var id = Request.Query["id"];

            var model = await GenerateChat(id);
            return View("Chat", model);
        }

        [Route("Chat")]
        [HttpPost]
        public async Task<IActionResult> Chat(string id)
        {
            var model = await GenerateChat(id);
            return View("Chat", model);
        }

        [Route("NewMessage")]
        [HttpPost]
        public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);
            var friend = await _userManager.FindByIdAsync(id);

            Message item = new Message()
            {
                Sender = result,
                Recipient = friend,
                Text = chat.NewMessage.Text,
                MessageDateTime = DateTime.Now,
            };
            _messageRepository.AddMessage(item);

            var mess = _messageRepository.GetMessages(result, friend);

            var model = new ChatViewModel()
            {
                You = result,
                ToWhom = friend,
                History = mess.OrderBy(x => x.MessageId).ToList(),
            };
            return View("Chat", model);
        }
    }
}
