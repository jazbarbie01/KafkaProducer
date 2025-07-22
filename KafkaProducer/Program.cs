using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var loanSurvey = new LoanSurvey
                    {
                        LoanRecordId = 1,
                        SurveyResults = new List<SurveyResult>
                        {
                            new SurveyResult
                            {
                                CustomerRecordId = 101,
                                Rating = 5,
                                SurveyIssuedDate = DateTime.UtcNow,
                                SurveyReceivedDate = DateTime.UtcNow
                            }
                        }
                    };

                    string jsonMessage = JsonSerializer.Serialize(loanSurvey);

                    var result = await producer.ProduceAsync("loan-surveys", new Message<Null, string> { Value = jsonMessage });
                    Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
