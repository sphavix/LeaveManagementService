﻿
using FluentValidation.Results;

namespace HRLeaveManagement.Application.ErrorHandling
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            
        }

        public BadRequestException(string message, ValidationResult validationResult): base(message)
        {
            ValidationErrors = new();

            foreach(var error in validationResult.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
        }

        public List<string> ValidationErrors { get; set; }
    }
}
