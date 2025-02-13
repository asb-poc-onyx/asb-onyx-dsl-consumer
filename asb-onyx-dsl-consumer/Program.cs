// See https://aka.ms/new-console-template for more information
using asb_onyx_dsl_consumer;
using Confluent.Kafka;

class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("asb-onyx-dsl-consumer 1.0");
        try
        {
            Secrets? secrets = Secrets.Instance("run/secrets.json");
            if (null != secrets)
            {
                string topic = secrets.topic;
                var config = new ConsumerConfig
                {
                    BootstrapServers = secrets.broker_address,
                    GroupId = secrets.consumer_group_id
                };
                // Set up the basic Auth if configured
                if (null != secrets.BasicAuthConfig)
                {
                    int idx = secrets.BasicAuthConfig.IndexOf(':');
                    string user = secrets.BasicAuthConfig.Substring(0, idx);
                    string password = secrets.BasicAuthConfig.Substring(idx + 1);
                    config.SecurityProtocol = SecurityProtocol.SaslSsl;
                    config.SaslMechanism = SaslMechanism.Plain;
                    config.SaslUsername = user;
                    config.SaslPassword = password;
                }
                Console.WriteLine("Consuming from " + topic + " on " + secrets.broker_address);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}