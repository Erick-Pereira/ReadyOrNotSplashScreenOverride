using SomethingSomethingReadyOrNot.Shared;
using System;

namespace SomethingSomethingReadyOrNot
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1:Override Image \r\n 2:Continue");
                Console.Write("Option: ");
                string option = Console.ReadLine().Replace("Option: ", "");
                switch (option)
                {
                    case "1":
                        Response response = ExternalFunctions.OverrideImage();
                        if (!response.HasSuccess)
                        {
                            Console.WriteLine(response.Message);
                        }
                        break;

                    case "2":
                        if (!FileSomething.HasFoundPath)
                        {
                            Console.WriteLine("Select Ready or Not Game Folder");
                            Console.WriteLine(@"Normal Path: C:\Program Files (x86)\Steam\steamapps\common\Ready Or Not");
                            Response response2 = ExternalFunctions.OverrideSplashScreen();
                            if (!response2.HasSuccess)
                            {
                                Console.WriteLine(response2.Message);
                            }
                        }
                        else
                        {
                            Response response2 = ExternalFunctions.OverrideSplashScreen();
                            if (!response2.HasSuccess)
                            {
                                Console.WriteLine(response2.Message);
                            }
                        }
                        break;
                }
            }
        }
    }
}