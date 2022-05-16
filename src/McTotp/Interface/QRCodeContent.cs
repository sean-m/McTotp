using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTotp.Interface
{
    internal interface QRContent
    {
        string content { get; }
        IEnumerable<string> allContent { get; }
    }
}
