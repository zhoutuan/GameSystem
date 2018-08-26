using System;
using System.Collections.Generic;

namespace GameSystem.Models
{
    public class PaginResult<T> where T : class
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public int Count { get; set; }

        public List<T> Data { get; set; }
    }
}
