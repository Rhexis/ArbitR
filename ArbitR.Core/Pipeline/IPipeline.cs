using System.Collections.Generic;

namespace ArbitR.Core.Pipeline
{
    public interface IPipeline
    {
        IEnumerable<PipelineStep> Steps { get; }
    }
}