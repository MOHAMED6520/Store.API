using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.UnAuthorizedException
{
    public class UnAuthorizedException (string massege = "Invalid Email Or Password"):Exception(massege)
    {
    }
}
