using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/1").Result;

            if(response.IsSuccessStatusCode)
            {
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Pokemon pokemonResponse = response.Content.ReadAsAsync<Pokemon>().Result;
                Console.WriteLine(pokemonResponse.name);

                foreach(var ability in pokemonResponse.abilities)
                {
                    Console.WriteLine(ability.ability.name);
                }
            }

            POKEAPIService service = new POKEAPIService();
            Pokemon numberTwo = service.GetPokemonAsync("https://pokeapi.co/api/v2/pokemon/807").Result;
            if (numberTwo != null)
            {
                Console.WriteLine(numberTwo.name);
            }


            Pokemon anotherPokemon = service.GetAsync<Pokemon>("https://pokeapi.co/api/v2/pokemon/37").Result;
            var test = service.GetAsync<Move>("https://pokeapi.co/api/v2/pokemon/37").Result;
            Console.WriteLine(anotherPokemon.name);
            Console.WriteLine(test.move);


            var listOfPokemon = service.GetAsync<ListOfPokemon>("https://pokeapi.co/api/v2/pokemon?offset=0&limit=10").Result;
            foreach(var pokemon in listOfPokemon.results)
            {
                Console.WriteLine( pokemon.name );
                Console.WriteLine("-------");
                var onePokemon = service.GetAsync<Pokemon>(pokemon.url).Result;
                foreach(var move in onePokemon.moves)
                {
                    Console.WriteLine(move.move.name);
                }
                Console.WriteLine("-------");

            }

            Console.ReadKey();
        }
    }
}
