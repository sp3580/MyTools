using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BusinessModels.UserModels
{
    public enum UserStatus
    {
        enabled = 1,
        disabled = 2,
        delete = 3,
    }
    public enum UserRole
    {
        normal = 1,
        manage = 2,
    }
}
