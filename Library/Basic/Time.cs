using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* CIOBAN BENIAMIN
* 3134A
*/
namespace CIOBAN.Librarie.Basic
{
    // O clasa statica pentru apelarea variabilei
    // care indica timpul trecut dintre frame-uri
    // este modificata in "OnUpdateFrame()"
    public static class Time
    {
        public static double deltaTime { get; set; }
    }
}
