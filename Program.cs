using RestSharp;
using System;
using System.IO;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string projDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            String path = projDir + @"\FetchWebsiteInfo.xml";

            Methods method = new Methods();

            //Add attribute to xml
            method.CreateAttribute(path);

            //De-serialize XML
            Websites webs = method.Deserialize(path);

            //Convert XML to JSON
            string json = method.ConvertToJson(webs);

            //Print information of the website
            method.PrintData(json);

            //Get all websites that were created after September 1
            method.GetDataWithCreatedDateAfterSpesificMonth(json, 9, 1);

            string userName = "Marwa" + DateTime.Now;
            string password = "1234";
            string baseUrl = "https://api.demoblaze.com";

            //post signup Api
            method.SignUp(baseUrl, userName, password);

            //post login Api
            method.Login(baseUrl, userName, password);

            RestClient client = new RestClient("http://jsonplaceholder.typicode.com");
            RestRequest request = new RestRequest("/users/3", Method.GET);


            // request.AddUrlSegment("token", "saga001"); 


            var queryResult = client.ExecuteAsync(request);

            Console.WriteLine(queryResult);

        }
    }
    
}
