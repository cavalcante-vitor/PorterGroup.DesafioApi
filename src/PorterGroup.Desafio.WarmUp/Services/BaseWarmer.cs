using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace PorterGroup.Desafio.WarmUp.Services
{
    [ExcludeFromCodeCoverage]
    internal abstract class BaseWarmer
    {
        private readonly Action<string> _logClass;

        protected BaseWarmer(Action<string> logClass) =>
            _logClass = logClass;

        public abstract Task ExecuteAsync();

        protected void Log(string className)
        {
            if (_logClass is null)
            {
                return;
            }

            _logClass(className);
        }
    }
}
