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
    public class Cub
    {
        #region Parametri
        public bool isVisible;
        public Vector3 Position;
        private float lenght;
        public float Lenght { 
            get { return lenght; }
            set { lenght = value > 0 ? value : 1f; }
        }

        public Color Top;
        public Color Bottom;
        #endregion
        #region Constructori
        public Cub(float lenght)
        {
            Lenght= lenght;
            Top = Color.Red;
            Bottom = Color.Blue;
            isVisible = true;
        }
        public Cub()
        {
            Position = new Vector3();
            Lenght = 1f;
            Top = Color.Red;
            Bottom = Color.Blue;
            isVisible = true;
        }
        public Cub(Vector3 position, float lenght)
        {
            Position = position;
            Lenght = lenght;
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
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z + lenght / 2);
                GL.Color3(Bottom);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);
                // Y-
                GL.Color3(Bottom);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);
                // Z-
                GL.Color3(Top);
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y + lenght / 2, Position.Z - lenght / 2);
                GL.Color3(Bottom);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);

                // X+
                GL.Color3(Top);
                GL.Vertex3(Position.X + lenght/2, Position.Y + lenght/2, Position.Z + lenght/2);
                GL.Vertex3(Position.X + lenght/2, Position.Y + lenght/2, Position.Z - lenght/2);
                GL.Color3(Bottom);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);
                // Z+
                GL.Color3(Top);
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y + lenght / 2, Position.Z + lenght / 2);
                GL.Color3(Bottom);
                GL.Vertex3(Position.X + lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y - lenght / 2, Position.Z + lenght / 2);

                // Y+
                GL.Color3(Top);
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z - lenght / 2);
                GL.Vertex3(Position.X - lenght / 2, Position.Y + lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y + lenght / 2, Position.Z + lenght / 2);
                GL.Vertex3(Position.X + lenght / 2, Position.Y + lenght / 2, Position.Z - lenght / 2);

                GL.End();
            }
        }
        #endregion
    }
}
