using Service.Services;
using SqlProject.Helpers.Extentions;
using SqlProject.Helpers.Constants;
using Domain.Entities;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Internal;
using Service.Services.Interfaces;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Service.Helpers.Exceptions;

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
                ConsoleColor.Blue.WriteConsole($"{item.Id}-{item.Name} {item.CreatedDate} ");
            }
        }
        public async Task CreateAsync()
        {
            try
            {
                var data = await _categoryServices.GetAllAsync();

                Console.WriteLine("Enter the category name:");
            CategoryName: string categoryName = Console.ReadLine();

                string symbols = @"^[A-Za-z\s]+$";
                if (string.IsNullOrEmpty(categoryName.Trim()) || !Regex.IsMatch(categoryName,symbols))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto CategoryName;
                }
                else if(categoryName.Any(char.IsDigit))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto CategoryName;
                }
                else if (!categoryName.Any(char.IsLetter))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto CategoryName;
                }
                
             
                foreach(var item in  data)
                {
                    if(item.Name.ToLower().Trim()==categoryName.ToLower().Trim())
                    {
                        ConsoleColor.Red.WriteConsole("Data exist");
                        goto CategoryName;
                    }
                }
                
                 
                ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                await _categoryServices.CreateAsync(new Category { Name = categoryName.Trim().ToLower() });



            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message + "," + "Please try again:");
            }




        }

        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter the category id:");
            Id: string idStr=Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);
            if(isCorrectIdFormat)
            {
                try
                {

                    
                    


                        ConsoleColor.Yellow.WriteConsole("Are you sure this category should be deleted?");
                    Input: string inputStr = Console.ReadLine();
                        bool isCorrectInputFormat = int.TryParse(inputStr, out int input);

                        if (isCorrectInputFormat)
                        {
                            switch (input)
                            {
                                case 1:
                                    await _categoryServices.DeleteAsync(id);
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
                catch(Exception ex)
                {
                    ConsoleColor.Red.WriteConsole("Data not found" + "," + "Please try again:");
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto Id;
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
            ConsoleColor.Yellow.WriteConsole($@"
1-Order by
2-Order by descending");
            Console.WriteLine("Enter the option");
            Input: string inputStr=Console.ReadLine();

            bool isCorrectFormat=int.TryParse(inputStr, out int input);
            if(isCorrectFormat)
            {
                switch (input)
                {
                    case 1:
                        foreach (var item in await _categoryServices.SortWithCreatedDateAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"{item.Name}");
                        }

                        break;
                    case 2:
                        foreach (var item in await _categoryServices.SortWithCreatedDateAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"{item.Name}");
                        }

                            break;
                    default:
                        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
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

            if (string.IsNullOrEmpty(searchText.Trim()))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto Search;
            }
            else if (searchText.Any(char.IsDigit))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto Search;
            }
            else if (!searchText.Any(char.IsLetter))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto Search;
            }
            var result=await _categoryServices.SearchAsync(searchText);
            if(result.Count()==0)
            {
                ConsoleColor.Red.WriteConsole("Data not found");

            }
            else
            {
                foreach (var item in result)
                {
                    ConsoleColor.Blue.WriteConsole($"{item.Name}");
                }
               
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

            bool isCorrectIdForma = int.TryParse(idStr, out int id);

            if (isCorrectIdForma)
            {
                try
                {


                CategoryName: Console.WriteLine("Enter the new category name:");
                    string newCategoryName = Console.ReadLine();

                    var data = await _categoryServices.GetAllAsync();


                    string symbols = @"^[A-Za-z\s]+$";
                    if (!Regex.IsMatch(newCategoryName, symbols))
                    {
                        if (!string.IsNullOrEmpty(newCategoryName.Trim()))
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto CategoryName;
                        }
                    }
                    else if (newCategoryName.Any(char.IsDigit))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto CategoryName;
                    }
                    else if (!newCategoryName.Any(char.IsLetter))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto CategoryName;
                    }

                    foreach (var item in data)
                    {
                        if (item.Name == newCategoryName.ToLower().Trim())
                        {
                            ConsoleColor.Red.WriteConsole("Data exist");
                            goto CategoryName;
                        }
                    }


                    ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                    await _categoryServices.UpdateAsync(id, new Category { Name = newCategoryName });
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole("Data not found" + "," + "Please try again:");
                    goto CategoryId;

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
