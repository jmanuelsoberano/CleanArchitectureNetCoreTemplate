using FluentValidation.Results;

namespace CleanArchitectureNetCore.Application.Common;

public static class ValidationResultExtension
{
    public static string ErrorMessages(this ValidationResult validationResult)
    {
        return string.Join(",", validationResult.Errors.Select(s => s.ErrorMessage));
    }
}