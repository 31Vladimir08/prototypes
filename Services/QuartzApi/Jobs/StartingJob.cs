using Confluent.Kafka;
using Newtonsoft.Json;

using Quartz;

namespace QuartzApi.Jobs
{
    public class StartingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
            //string serializedData = JsonConvert.SerializeObject("");

            //var topic = _config.GetSection("TopicName").Value;

            //using (var producer = new ProducerBuilder<Null, string>(_configuration).Build())
            //{
            //    await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedData });
            //    producer.Flush(TimeSpan.FromSeconds(10));
            //    return Ok(true);
            //}
        }
    }
}
