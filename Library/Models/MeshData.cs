using CIOBAN.Librarie;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
/*
 * CIOBAN BENIAMIN
 * 3134A
 */
namespace CIOBAN.Library.Models
{
    public class MeshData:IRenderer
    {
        public List<Vector3> vertex;
        public List<Vector3> face;
        private List<Color> color;

        public bool isVisible = true;
        public bool wireframe = false;
        public MeshData() { 
            vertex = new List<Vector3>();
            face = new List<Vector3>();
            color = new List<Color>();
        }

        public MeshData(List<Vector3> Vertex, List<Vector3> Faces)
        {
            vertex = Vertex;
            face = Faces;
            color = new List<Color>();
        }
        public void Draw()
        {
            if (color.Count < face.Count)
            {
                for (int i = color.Count; i < face.Count; i++)
                {
                    color.Add(Color.Pink);
                }
            }
            if(isVisible)
            {
                for (int i = 0; i < face.Count; i++)
                {
                    GL.Begin(wireframe ? PrimitiveType.LineLoop : PrimitiveType.Triangles);
                    GL.Color3(wireframe? Color.Black : color[i]);
                    GL.Vertex3(vertex[(int)face[i].X - 1]);
                    GL.Vertex3(vertex[(int)face[i].Y - 1]);
                    GL.Vertex3(vertex[(int)face[i].Z - 1]);
                    GL.End();
                }
            }
        }

        public void SetColors(List<Color> colors)
        {
            this.color = colors;
        }
    }
}
