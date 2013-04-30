using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.ModelBinding;
using FlitBit.IoC;
using FlitBit.WebApi;

public static class ValidationExtensions
{
    public static bool IsObjectValid<T>(this T entity)
    {
        return !GetValidationErrors(entity).Any();
    }

    public static IEnumerable<IValidationEntityError> GetValidationErrors<T>(this T entity)
    {
        var entityType = entity.GetType();
        var entityInterfaces = entityType.GetInterfaces();

        return from interfaces in entityInterfaces
               from prop in interfaces.GetProperties()
               let propVal = prop.GetValue(entity, null)
               from attr in prop.GetCustomAttributes(typeof(ValidationAttribute), true).Cast<ValidationAttribute>().Where(attr => !attr.IsValid(propVal))
               select Create.NewInit<IValidationEntityError>().Init(new
                                                                        {
                                                                            Message = attr.FormatErrorMessage(prop.Name),
                                                                            PropertyName = prop.Name
                                                                        });

    }

    public static void ToModelState(this IEnumerable<IValidationEntityError> errors, ModelStateDictionary modelState)
    {
        foreach (var error in errors)
            modelState.AddModelError(error.PropertyName, error.Message);
    }
}
