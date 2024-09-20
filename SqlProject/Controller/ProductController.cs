using Domain.Entities;
using Service.Services;
using Service.Services.Interfaces;
using SqlProject.Helpers.Constants;
using SqlProject.Helpers.Extentions;
using System.Text.RegularExpressions;

namespace SqlProject.Controller
{
    public class ProductController
    {
        private readonly ProductServices _productServices;
        private readonly CategoryServices _categoryServices;

        public ProductController()
        {
            _productServices = new ProductServices();
            _categoryServices = new CategoryServices();
        }

        public async Task GetAllAsync()
        {
            var result = await _productServices.GetAllAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($@"
{item.Id}-
ProductName:{item.Name}
ProductPrice:{item.Price}
ProductDescription:{item.Description}
ProductColor:{item.Color}
ProductCount:{item.Count}
ProductCategory:{item.Category}
CreatedDate:{item.CreatedDate}");
            }
        }
        public async Task CreateAsync()
        {
            try
            {
                var data = await _productServices.GetAllAsync();
                Console.WriteLine("Enter the product name:");
            ProductName: string productName = Console.ReadLine();

                string symbols = @"^[A-Za-z\s]+$";
                if (string.IsNullOrEmpty(productName.Trim()))
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                    goto ProductName;
                }



                foreach (var item in data)
                {
                    if (item.Name.ToLower().Trim() == productName.ToLower().Trim())
                    {
                        ConsoleColor.Red.WriteConsole("Data exist");
                        goto ProductName;
                    }
                }
                Console.WriteLine("Enter the product price:");
            ProductPrice: string productPriceStr = Console.ReadLine();

                bool isCorrectFormat = float.TryParse(productPriceStr, out float productPrice);
                if (isCorrectFormat)
                {
                    Console.WriteLine("Enter the product description:");
                ProductDescription: string productDescription = Console.ReadLine();

                    if (string.IsNullOrEmpty(productDescription.Trim()) || !Regex.IsMatch(productDescription, symbols))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductDescription;
                    }
                    else if (productDescription.Any(char.IsDigit))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductDescription;
                    }
                    else if (!productDescription.Any(char.IsLetter))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductDescription;
                    }

                    Console.WriteLine("Enter the product color");
                ProductColor: string productColor = Console.ReadLine();

