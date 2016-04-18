using Castle.Core.Logging;
using MSP.Core.Dependency;

namespace MSP.Core.Log
{
    public interface IMSPLogger:ILogger, ITransientDependency
    {
    }
}
