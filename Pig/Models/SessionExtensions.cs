using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Pig.Models
{
    public static class SessionExtensions
    {
        public static void setObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T getObject<T>(this ISession session, string key)
        {
            string stringObject = session.GetString(key);

            if (string.IsNullOrEmpty(stringObject))
            {
                return default(T); 
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(stringObject);
            }
            
        }

    }
}
