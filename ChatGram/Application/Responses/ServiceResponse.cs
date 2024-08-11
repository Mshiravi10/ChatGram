using Application.Responses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseStatusCode ResponseStatusCode { get; set; }
    }

}
