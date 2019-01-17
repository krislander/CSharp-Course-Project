using ProjectTemplate_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Windows;
using Newtonsoft.Json;

namespace ProjectTemplate_v2
{
    public static class HttpService
    {
        private static HttpClient _client;
        public static List<SensorModel> SensorList { get; set; }

        public static void InitializeClient()
        {

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.TryAddWithoutValidation("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
            SensorList = JsonConvert.DeserializeObject<List<SensorModel>>(GetList().Result);

            //string names = "";

            //foreach (var sensor in sensorList)
            //{
            //    names += sensor.Tag;
            //}
            //MessageBox.Show(names);
        }

        public static async Task<string> GetList()
        {
            string list = "";
            string url = "http://telerikacademy.icb.bg/api/sensor/all";

            HttpResponseMessage response = await _client.GetAsync(url).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return list;
        }

        public static async Task<double> GetValueAsync(string sensorId)
        {
            ValueModel vm = null;
            string url = "http://telerikacademy.icb.bg/api/sensor/"+sensorId;

            HttpResponseMessage response = await _client.GetAsync(url).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                vm = await response.Content.ReadAsAsync<ValueModel>().ConfigureAwait(false);

            }

            return Convert.ToDouble(vm.Value);
        }

        //public static async Task<double> GetProductValueAsync(string path)
        //{
        //    SensorModel sensor = null;

        //    //await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

        //    HttpResponseMessage responseMessage = await _client.GetAsync(path).ConfigureAwait(false);

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        sensor = await responseMessage.Content.ReadAsAsync<SensorModel>().ConfigureAwait(false);
        //    }
        //    else
        //    {
        //        throw new Exception(responseMessage.ReasonPhrase);
        //    }

        //    return sensor.Value;
        //}

    }
}
