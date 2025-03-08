using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Banking.Core.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement>
        MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
    
    public static IRuleBuilderOptionsConditions<T, TProperty>
        MustBeEnum<T, TProperty, TEnum>(
            this IRuleBuilder<T, TProperty> ruleBuilder) where TEnum : Enum
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (!Enum.TryParse(typeof(TEnum), value!.ToString(), out var result))
            {
                context.AddFailure(Errors.General.ValueIsInvalid().Serialize());
            }
        });
    }
    
}