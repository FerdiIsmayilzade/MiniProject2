using Repository.Data;
using SqlProject.Helpers.Extentions;
using Service.Services;
using SqlProject.Helpers.Constants;
using Domain.Entities;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Service.Services.Interfaces;

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
            try
            {
                var data =await _userServices.GetAllAsync();
                Console.WriteLine("Enter the user fullname:");
            FullName: string fullName = Console.ReadLine();

                string symbols = @"^[\p{L}\p{M}' \.\-]+$";

                if (string.IsNullOrEmpty(fullName.Trim()) || !Regex.IsMatch(fullName,symbols))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto FullName;
                }
                else if (!fullName.Any(char.IsLetter))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto FullName;
                }
                

               
                Console.WriteLine("Enter the username:");
            UserName: string username = Console.ReadLine();

                if (string.IsNullOrEmpty(username.Trim()))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto UserName;
                }
                foreach (var item in data)
                {
                    if (item.UserName.ToLower().Trim() == username.ToLower().Trim())
                    {
                        ConsoleColor.Red.WriteConsole("Data exist");
                        goto UserName;
                    }
                }

                Console.WriteLine("Enter the email:");
            Email: string email = Console.ReadLine();

                if (string.IsNullOrEmpty(email.Trim()))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto Email;
                }
                if (!email.Contains('@'))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto Email;
                }
                for (int i = 0; i < email.Length; i++)
                {
                    if (email[0] == '@')
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto Email;
                    }
                }

                foreach (var item in data)
                {
                    if (item.Email.ToLower().Trim() == email.ToLower().Trim())
                    {
                        ConsoleColor.Red.WriteConsole("Data exist");
                        goto Email;
                    }
                }
                Console.WriteLine("Enter the password(at least 8 characters, one uppercase letter,min 1 number):");
            Password: string password = Console.ReadLine();
                if (string.IsNullOrEmpty(password.Trim()))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto Password;
                }
                else if(!password.Any(char.IsDigit))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto Password;
                }
                else if (password.ToString() == password.ToString().ToLower())
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto Password;
                }
                else if (password.Length < 8)
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto Password;
                }
              

            }
            catch(Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message+","+"Please try again:");
            }
        }

        public async Task<bool> LoginAsync()
        {
            Console.WriteLine("Enter the username:");
            userName: string userName= Console.ReadLine();
            if (string.IsNullOrEmpty(userName.Trim()))
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
            foreach (var item in await _userServices.GetAllAsync())
            {
                ConsoleColor.Blue.WriteConsole($@"{item.Id}-FullName:{item.FullName} UserName:{item.UserName} Email:{item.Email} Password:{item.Password}");
                
            }
            Console.WriteLine("Enter the user id:");
           Id: string idStr = Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);
            if (isCorrectIdFormat)
            {
                var response = await _userServices.GetByIdAsync(id);


                if (response == null)
                {
                    ConsoleColor.Red.WriteConsole("Data not found");
                    goto Id;
                }





                ConsoleColor.Yellow.WriteConsole("Are you sure this user should be deleted?");
                      Console.WriteLine("If you are sure, press button 1, if you are not sure, press button 2");

                Input: string inputStr = Console.ReadLine();
                    bool isCorrectInputFormat = int.TryParse(inputStr, out int input);

                    if (isCorrectInputFormat)
                    {
                        switch (input)
                        {
                            case 1:
                                await _userServices.DeleteAsync(id);
                                ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullDeleted);
                                break;
                            case 2:
                                return;
                                break;
                            default:
                                ConsoleColor.Red.WriteConsole("No correct option was specified.Please try again:");
                                goto Input;
                                break;


                        }
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                        goto Input;
                    }

              
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto Id;
            }



        }
    }
}
