﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Response
{
    public class ResponseDTO<T> : ResponseBasic
    {
        public T Data { get; set; }
    }
}
