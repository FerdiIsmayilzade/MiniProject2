using Service.Services;
using SqlProject.Helpers.Extentions;
using SqlProject.Helpers.Constants;
using Domain.Entities;
using System.Globalization;

namespace SqlProject.Controller
{
    public class CategoryController
    {
        private readonly CategoryServices _categoryServices;

        public CategoryController()
        {
            _categoryServices = new CategoryServices();
        }

        public async Task GetAllAsync()
        {
            var result=await _categoryServices.GetAllAsync();

            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Name} {item.CreatedDate} ");
            }
        }
        public async Task Create()
        {
            Console.WriteLine("Enter the category name:");
            CategoryName: string categoryName=Console.ReadLine();

            if (string.IsNullOrEmpty(categoryName))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryName;
            }
           
            _categoryServices.CreateAsync(new Category { Name = categoryName });
        
             
        }
        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter the category id:");
            CategoryId: string idStr = Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);

            if (isCorrectIdFormat)
            {
                try
                {
                    ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullDeleted);
                    await _categoryServices.DeleteAsync(id);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + "," + "Please try again:");
                    goto CategoryId;

                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryId;
            }
        }
        public async Task GetAllWithProductAsync()
        {
            var result=await _categoryServices.GetAllWithProductsAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Name} {item.Products}");
            }
        }
        public async Task SortWithCreatedDay()
        {
            var result=await _categoryServices.SortWithCreatedDateAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Name} {item.CreatedDate}");
            }
        }
        public async Task GetArchiveCategoriesAsync()
        {
            var result=await _categoryServices.GetArchiveCategoriesAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Operation} {item.CreatedDate} {item.CategoryId} {item.Name}");
            }
        }
        public async Task SearchAsync()
        {
            Console.WriteLine("Enter the searchtext:");
            Search: string searchText=Console.ReadLine();

            if (string.IsNullOrEmpty(searchText))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto Search;
            }
            var result=await _categoryServices.SearchAsync(searchText);
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Name}");
            }
        }
        public async Task GetById()
        {
            Console.WriteLine("Enter the category id:");
            CategoryId: string idStr=Console.ReadLine();

            bool isCorrectIdForma=int.TryParse(idStr, out int id);

            if (isCorrectIdForma)
            {
                try
                {
                    var response = await _categoryServices.GetByIdAsync(id);
                    ConsoleColor.Blue.WriteConsole(response.Name);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message+","+"Please try again:");
                    goto CategoryId;

                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryId;
            }
        }
        public async Task UpdateAsync()
        {
            Console.WriteLine("Enter the category id:");
            CategoryId: string idStr = Console.ReadLine();

            bool isCorrectIdFormat=int.TryParse(idStr,out int id);

            if (isCorrectIdFormat)
            {
                Console.WriteLine("Enter the new category name:");
                CategoryName: string newCategoryName = Console.ReadLine();

                if (Convert.ToInt32(newCategoryName) == null)
                {
                    int count = 0;
                    foreach (var item in await _categoryServices.GetAllAsync())
                    {
                        if (newCategoryName == item.Name)
                        {
                            count++;
                        }

                    }
                    if (count == 0)
                    {
                        await _categoryServices.UpdateAsync(id, new Category { Name = newCategoryName });
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                        goto CategoryName;
                    }
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryId;
            }





        }
    }
}
