using System.Web;
using FlitBit.IoC;

namespace FlitBit.WebApi.IoC
{
    public class ContainerScopePerRequestHttpModule : IHttpModule
    {
        IContainer _container;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) => _container = Container.Current.MakeChildContainer(CreationContextOptions.InheritScope);
            context.EndRequest += (sender, args) => _container.Dispose(); ;
        }

        public void Dispose()
        {
        }
    }
}