using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Task
{
    class Methods
    {
        /// <summary>
        /// De-serialize xml to object
        /// </summary>
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message, DateTime.Now);
            }
            return returnObject;
        }

        /// <summary>
        /// add attribute to xml root
        /// </summary>
        public void CreateAttribute(string path)
        {
            XmlDocument docXML = new XmlDocument();

            if (File.Exists(path))
            {
                // Open the XML file
                docXML.Load(path);

                // Create an attribute and add it to the root element
                docXML.DocumentElement.SetAttribute("FileDesc",
                                   DateTime.Now.ToString());
                docXML.Save(path);
            }
        }

        /// <summary>
        /// De-serialize XML
        /// </summary>
        public Websites Deserialize(string path)
        {
            return DeserializeXMLFileToObject<Websites>(path);
        }

        /// <summary>
        /// convert to json string
        /// </summary>
        public string ConvertToJson(Websites webs)
        {
            return JsonConvert.SerializeObject(webs);
        }

        /// <summary>
        /// Print data on the file 
        /// </summary>
        public void PrintData(string json)
        {
            var o = JObject.Parse(json);
            JArray items = (JArray)o["Website"];
            int length = items.Count;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("Website " + (i + 1));
                Console.WriteLine("URL: " + (string)o.SelectTokens("$..Website[" + i + "].URL").First());
                Console.WriteLine("Title: " + (string)o.SelectTokens("$..Website[" + i + "].Tile").First());
                Console.WriteLine("Description: " + (string)o.SelectTokens("$..Website[" + i + "].Description").First());
                Console.WriteLine("Created Date: " + (string)o.SelectTokens("$..Website[" + i + "].CreatedDate").First());
            }
        }

        public void GetDataWithCreatedDateAfterSpesificMonth(string json, int month, int day)
        {
            var o = JObject.Parse(json);
            JArray items = (JArray)o["Website"];
            int length = items.Count;
            //Get all websites that were created after September 1
            for (int i = 0; i < length; i++)
            {
                DateTime date = DateTime.Parse((string)o.SelectTokens("$..Website[" + i + "].CreatedDate").First());
                if (date.Month >= month && date.Day>= day)
                {
                    Console.WriteLine("Website " + (i + 1));
                    Console.WriteLine("URL: " + (string)o.SelectTokens("$..Website[" + i + "].URL").First());
                    Console.WriteLine("Title: " + (string)o.SelectTokens("$..Website[" + i + "].Tile").First());
                    Console.WriteLine("Description: " + (string)o.SelectTokens("$..Website[" + i + "].Description").First());
                    Console.WriteLine("Created Date: " + (string)o.SelectTokens("$..Website[" + i + "].CreatedDate").First());
                }
            }
        }

        public void Login(string apiUrl, string userName, string password)
        {
            RestClient client = new RestClient(apiUrl);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", userName);
            jObjectbody.Add("password", password);

            RestRequest restRequest = new RestRequest("/login", Method.POST);
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse restResponse = client.Execute(restRequest);
            string response = restResponse.Content;
            if (!response.Contains("Auth_token"))
            {
                throw new InvalidOperationException("login is failed");
            }
        }

        public void SignUp(string apiUrl, string userName, string password)
        {
            RestClient client = new RestClient(apiUrl);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", userName);
            jObjectbody.Add("password", password);

            RestRequest restRequest = new RestRequest("/signup", Method.POST);
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse restResponse = client.Execute(restRequest);
            string response = restResponse.Content;
            if (response.Contains("errorMessage"))
            {
                throw new InvalidOperationException("signup is failed");
            }
        }


    }
}
