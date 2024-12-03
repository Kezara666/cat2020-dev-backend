﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO
{
    public class ApiResponseModel<T>
    {
       public int Status {  get; set; }
       public string Message { get; set; }
       public T Data { get; set; }

    }
}
