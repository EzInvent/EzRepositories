using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class CloseIssueRequest
    {
        public string  State{ get; set; }
        public string State_Reason { get; set; }
    }
}
