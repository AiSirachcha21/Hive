using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Hive.Shared.Errors
{
    public class ErrorResponse
    {
        public String Message { get; set; }
        public int ErrorCode { get; set; }
        public IList<IdentityError> Errors { get; set; }
    }
}
