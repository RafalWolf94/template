using P3Model.Annotations.Domain.DDD;

namespace Web.Domain.Models.ValueObjects;

[DddValueObject]
public interface IValueObject<T>
{
    T Value { get; init; }
}