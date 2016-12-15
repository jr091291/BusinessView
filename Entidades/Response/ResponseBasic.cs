using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Response
{
    public class ResponseBasic
    {
        public ResponseBasic()
        {
            Mensagge = "";
            Errors = new List<ErrorMessage>();
            RowAffected = 0;
        }

        public string Mensagge { get; set; }
        public int RowAffected { get; set; }
        public List<ErrorMessage> Errors { get; set; }
    }
}
