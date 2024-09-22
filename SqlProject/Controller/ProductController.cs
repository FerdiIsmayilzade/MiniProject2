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
ProductCategoryId:{item.CategoryId}
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

                string symbols = @"^[\p{L}\p{M}' \.\-]+$";

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
                    if(productPrice <= 0)
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductPrice;
                    }
                    Console.WriteLine("Enter the product description:");
                ProductDescription: string productDescription = Console.ReadLine();

                    if (string.IsNullOrEmpty(productDescription.Trim()) || !Regex.IsMatch(productDescription, symbols))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductDescription;
                    }
                  

                    Console.WriteLine("Enter the product color");
                ProductColor: string productColor = Console.ReadLine();

                    if (string.IsNullOrEmpty(productColor.Trim()) || !Regex.IsMatch(productColor, symbols) || !productColor.Any(char.IsLetter))
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductColor;
                    }


                    Console.WriteLine("Enter the product count:");
                ProductCount: string productCountStr = Console.ReadLine();

                    bool isCorrectCountFormat = int.TryParse(productCountStr, out int productCount);



                    if (isCorrectCountFormat)
                    {
                        if(productCount < 0)
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto ProductCount;
                        }
                        var response = await _categoryServices.GetAllAsync();
                        foreach (var item in response)
                        {
                            ConsoleColor.Blue.WriteConsole($"{item.Id} {item.Name}");
                        }
                        Console.WriteLine("Choose CategoryId");
                        Id: string idStr = Console.ReadLine();
                        if (string.IsNullOrEmpty(idStr.Trim()))
                        {
                            ConsoleColor.Red.WriteConsole("This can not be empty");
                            goto Id;
                        }

                       

                        bool isCorrectIdFormat = int.TryParse(idStr, out int id);
                        if (isCorrectIdFormat)
                        {
                            if(id <= 0)
                            {
                                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                                goto Id;
                            }

                            Category category = await _categoryServices.GetByIdAsync(id);
                            if (category == null)
                            {
                                ConsoleColor.Red.WriteConsole("Category not found");
                                return;
                            }
                        
                       
                            ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);
                            await _productServices.CreateAsync(new Product
                            {
                                Name = productName,
                                Price = productPrice,
                                Description = productDescription,
                                Color = productColor,
                                Count = productCount,
                                CategoryId = id
                            });

                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                            goto Id;
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
            foreach (var item in await _productServices.GetAllAsync())
            {
                ConsoleColor.Blue.WriteConsole($"{item.Id}-ProductName:{item.Name} ProductPrice:{item.Price} ProductDescription:{item.Description} ProductColor:{item.Color} ProductCount:{item.Count} ProductCategoryId:{item.CategoryId} CreatedDate:{item.CreatedDate} ");

            }
            Console.WriteLine("Enter the product id:");
        Id: string idStr = Console.ReadLine();

            bool isCorrectIdFormat = int.TryParse(idStr, out int id);
            if (isCorrectIdFormat)
            {
                
                var response=await _productServices.GetByIdAsync(id);

                if (response == null)
                {
                    ConsoleColor.Red.WriteConsole("Data not found");
                    goto Id;
                }




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
        ProductId: string idStr = Console.ReadLine();

            bool isCorrectIdForma = int.TryParse(idStr, out int id);

            if (isCorrectIdForma)
            {
                var response = await _productServices.GetByIdAsync(id);

                if (response == null)
                {
                    ConsoleColor.Red.WriteConsole("Data not found");
                    goto ProductId;

                }
                ConsoleColor.Blue.WriteConsole(response.Name);





            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto ProductId;
            }
        }
        public async Task SearchByColorAsync()
        {
            Console.WriteLine("Enter the searchtext:");
        Search: string searchText = Console.ReadLine();

            string symbols = @"^[\p{L}\p{M}' \.\-]+$";

            if (string.IsNullOrEmpty(searchText.Trim()) || !Regex.IsMatch(searchText,symbols) || !searchText.Any(char.IsLetter))
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
                ConsoleColor.Blue.WriteConsole($"CategoryName{item.Name}- {item.Id}");
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
        public async Task FilterByCategoryNameAsync()
        {
            Console.WriteLine("Enter the product name:");
        FilterName: string productName = Console.ReadLine();

            if (string.IsNullOrEmpty(productName.Trim()))
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                goto FilterName;
            }
            var result=await _productServices.FilterByCategoryNameAsync(productName);
            if (result == null)
            {
                ConsoleColor.Red.WriteConsole("Data not found");
            }
            else
            {
                foreach (var item in result)
                {
                    ConsoleColor.Blue.WriteConsole($"{item.Id}-ProductName:{item.Name} ProductPrice:{item.Price} ProductDescription:{item.Description} ProductColor:{item.Color} ProductCount:{item.Count} ProductCategoryId:{item.CategoryId} CreatedDate:{item.CreatedDate} ");

                }
            }


        }

        public async Task UpdateAsync()
        {
            foreach (var item in await _productServices.GetAllAsync())
            {
                ConsoleColor.Blue.WriteConsole($"{item.Id}-ProductName:{item.Name} ProductPrice:{item.Price} ProductDescription:{item.Description} ProductColor:{item.Color} ProductCount:{item.Count} ProductCategoryId:{item.CategoryId} CreatedDate:{item.CreatedDate} ");



            }
            Console.WriteLine("Enter the product id:");
            ProductId: string productIdStr =Console.ReadLine();

            bool isCorrectIdFormat=int.TryParse(productIdStr, out int productId);

            if (isCorrectIdFormat)
            {
                var reponse =await _productServices.GetByIdAsync(productId);
                if(reponse == null)
                {
                    ConsoleColor.Red.WriteConsole("Data not found");
                    goto ProductId;
                }
                
                    var data =await _productServices.GetAllAsync();
                    Console.WriteLine("Enter the new productName: ");
                    ProductName: string newProductName=Console.ReadLine();

                    string symbols = @"^[\p{L}\p{M}' \.\-]+$";

                     if (!Regex.IsMatch(newProductName, symbols))
                     {
                        if (!string.IsNullOrEmpty(newProductName.Trim()))
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto ProductName;
                        }
                     }
                    

                    foreach (var item in data)
                    {
                        if (item.Name == newProductName.ToLower().Trim())
                        {
                            ConsoleColor.Red.WriteConsole("Data exist");
                            goto ProductName;
                        }
                    }

                    Console.WriteLine("Enter the new product price:");
                    ProductPrice: string newProductPriceStr =Console.ReadLine();

                    bool isCorrectPriceFormat = float.TryParse(newProductPriceStr, out float newProductPrice);

                    if (isCorrectPriceFormat)
                    {
                        if(newProductPrice <= 0)
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto ProductPrice;

                        }

                        Console.WriteLine("Enter the new product descpriton:");
                        ProductDescription: string newProductDescption =Console.ReadLine();
                        if (!Regex.IsMatch(newProductDescption, symbols))
                        {
                            if (!string.IsNullOrEmpty(newProductDescption.Trim()))
                            {
                                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                                goto ProductDescription;
                            }
                        }
                        

                        Console.WriteLine("Enter the new product color:");
                        ProductColor: string newProductColor =Console.ReadLine();
                        if (!Regex.IsMatch(newProductColor, symbols) || !newProductColor.Any(char.IsLetter))
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto ProductColor;
                        }
                      

                        Console.WriteLine("Enter the new product count:");
                        ProductCount: string newProductCountStr =Console.ReadLine();

                        bool isCorrectCountFormat = int.TryParse(newProductCountStr, out int newProductCount);

                        if (isCorrectCountFormat)
                        {
                            
                            if (newProductCount <= 0)
                            {
                                ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                                goto ProductCount;

                            }
                            var response = await _categoryServices.GetAllAsync();
                            foreach (var item in response)
                            {
                                ConsoleColor.Blue.WriteConsole($"{item.Id} {item.Name}");
                            }
                            Console.WriteLine("Choose CategoryId");
                        Id: string idStr = Console.ReadLine();
                            if (string.IsNullOrEmpty(idStr))
                            {
                                ConsoleColor.Red.WriteConsole("This can not be empty");
                                goto Id;
                            }



                            bool isCorrectCategoryIdFormat = int.TryParse(idStr, out int id);
                            if (isCorrectCategoryIdFormat)
                            {
                                if (id <= 0)
                                {
                                    ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                                    goto Id;
                                }

                                Category category = await _categoryServices.GetByIdAsync(id);
                                if (category == null)
                                {
                                    ConsoleColor.Red.WriteConsole("Category not found");
                                    return;
                                }
                                await _productServices.UpdateAsync(id, new Product
                                {
                                    Name = newProductName,
                                    Price = newProductPrice,
                                    Description = newProductDescption,
                                    Color = newProductColor,
                                    Count = newProductCount
                                });
                                ConsoleColor.Green.WriteConsole(SuccesfullMessages.SuccessfullOperation);


                            }
                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                            goto ProductCount;
                        }
                        

                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole(ErrorMessages.FormatWrong);
                        goto ProductPrice;
                    }




                
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ErrorMessages.WrongInput);
                goto ProductId;
            }
        }

    }
}
