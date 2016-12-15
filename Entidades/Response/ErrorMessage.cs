using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Response
{
    public class ErrorMessage
    {
        public ErrorMessage(string Code, string Mensagge)
        {
            this.Code = Code;
            this.Mensagge = Mensagge;
        }

        public string Code { get; set; }
        public string Mensagge { get; set; }
    }
}
