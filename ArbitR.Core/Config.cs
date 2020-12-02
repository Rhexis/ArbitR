namespace ArbitR.Core
{
    internal static class Config
    {
        private static bool _configured;
        public static bool RaiseAuditEvent { get; private set; }

        public static void Configure(bool raiseAuditEvent)
        {
            if (_configured) return;
            _configured = true;
            RaiseAuditEvent = raiseAuditEvent;
        }
    }
}