using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.Responses
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public Response(T? data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
}
