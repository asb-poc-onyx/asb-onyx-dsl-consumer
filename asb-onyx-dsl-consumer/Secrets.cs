using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace asb_onyx_dsl_consumer
{
    public class Secrets
    {
        public Secrets()
        {
        }
        public string broker_address { get; set; }
        public string topic { get; set; }
        public string consumer_group_id { get; set; }
        public string OAuthConfig { get; set; }
        public string BasicAuthConfig { get; set; }
        /// <summary>
        /// Get an instance of Secrets from the named file
        /// </summary>
        /// <param name="fileName">The name of the file to read</param>
        /// <returns>Secret values or null</returns>
        public static Secrets? Instance(string fileName)
        {
            Secrets? result = null;
            string? fileJson = File.ReadAllText(fileName);
            if (!string.IsNullOrEmpty(fileJson))
            {
                result = JsonConvert.DeserializeObject<Secrets>(fileJson);
            }
            return result;
        }
    }
}
