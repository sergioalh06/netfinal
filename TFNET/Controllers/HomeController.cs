using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TFNET.Models;
using System.Transactions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TFNET.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult Result(string Result)
        {
            if (Result != null)
            {
                ViewBag.Result = Result;
            }

            return View();
        }

        public ActionResult RegisterUser()
        {
            var ViewModel = new Models.Input();


            ViewModel.Genders.Add("Male");
            ViewModel.Genders.Add("Female");

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult RegisterUser(Models.Input model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    model.Genders.Add("Male");
                    model.Genders.Add("Female");
                    TryUpdateModel(model);
                    return View(model);
                }

                InvokeRequestResponseService(model).Wait();
                

                return RedirectToAction("Result", new {Result = model.result });
            }
            catch (Exception ex)
            {
                model.Genders.Add("Male");
                model.Genders.Add("Female");
                TryUpdateModel(model);

                return View(model);
            }
        }

        static async Task InvokeRequestResponseService(Models.Input input)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "UserId", input.UserId
                                            },
                                            {
                                                "Age", input.Age
                                            },
                                            {
                                                "Address", input.Address
                                            },
                                            {
                                                "Gender", input.Gender
                                            },
                                            {
                                                "UserType", input.UserType
                                            },
                                            {
                                                "TransactionId", input.TransactionId
                                            },
                                            {
                                                "Timestamp", input.Timestamp
                                            },
                                            {
                                                "ItemId", input.ItemId
                                            },
                                            {
                                                "Quantity", input.Quantity
                                            },
                                            {
                                                "Value", input.Value
                                            },
                                            {
                                                "Location", input.Location
                                            },
                                            {
                                                "ProductCategory", input.ProductCategory
                                            },
                                            {
                                                "Group", input.Group
                                            },
                                            {
                                                "ChurnPeriod", input.ChurnPeriod
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "nEbVHiFhByd7Bu13U9saLXhffkcOBq6zJD0txo3jQnuv8NEwLeQdu7k5rWzZNTtGeeazkfjKFrG9AQx4+c/pdA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/11ee6507e4cc444781c42cc522ca73ba/services/75ff1f63cd054a5d833e32127feb0527/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    OutputData outputData = JsonConvert.DeserializeObject<OutputData>(result);
                    input.result =Math.Round(Convert.ToDouble(outputData.GetResponse().ScoredProbabilities)*100,0).ToString();
                    //Result: {"Results":{"output1":[{"Scored Labels":"Churner","Scored Probabilities":"0.287719696760178"}]}}
                    
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}