using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Shared.ErrorsModels
{
    public class ValidationErrorResponce
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Error";
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
