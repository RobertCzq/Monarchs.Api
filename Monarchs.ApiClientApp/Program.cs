using Monarchs.ApiClientApp.Utils;
using Monarchs.Common.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.ApiClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("##################################################");
                Console.WriteLine("Type in one of the following options to call api:");
                Console.WriteLine(StringConstansts.Login);
                Console.WriteLine(StringConstansts.GetNumberOfMonarchs);
                Console.WriteLine(StringConstansts.GetLongestRulingMonarch);
                Console.WriteLine(StringConstansts.GetLongestRulingHouse);
                Console.WriteLine(StringConstansts.GetMostCommonFirstName);
                Console.WriteLine(StringConstansts.Exit);
                Console.WriteLine("##################################################");
                Console.WriteLine("Typed values are case sensitive");
                Console.WriteLine("For the following endpoints you have to login first: ");
                Console.WriteLine("GetLongestRulingMonarch, GetLongestRulingHouse, GetMostCommonFirstName");
                Console.WriteLine("##################################################");

                var run = true;
                using (HttpClient client = new())
                {
                    var clientHelper = new HttpClientHelper(client);
                    while (run)
                    {
                        Console.WriteLine("Please type your option:");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case StringConstansts.Login:
                                Console.WriteLine(await clientHelper.Login());
                                break;
                            case StringConstansts.GetNumberOfMonarchs:
                                Console.WriteLine(await clientHelper.GetNumberOfMonarchs());
                                break;
                            case StringConstansts.GetLongestRulingMonarch:
                                Console.WriteLine(await clientHelper.GetLongestRuler());
                                break;
                            case StringConstansts.GetLongestRulingHouse:
                                Console.WriteLine(await clientHelper.GetLongestRulingHouse());
                                break;
                            case StringConstansts.GetMostCommonFirstName:
                                Console.WriteLine(await clientHelper.GetMostCommonFirstName());
                                break;
                            case StringConstansts.Exit:
                                run = false;
                                break;
                            default:
                                Console.WriteLine("Command is not recognized");
                                break;
                        }
                        Console.WriteLine("##################################################");
                    }
                }
                    
            }
            catch (Exception e) 
            {
                Console.WriteLine("Console app has encountered an unexepected error with the following message " + e.Message);
            }

            Console.WriteLine("Press any key to close console");
            Console.ReadKey();
        }
    }
}
