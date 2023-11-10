using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* CIOBAN BENIAMIN
* 3134A
*/
namespace CIOBAN.Librarie
{
    // O interfata pentru implementarea obiectelor
    // care pot fi randate
    public interface IRenderer
    {
        void Draw();
        void SetColors(List<Color> colors);
    }
}
