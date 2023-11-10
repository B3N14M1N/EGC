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
    // Un triunghi caruia i se pot schimba
    // pozitia, culoarea si vizibilitatea
    public class Triunghi
    {
        #region Parametri
        public Vector3 Position;
        public bool isVisible;
        public bool uniColor;
        public Color ver1Color;
        public Color ver2Color;
        public Color ver3Color;
        #endregion

        #region Constructori
        public Triunghi() {
            Position = Vector3.Zero;
            isVisible = true;
            uniColor = false;
            ver1Color = RandomThings.RandomThings.GetRandomColor(10);
            ver2Color = RandomThings.RandomThings.GetRandomColor(100);
            ver3Color = RandomThings.RandomThings.GetRandomColor(1000);
        }
        public Triunghi(Vector3 position)
        {
            Position = position;
            isVisible = true;
        }
        public Triunghi(Vector3 position, Color color)
        {
            Position = position;
            isVisible = true;
            uniColor = true;
            ver1Color = color;
            ver2Color = color;
            ver3Color = color;
        }
        public Triunghi(Vector3 position, Color ver1Color, Color ver2Color, Color ver3Color)
        {
            Position = position;
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
                GL.Vertex3(Position.X, Position.Y + 1, Position.Z);

                if(!uniColor)
                    GL.Color4(ver2Color);
                GL.Vertex3(Position.X + 1, Position.Y, Position.Z);

                if (!uniColor)
                    GL.Color4(ver3Color);
                GL.Vertex3(Position.X, Position.Y, Position.Z + 1);

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
