﻿using ProjectTemplate_v2.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProjectTemplate_v2
{
    public static class HttpService
    {
        private static HttpClient _client;
        public static List<SensorModel> SensorList { get; set; }
        public static bool IsInitialized { get; private set; }

        public static void InitializeClient()
        {

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.TryAddWithoutValidation("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
            try
            {
                SensorList = JsonConvert.DeserializeObject<List<SensorModel>>(GetList().Result);
                IsInitialized = true;
            }
            catch
            {
                SensorList = new List<SensorModel>();
                IsInitialized = false;
            }
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

        public static async Task<ValueModel> GetValueAsync(string sensorId)
        {
            ValueModel vm = null;
            string url = "http://telerikacademy.icb.bg/api/sensor/" + sensorId;

            HttpResponseMessage response = await _client.GetAsync(url).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                vm = await response.Content.ReadAsAsync<ValueModel>().ConfigureAwait(false);

            }

            return vm;

        }
    }
}
