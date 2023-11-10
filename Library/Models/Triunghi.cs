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
    public class Triunghi
    {
        #region Parametri
        public bool isVisible;
        public bool uniColor;
        public Color ver1Color;
        public Color ver2Color;
        public Color ver3Color;
        #endregion

        #region Constructori
        public Triunghi() {
            isVisible = true;
            uniColor = false;
            ver1Color = RandomThings.RandomThings.GetRandomColor(10);
            ver2Color = RandomThings.RandomThings.GetRandomColor(100);
            ver3Color = RandomThings.RandomThings.GetRandomColor(1000);
        }
        public Triunghi( Color color)
        {
            isVisible = true;
            uniColor = true;
            ver1Color = color;
            ver2Color = color;
            ver3Color = color;
        }
        public Triunghi( Color ver1Color, Color ver2Color, Color ver3Color)
        {
            isVisible = true;
            uniColor = false;
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

                if(!uniColor)
                    GL.Color4(ver2Color);
                GL.Vertex3( 1, 0, 0);

                if (!uniColor)
                    GL.Color4(ver3Color);
                GL.Vertex3(0, 0, 1);

                GL.End();
            }
        }
        public override string ToString()
        {
            return !isVisible ? "" : uniColor?
                "\nCuloare triunghi: "+ ver1Color.ToString() + "\n" : 
                "Culori triunghi:\nVer1: " + ver1Color.ToString() +
                "\nVer2: " + ver2Color.ToString() +
                "\nVer3: " + ver3Color.ToString() + "\n";
        }
        #endregion
    }
}
