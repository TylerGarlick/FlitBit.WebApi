using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using FlitBit.WebApi.ModelBinding;

namespace FlitBit.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class InterfaceFromBodyAttribute : ModelBinderAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            parameter.BindWithModelBinding(new WebApiDtoModelBinder());
            return base.GetBinding(parameter);
        }
    }
}