using SqlProject.Controller;
using SqlProject.Helpers.Constants;
using SqlProject.Helpers.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProject.Helpers.Menues
{
    public class Menu
    {
        public async Task AuthenticationMenu()
        {
            ShowUserMenu();

            bool isContinued = true;

            do
            {

                UserController userController = new UserController();
                ConsoleColor.Yellow.WriteConsole("Enter the menu number:");
            MenuNumber: string menuNumberStr = Console.ReadLine();

                bool isCorrectNumberFormat = int.TryParse(menuNumberStr, out int menuNumber);
                if (isCorrectNumberFormat)
                {
                    switch (menuNumber)
                    {
                        case (int)UserMenu.Register:
                            await userController.RegisterAsync();
                            break;
                        case (int)UserMenu.Login:
                            if (await userController.LoginAsync())
                            {
                                isContinued = false;
                            }
                            else
                            {
                                ConsoleColor.Red.WriteConsole("Login failed.No such user exists");
                            }
                            break;


                        case (int)UserMenu.Delete:
                            userController.DeleteAsync();
                            break;
                        default:
                            ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                            goto MenuNumber;
                            break;
                    }

                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto MenuNumber;
                }

            } while (isContinued);

            isContinued=true;

            do
            {
                ShowCategoryMenu();
                ShowProductMenu();
                CategoryController categoryController = new CategoryController();
                ProductController productController = new ProductController();

                ConsoleColor.Yellow.WriteConsole("Enter the menu Number");
            MenuNumber2: string menu2NumberStr = Console.ReadLine();

                bool isCorrectNumberFormat2 = int.TryParse(menu2NumberStr, out int menuNumber2);
                if (isCorrectNumberFormat2)
                {
                    switch (menuNumber2)
                    {
                        case (int)CategoryMenu.GetAll:
                            await categoryController.GetAllAsync();
                            break;
                        case (int)CategoryMenu.Create:
                            await categoryController.CreateAsync();
                            break;
                        case (int)CategoryMenu.GetById:
                            await categoryController.GetByIdAsync();
                            break;
                        case (int)CategoryMenu.Delete:
                            await categoryController.DeleteAsync();
                            break;
                        case (int)CategoryMenu.Update:
                            await categoryController.UpdateAsync();
                            break;
                        case (int)CategoryMenu.Search:
                            await categoryController.SearchAsync();
                            break;
                        case (int)CategoryMenu.GetAllWithProducts:
                            await categoryController.GetAllWithProductAsync();
                            break;
                        case (int)CategoryMenu.SortWithCreatedDate:
                            await categoryController.SortWithCreatedDayAsync();
                            break;
                        case (int)CategoryMenu.GetArchiveCategories:
                            await categoryController.GetArchiveCategoriesAsync();
                            break;
                        case (int)ProductMenu.GetAll:
                            await productController.GetAllAsync();
                            break;
                        case (int)ProductMenu.Create:
                            await productController.CreateAsync();
                            break;
                        case (int)ProductMenu.GetById:
                            await productController.GetByIdAsync();
                            break;
                        case (int)ProductMenu.Delete:
                            await productController.DeleteAsync();
                            break;
                        case (int)ProductMenu.Update:
                            await productController.UpdateAsync();
                            break;
                        case (int)ProductMenu.SearchByName:
                            await productController.SearchByNameAsync();
                            break;
                        case (int)ProductMenu.FilterByCategoryName:
                            await productController.FilterByCategoryNameAsync();
                            break;
                        case (int)ProductMenu.GetAllWithCategoryId:
                            await productController.GetAllWithCategoryIdAsync();
                            break;
                        case (int)ProductMenu.SortWithPrice:
                            await productController.SortWithPriceAsync();
                            break;
                        case (int)ProductMenu.SortWithCreatedDate:
                            await productController.SortByCreatedDateAsync();
                            break;
                        case (int)ProductMenu.SearchByColor:
                            await productController.SearchByColorAsync();
                            break;
                        default:
                            ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                            goto MenuNumber2;
                            break;


                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto MenuNumber2;
                }

            } while (isContinued);
        }

      
        public  void ShowUserMenu()
        {
            ConsoleColor.Cyan.WriteConsole($@"
1-Register
2-Login 
3-Delete");
        }

        public  void ShowCategoryMenu()
        {
            ConsoleColor.Cyan.WriteConsole($@"1-CategoryGetAll,2-CategoryCreate,3-CategoryGetById,4-CategoryDelete,5-CategoryUpdate
6-CategorySearch,7-CategoryGetAllWithProducts,8-CategorySortWithCreatedDate,9-CategoryGetArchiveCategories");
        }

        public void ShowProductMenu()
        {
            ConsoleColor.Cyan.WriteConsole($@"10-ProductGetAll,11-ProductCreate,12-ProductGetById,13-ProductDelete,14-ProductUpdate,15-ProductSearchByName
16-ProductFilterByCategoryName,17-ProductGetAllWithCategoryId,18-ProductSortWithPrice,19-ProductSortWithCreatedDate,20-ProductSearchByColor");
        }
    }
}
