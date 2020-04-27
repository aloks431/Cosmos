using CoSMoS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoSMoS.ViewModels
{
    public class SymptomsViewModel: BaseViewModel
    {
        Dictionary<string, string> statusMap = new Dictionary<string, string>
        {
            {"\"normal\"\n", "normal" },
            { "\"moderate\"\n", "moderate" },
            { "\"critical\"\n", "critical" }
        };

        Dictionary<string, Color> statusColorMap = new Dictionary<string, Color>
        {
            {"\"normal\"\n", Color.Green },
            { "\"moderate\"\n", Color.Yellow },
            { "\"critical\"\n", Color.Red }
        };

        public Symptoms Symptoms { get; set; }
        public Command CheckCommand { get; set; }

        public string status;

        public Color statusBackground;

        public string Status { 
            get 
            { 
                return status; 
            }
            set
            {
                SetProperty(ref status, value);
            }
        }

        public Color StatusBackground
        {
            get
            {
                return statusBackground;
            }
            set
            {
                SetProperty(ref statusBackground, value);
            }
        }
        public SymptomsViewModel(Symptoms symptoms = null)
        {
            Title = "Symptoms";
            Symptoms = symptoms;
        }

        public SymptomsViewModel()
        {
            Symptoms = new Symptoms();
            CheckCommand = new Command(async () => await ExecuteCheckCommand());
        }

        async Task ExecuteCheckCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://dev-dispatch-275311.ew.r.appspot.com/message");
                var json = new
                {
                    Age = Symptoms.Age,
                    BodyTemperature = Symptoms.BodyTemperature,
                    City = Symptoms.City,
                    IsCough = Symptoms.IsCough.ToString().ToLower(),
                    IsDiarrhea = Symptoms.IsDiarrhea.ToString().ToLower(),
                    IsDifficultyInBreathing = Symptoms.IsDifficultyInBreathing.ToString().ToLower(),
                    IsFatigue = Symptoms.IsFatigue.ToString().ToLower(),
                    IsFever = Symptoms.IsFever.ToString().ToLower(),
                    IsHeadaches = Symptoms.IsHeadaches.ToString().ToLower(),
                    IsRunnyNose = Symptoms.IsRunnyNose.ToString().ToLower(),
                    IsShortnessOfBreath = Symptoms.IsShortnessOfBreath.ToString().ToLower(),
                    ZipCode = Symptoms.ZipCode
                };

                string jsonData = JsonConvert.SerializeObject(json);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                var result = await response.Content.ReadAsStringAsync();

                Status = $"Your Status is {statusMap[result]}";
                StatusBackground = statusColorMap[result];

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
