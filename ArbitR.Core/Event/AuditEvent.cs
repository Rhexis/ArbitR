namespace ArbitR.Core.Event
{
    public sealed class AuditEvent : IEvent
    {
        public string Type { get; }
        public string Name { get; }
        public string Json { get; }
        public bool Success { get; }

        public AuditEvent(string type, string name, string json, bool success)
        {
            Type = type;
            Name = name;
            Json = json;
            Success = success;
        }
    }
}