using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Take.Blip.Client.Activation;

namespace Take.Blip.Builder.Variables
{
    public class ApplicationVariableProvider : IVariableProvider
    {
        private readonly Application _application;

        public ApplicationVariableProvider(Application application)
        {
            _application = application;
        }
        
        public VariableSource Source => VariableSource.Application;
        
        public Task<string> GetVariableAsync(string name, IContext context, CancellationToken cancellationToken)
        {
            return GetVariable(name).AsCompletedTask();
        }

        private string GetVariable(string name)
        {
            switch (name.ToLowerInvariant())
            {
                case "identifier":
                    return _application.Identifier;
                
                case "domain":
                    return _application.Domain;
                
                case "instance":
                    return _application.Instance;
                
                case "identity":
                    return _application.Identity;                
                
                case "node":
                    return _application.Node;
                
                case "settings":
                    return _application.Settings != null
                        ? JsonSerializer.Serialize(_application.Settings, Application.JsonSerializerOptions)
                        : null;
                
                default:
                    return null;
                
            }
        }
    }
}