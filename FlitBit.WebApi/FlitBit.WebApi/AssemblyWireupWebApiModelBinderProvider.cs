using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;
using FlitBit.WebApi;
using FlitBit.WebApi.IoC;
using FlitBit.WebApi.ModelBinding;
using FlitBit.Wireup;
using FlitBit.Wireup.Meta;

[assembly: WireupDependency(typeof(FlitBit.Dto.AssemblyWireup))]
[assembly: WireupDependency(typeof(FlitBit.IoC.WireupThisAssembly))]
[assembly: Wireup(typeof(AssemblyWireupWebApiModelBinderProvider))]

namespace FlitBit.WebApi
{
    public sealed class AssemblyWireupWebApiModelBinderProvider : IWireupCommand
    {
        public void Execute(IWireupCoordinator coordinator)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WebApiDtoHttpControllerActivator());
            GlobalConfiguration.Configuration.Services.Insert(typeof(ModelBinderProvider), 0, new WebApiDtoModelBinderProvider());
            GlobalConfiguration.Configuration.Formatters[0] = new DtoJsonNetFormatter();
        }
    }
}
