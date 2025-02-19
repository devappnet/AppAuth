using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class ResponseBase<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } 
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
