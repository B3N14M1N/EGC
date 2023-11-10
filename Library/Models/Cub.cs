using OpenTK;
using OpenTK.Graphics.OpenGL;
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
    // L4 
    // Un model 3d pentru cub
    public class Cub
    {
        #region Parametri
        public bool isVisible;
        private float length;
        public float Length { 
            get { return length; }
            set { length = value > 0 ? value : 1f; }
        }

        public Color Top;
        public Color Bottom;
        #endregion
        #region Constructori
        public Cub()
        {
            Length = 1f;
            Top = Color.Red;
            Bottom = Color.Blue;
            isVisible = true;
        }
        public Cub(float lenght)
        {
            Length= lenght;
            Top = Color.Red;
            Bottom = Color.Blue;
            isVisible = true;
        }
        #endregion
        #region Metode
        public void Draw()
        {
            if(isVisible)
            {
                GL.Begin(PrimitiveType.Quads);

                // X-
                GL.Color3(Top);
                GL.Vertex3(-length / 2, length / 2, -length / 2);
                GL.Vertex3(-length / 2, length / 2, length / 2);
                GL.Color3(Bottom);
                GL.Vertex3(-length / 2, -length / 2, length / 2);
                GL.Vertex3(-length / 2, -length / 2, -length / 2);
                // Y-
                GL.Color3(Bottom);
                GL.Vertex3(-length / 2, -length / 2, -length / 2);
                GL.Vertex3(-length / 2, -length / 2, length / 2);
                GL.Vertex3(length / 2, -length / 2, length / 2);
                GL.Vertex3(length / 2, -length / 2, -length / 2);
                // Z-
                GL.Color3(Top);
                GL.Vertex3(-length / 2, length / 2, -length / 2);
                GL.Vertex3(length / 2, length / 2, -length / 2);
                GL.Color3(Bottom);
                GL.Vertex3(length / 2, -length / 2, -length / 2);
                GL.Vertex3(-length / 2, -length / 2, -length / 2);

                // X+
                GL.Color3(Top);
                GL.Vertex3(length / 2, length / 2, length / 2);
                GL.Vertex3(length / 2, length / 2, -length / 2);
                GL.Color3(Bottom);
                GL.Vertex3(length / 2, -length / 2, -length / 2);
                GL.Vertex3(length / 2, -length / 2, length / 2);
                // Z+
                GL.Color3(Top);
                GL.Vertex3(-length / 2, length / 2, length / 2);
                GL.Vertex3(length / 2, length / 2, length / 2);
                GL.Color3(Bottom);
                GL.Vertex3(length / 2, -length / 2, length / 2);
                GL.Vertex3(-length / 2, -length / 2, length / 2);

                // Y+
                GL.Color3(Top);
                GL.Vertex3(-length / 2, length / 2, -length / 2);
                GL.Vertex3(-length / 2, length / 2, length / 2);
                GL.Vertex3(length / 2, length / 2, length / 2);
                GL.Vertex3(length / 2, length / 2, -length / 2);

                GL.End();
            }
        }
        public override string ToString()
        {
            return "Culori Cub:\nTop: " + Top.ToString() +
                "\nBottom: " + Bottom.ToString() + "\n";
        }
        #endregion
    }
}
