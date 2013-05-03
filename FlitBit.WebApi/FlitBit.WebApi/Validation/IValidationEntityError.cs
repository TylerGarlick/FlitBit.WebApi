using FlitBit.Dto;

namespace FlitBit.WebApi
{
    [DTO]
    public interface IValidationEntityError
    {
        string Message { get; set; }
        string PropertyName { get; set; }
    }
}
