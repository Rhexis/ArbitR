namespace ArbitR.Pipeline.Configuration
{
    public class Config
    {
        public int QueueCapacity { get; set; } = 1000;
        public QueueType QueueType { get; set; } = QueueType.Synchronous;
    }
}