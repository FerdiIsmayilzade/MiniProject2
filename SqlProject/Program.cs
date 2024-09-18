
//using SqlProject.Controller;
//using SqlProject.Helpers.Constants;
//using SqlProject.Helpers.Extentions;
//using SqlProject.Helpers.Menues;

//ShowUserMenu();

//bool isContinued=true;

//do
//{
//    UserController userController = new UserController();
//    ConsoleColor.Yellow.WriteConsole("Enter the menu number:");
//    MenuNumber: string menuNumberStr=Console.ReadLine();

//    bool isCorrectNumberFormat = int.TryParse(menuNumberStr, out int menuNumber);
//    if (isCorrectNumberFormat)
//    {
//        switch(menuNumber)
//        {
//            case (int)UserMenu.Register:
//                userController.RegisterAsync();
//                break;
//            case (int)UserMenu.Login:
//                if(await userController.LoginAsync())
//                {

//                }
//                else
//                {
//                    ConsoleColor.Red.WriteConsole("No such user exists");
//                }
//                break;


//            case (int)UserMenu.Delete:
//                userController.DeleteAsync();
//                break;
//        }

//    }
//    else
//    {
//        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
//        goto MenuNumber;
//    }

//} while(isContinued);






//static void ShowUserMenu()
//{
//    ConsoleColor.Cyan.WriteConsole($@"
//1-Register
//2-Login
//3-Delete");
//}



using SqlProject.Controller;

CategoryController categoryController = new CategoryController();

categoryController.DeleteAsync();