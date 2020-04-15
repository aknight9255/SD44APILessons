using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    public class POKEAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient(); 
        
        public async Task<Pokemon> GetPokemonAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Pokemon pokemon = await response.Content.ReadAsAsync<Pokemon>();
                return pokemon;
            }
            return null;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default;
        }
    }
}
