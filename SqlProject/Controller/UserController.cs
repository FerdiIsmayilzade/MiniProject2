using Repository.Data;
using SqlProject.Helpers.Extentions;
using Service.Services;
using SqlProject.Helpers.Constants;
using Domain.Entities;

namespace SqlProject.Controller
{
    public class UserController
    {
        private readonly UserServices _userServices;

        public UserController()
        {
            _userServices = new UserServices();
        }

        public async Task RegisterAsync()
        {
            Console.WriteLine("Enter the user fullname:");
        FullName: string fullName = Console.ReadLine();

            if (string.IsNullOrEmpty(fullName))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto FullName;
            }
            Console.WriteLine("Enter the username:");
        UserName: string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto UserName;
            }
            Console.WriteLine("Enter the email:");
        Email: string email = Console.ReadLine();

            if (string.IsNullOrEmpty(email))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto Email;
            }
            Console.WriteLine("Enter the password(min 8 characters):");
        Password: string password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto Password;
            }
            for (int i = 0; i < password.Length; i++)
            {
                if (Convert.ToInt32(password[i]) !=null &&   password[i].ToString() != password[i].ToString().ToLower() && password.Length >= 8)
                {
                     _userServices.CreateAsync(new User { FullName = fullName,UserName=username, Email = email, Password = password });
                    ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto Password;
                }
            }
        }
        public async Task<bool> LoginAsync()
        {
            Console.WriteLine("Enter the username:");
            userName: string userName= Console.ReadLine();
            if (string.IsNullOrEmpty(userName))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto userName;
            }
            Console.WriteLine("Enter the password:");
            password: string password= Console.ReadLine();    

            if (string.IsNullOrEmpty(password))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto password;
            }
            bool result= await _userServices.CheckAsync(userName, password);
           
            if(result)
            {
                ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter the user id:");
            UserId: string idStr=Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);

            if (isCorrectIdFormat)
            {
                await _userServices.DeleteAsync(id);
                ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullDeleted);

            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto UserId;
            }

        }
    }
}
