using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CutestCat.Repositories.Http
{
    public static class HttpHelper
    {
        public async static Task<T> Get<T>(string uri)
        {
            var response = await new HttpClient().GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
