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

        public async Task Register()
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
                if (password[i].ToString() != password[i].ToString().ToLower() && password.Length < 9)
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
    }
}
