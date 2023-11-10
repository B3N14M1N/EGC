using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIOBAN.Librarie.RandomThings;
/*
 * CIOBAN BENIAMIN
 * 3134A
 */
namespace CIOBAN.Librarie
{
    // L3
    // Un model 3d triunghi
    // caruia i se pot modifica culorile
    public class Triunghi: IRenderer
    {
        #region Parametri
        public bool isVisible;
        public Color ver1Color;
        public Color ver2Color;
        public Color ver3Color;
        #endregion

        #region Constructori
        public Triunghi() {
            isVisible = true;
            ver1Color = RandomThings.RandomGenerator.GetRandomColor(10);
            ver2Color = RandomThings.RandomGenerator.GetRandomColor(100);
            ver3Color = RandomThings.RandomGenerator.GetRandomColor(1000);
        }
        public Triunghi( Color color)
        {
            isVisible = true;
            ver1Color = color;
            ver2Color = color;
            ver3Color = color;
        }
        public Triunghi( Color ver1Color, Color ver2Color, Color ver3Color)
        {
            isVisible = true;
            this.ver1Color = ver1Color;
            this.ver2Color = ver2Color;
            this.ver3Color = ver3Color;
        }
        #endregion
        #region Metode

        public void Draw()
        {
            if(isVisible)
            {
                GL.Begin(PrimitiveType.Triangles);
                GL.Color4(ver1Color);
                GL.Vertex3(0, 1, 0);
                GL.Color4(ver2Color);
                GL.Vertex3( 1, 0, 0);
                GL.Color4(ver3Color);
                GL.Vertex3(0, 0, 1);

                GL.End();
            }
        }

        public void SetColors(List<Color> colors)
        {
            if (colors == null || colors.Count == 0) return;
            if(colors.Count > 2) {
                ver1Color = colors[0];
                ver2Color = colors[1];
                ver3Color = colors[2];
                return;
            }
            ver1Color = ver2Color = ver3Color = colors[0];
        }

        public override string ToString()
        {
            return !isVisible ? "" : (ver1Color == ver2Color && ver2Color == ver3Color) ?
                "\nCuloare triunghi: "+ ver1Color.ToString() + "\n" : 
                "Culori triunghi:\nVer1: " + ver1Color.ToString() +
                "\nVer2: " + ver2Color.ToString() +
                "\nVer3: " + ver3Color.ToString() + "\n";
        }
        #endregion
    }
}
