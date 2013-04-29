using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FlitBit.WebApi.ModelBinding
{
    public class WebApiDtoModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new WebApiDtoModelBinder();
        }
    }
}