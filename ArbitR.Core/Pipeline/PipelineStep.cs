using System.Collections.Generic;

namespace ArbitR.Core.Pipeline
{
    public class PipelineStep
    {
        public PipelineStepHandle Handle { get; }
        public IEnumerable<object?> Args { get; }
        
        public PipelineStep(PipelineStepHandle handle, IEnumerable<object?> args)
        {
            Handle = handle;
            Args = args;
        }
    }
}