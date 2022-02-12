using System.Threading;

namespace Test.It.While.Hosting.Your.Web.Application
{
    /// <summary>
    /// Settings for the application specification
    /// </summary>
    public sealed class Settings
    {
        public Settings(
            CancellationToken executionCancellationToken = default, 
            CancellationToken stoppingCancellationToken = default, 
            bool stopAutomatically = true)
        {
            ExecutionCancellationToken = executionCancellationToken;
            StoppingCancellationToken = stoppingCancellationToken;
            StopAutomatically = stopAutomatically;
        }

        /// <summary>
        /// Cancels the starting of the test host and the When method
        /// </summary>
        public CancellationToken ExecutionCancellationToken { get; }
        
        /// <summary>
        /// Cancels stopping the host
        /// </summary>
        public CancellationToken StoppingCancellationToken { get; }

        /// <summary>
        /// If set stops the host automatically after When.
        /// When not set it is up to the test to stop the host.
        /// </summary>
        public bool StopAutomatically { get; }
    }
}