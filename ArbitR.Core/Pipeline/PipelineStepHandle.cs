using System;

namespace ArbitR.Core.Pipeline
{
    public class PipelineStepHandle
    {
        public Type Service { get; }
        public string Method { get; }
        
        public PipelineStepHandle(Type service, string method)
        {
            Service = service;
            Method = method;
        }
    }
}