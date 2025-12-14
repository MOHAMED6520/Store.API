using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.ValidationException
{
    public class ValidationException: Exception
    {
        public ValidationException(string massege):base(massege)
        {
            
        }
        public ValidationException(IEnumerable<string> errors) :base (string.Join(" , ", errors))
        {

        }
        public IEnumerable<string> errors { get; set; }
    }
}