                    if (string.IsNullOrEmpty(productColor.Trim()) || !Regex.IsMatch(productColor, symbols) || productColor.Any(char.IsDigit))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductColor;
                    }


                    Console.WriteLine("Enter the product count:");
                ProductCount: string productCountStr = Console.ReadLine();

                    bool isCorrectCountFormat = int.TryParse(productCountStr, out int productCount);



                    if (isCorrectCountFormat)
                    {
                        var response = await _categoryServices.GetAllAsync();
                        foreach (var item in response)
                        {
                            ConsoleColor.Blue.WriteConsole($"{item.Id} {item.Name}");
                        }
                        Console.WriteLine("Choose CategoryId");
                        CategoryName: string idStr = Console.ReadLine();
                        if (string.IsNullOrEmpty(idStr))
                        {
                            ConsoleColor.Red.WriteConsole("This can not be empty");
                            goto CategoryName;
                        }

                        Console.WriteLine("Enter the CategoryId:");
                    ProductCategoryId: string categoryIdStr = Console.ReadLine();

                        bool isCorrectIdFormat = int.TryParse(categoryIdStr, out int categoryId);
                        if (isCorrectIdFormat)
                        {
                            Category category = await _categoryServices.GetByIdAsync(categoryId);
                            if (category == null)
                            {
                                ConsoleColor.Red.WriteConsole("Category not found");
                                return;
                            }
                        }
                        if (isCorrectIdFormat)
                        {
                            ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                            await _productServices.CreateAsync(new Product
                            {
                                Name = productName,
                                Price = productPrice,
                                Description = productDescription,
                                Color = productColor,
                                Count = productCount,
                                CategoryId = categoryId
                            });

                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                            goto ProductCategoryId;
                        }

                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                        goto ProductCount;

                    }


                }




                else
                {
                    ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                    goto ProductPrice;

                }










            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message + "," + "Please try again:");
            }
        }
        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter the product id:");
        Id: string idStr = Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);
            if (isCorrectIdFormat)
            {
                try
                {





                    ConsoleColor.Yellow.WriteConsole("Are you sure this product should be deleted?");
                    Console.WriteLine("If you are sure, press button 1, if you are not sure, press button 2");
                Input: string inputStr = Console.ReadLine();
                    bool isCorrectInputFormat = int.TryParse(inputStr, out int input);

                    if (isCorrectInputFormat)
                    {
                        switch (input)
                        {
                            case 1:
                                await _productServices.DeleteAsync(id);
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
                catch (Exception ex)
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

        public async Task SearchByNameAsync()
        {
            Console.WriteLine("Enter the searchtext:");
        Search: string searchText = Console.ReadLine();

            if (string.IsNullOrEmpty(searchText.Trim()))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto Search;
            }

            var result = await _productServices.SearchByNameAsync(searchText);
            if (result.Count() == 0)
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
        public async Task GetByIdAsync()
        {
            Console.WriteLine("Enter the product id:");
        CategoryId: string idStr = Console.ReadLine();

            bool isCorrectIdForma = int.TryParse(idStr, out int id);

            if (isCorrectIdForma)
            {
                try
                {
                    var response = await _productServices.GetByIdAsync(id);
                    ConsoleColor.Blue.WriteConsole(response.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "," + "Please try again:");
                    goto CategoryId;

                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryId;
            }
        }
        public async Task SearchByColorAsync()
        {
            Console.WriteLine("Enter the searchtext:");
        Search: string searchText = Console.ReadLine();

            if (string.IsNullOrEmpty(searchText.Trim()))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto Search;
            }

            var result = await _productServices.SearchByNameAsync(searchText);
            if (result.Count() == 0)
            {
                ConsoleColor.Red.WriteConsole("Data not found");

            }
            else
            {
                foreach (var item in result)
                {
                    ConsoleColor.Blue.WriteConsole($"ProductName:{item.Name} ProductColor:{item.Color}");
                }


            }


        }
        public async Task GetAllWithCategoryIdAsync()
        {
            var result=await _categoryServices.GetAllAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Name} {item.Id}");
            }
            CategoryId: Console.WriteLine("Enter the category id:");
            Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdForma = int.TryParse(idStr, out  id);
            

            if (isCorrectIdForma)
            {
               
                try
                {
                    var response = await _productServices.GetAllWithCategoryIdAsync(id);
                    if (response.Count() == 0)
                    {
                        ConsoleColor.Red.WriteConsole("Data not found");
                        goto Id;
                    }
                    else
                    {
                        foreach (var item in response)
                        {
                            string str = $"Name-{item.Name + "," + item.CreatedDate}";

                            Console.WriteLine(str);
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + "," + "Please try again:");
                    goto Id;
                }
               
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto CategoryId;

            }



        }
        
        public async Task SortWithPriceAsync()

        {
            ConsoleColor.Yellow.WriteConsole($@"
1-Order by
2-Order by descending");
            Console.WriteLine("Enter the option");
        Input: string inputStr = Console.ReadLine();

            bool isCorrectFormat = int.TryParse(inputStr, out int input);
            if (isCorrectFormat)
            {
                switch (input)
                {
                    case 1:
                        foreach (var item in await _productServices.SortWithPriceAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"ProductName:{item.Name} ProductPrice{item.Price}");
                        }

                        break;
                    case 2:
                        foreach (var item in await _productServices.SortWithPriceAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"ProductName:{item.Name} ProductPrice{item.Price}");

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

        public async Task SortByCreatedDateAsync()
        {
            ConsoleColor.Yellow.WriteConsole($@"
1-Order by
2-Order by descending");
            Console.WriteLine("Enter the option");
        Input: string inputStr = Console.ReadLine();

            bool isCorrectFormat = int.TryParse(inputStr, out int input);
            if (isCorrectFormat)
            {
                switch (input)
                {
                    case 1:
                        foreach (var item in await _productServices.SortByCreatedDateAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"ProductName:{item.Name} ProductCreatedDate{item.CreatedDate}");
                        }

                        break;
                    case 2:
                        foreach (var item in await _productServices.SortByCreatedDateAsync(input))
                        {
                            ConsoleColor.Blue.WriteConsole($"ProductName:{item.Name} ProductCreatedDate{item.CreatedDate}");


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

    }
}
