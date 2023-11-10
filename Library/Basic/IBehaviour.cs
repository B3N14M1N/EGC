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
    // O interfata pentru obiecte care pot fi modificate 
    // in fiecare Frame
    public interface IBehaviour
    {
        void Start();
        void Update();
    }
}
