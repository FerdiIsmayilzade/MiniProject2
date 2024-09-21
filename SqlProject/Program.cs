
//using SqlProject.Controller;
//using SqlProject.Helpers.Constants;
//using SqlProject.Helpers.Extentions;
//using SqlProject.Helpers.Menues;

//ShowUserMenu();

//bool isContinued = true;

//do
//{

//    UserController userController = new UserController();
//    ConsoleColor.Yellow.WriteConsole("Enter the menu number:");
//MenuNumber: string menuNumberStr = Console.ReadLine();

//    bool isCorrectNumberFormat = int.TryParse(menuNumberStr, out int menuNumber);
//    if (isCorrectNumberFormat)
//    {
//        switch (menuNumber)
//        {
//            case (int)UserMenu.Register:
//                await userController.RegisterAsync();
//                break;
//            case (int)UserMenu.Login:
//                if (await userController.LoginAsync())
//                {
//                    ShowCategoryMenu();
//                    ShowProductMenu();
//                    CategoryController categoryController = new CategoryController();
//                    ProductController productController = new ProductController();

//                    ConsoleColor.Yellow.WriteConsole("Enter the menu Number");
//                    MenuNumber2: string menu2NumberStr =Console.ReadLine();

//                    bool isCorrectNumberFormat2=int.TryParse(menuNumberStr,out int menuNumber2);
//                    if (isCorrectNumberFormat2)
//                    {
//                        switch(menuNumber2)
//                        {
//                            case (int)CategoryMenu.GetAll:
//                                categoryController.GetAllAsync();
//                                break;
//                            case (int)CategoryMenu.Create:
//                                categoryController.CreateAsync();
//                                break;
//                            case (int)CategoryMenu.GetById:
//                                categoryController.GetByIdAsync();
//                                break;
//                            case (int)CategoryMenu.Delete:
//                                categoryController.DeleteAsync();
//                                break;
//                            case (int)CategoryMenu.Update:
//                                categoryController.UpdateAsync();
//                                break;
//                            case (int)CategoryMenu.Search:
//                                categoryController.SearchAsync();
//                                break;
//                            case (int)CategoryMenu.GetAllWithProducts:
//                                categoryController.GetAllWithProductAsync();
//                                break;
//                            case (int)CategoryMenu.SortWithCreatedDate:
//                                categoryController.SortWithCreatedDayAsync();
//                                break;
//                            case (int)CategoryMenu.GetArchiveCategories:
//                                categoryController.GetArchiveCategoriesAsync();
//                                break;
//                            case (int)ProductMenu.GetAll:
//                                productController.GetAllAsync();
//                                break;
//                            case (int)ProductMenu.Create:
//                                productController.CreateAsync();
//                                break;
//                            case (int)ProductMenu.GetById:
//                                productController.GetByIdAsync();
//                                break;
//                            case (int)ProductMenu.Delete:
//                                productController.DeleteAsync();
//                                break;
//                            case (int)ProductMenu.Update:
//                                productController.UpdateAsync();
//                                break;
//                            case (int)ProductMenu.SearchByName:
//                                productController.SearchByNameAsync();
//                                break;
//                            case (int)ProductMenu.FilterByCategoryName:
//                                productController.FilterByCategoryNameAsync();
//                                break;
//                            case (int)ProductMenu.GetAllWithCategoryId:
//                                productController.GetAllWithCategoryIdAsync();
//                                break;
//                            case (int)ProductMenu.SortWithPrice:
//                                productController.SortWithPriceAsync();
//                                break;
//                            case (int)ProductMenu.SortWithCreatedDate:
//                                productController.SortByCreatedDateAsync();
//                                break;
//                            case (int)ProductMenu.SearchByColor:
//                                productController.SearchByColorAsync();
//                                break;
//                            default:
//                                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
//                                goto MenuNumber2;
//                                break;


//                        }
//                    }
//                    else
//                    {
//                        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
//                        goto MenuNumber2;
//                    }



//                }
//                else
//                {
//                    ConsoleColor.Red.WriteConsole("Login failed.No such user exists");
//                }
//                break;


//            case (int)UserMenu.Delete:
//                userController.DeleteAsync();
//                break;
//            default:
//                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
//                goto MenuNumber;
//                break;
//        }

//    }
//    else
//    {
//        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
//        goto MenuNumber;
//    }

//} while (isContinued);






//static void ShowUserMenu()
//{
//    ConsoleColor.Cyan.WriteConsole($@"
//1-Register
//2-Login 
//3-Delete");
//}

//static void ShowCategoryMenu()
//{
//    ConsoleColor.Cyan.WriteConsole($@"1-CategoryGetAll,2-CategoryCreate,3-CategoryGetById,4-CategoryDelete,5-CategoryUpdate
//6-CategorySearch,7-CategoryGetAllWithProducts,8-CategorySortWithCreatedDate,9-CategoryGetArchiveCategories");
//}

//static void ShowProductMenu()
//{
//    ConsoleColor.Cyan.WriteConsole($@"10-ProductGetAll,11-ProductCreate,12-ProductGetById,13-ProductDelete,14-ProductUpdate,15-ProductSearchByName
//16-ProductFilterByCategoryName,17-ProductGetAllWithCategoryId,18-ProductSortWithPrice,19-ProductSortWithCreatedDate,20-ProductSearchByColor");
//}

using SqlProject.Helpers.Menues;

Menu menu = new Menu();

await menu.AuthenticationMenu();

