using System;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using FlitBit.Core;
using FlitBit.Emit;
using Newtonsoft.Json;

namespace FlitBit.WebApi.ModelBinding
{
    public class WebApiDtoModelBinder : IModelBinder
    {
        static readonly MethodInfo TransformToModelMethod = typeof(WebApiDtoModelBinder).MatchGenericMethod("TransformToModel", BindingFlags.Instance | BindingFlags.NonPublic, 1, typeof(object), typeof(string));

        readonly JsonSerializerSettings _settings;
        public WebApiDtoModelBinder()
        {
            _settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new[] { new DtoConverter() }
            };
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if ((bindingContext.ModelType.IsAbstract || bindingContext.ModelType.IsInterface))
            {
                var json = actionContext.Request.Content.ReadAsStringAsync().Result;
                var trans = TransformToModelMethod.MakeGenericMethod(bindingContext.ModelType);
                var model = trans.Invoke(this, new[] { json });
                if (model != null)
                {
                    bindingContext.Model = model;
                    return true;
                }
            }

            return false;
        }

        T TransformToModel<T>(string json)
        {
            var currentFactory = FactoryProvider.Factory;

            return currentFactory.CanConstruct<T>() ?
                JsonConvert.DeserializeObject<T>(json, _settings) : default(T);
        }
    }
}
