using OpenTK;
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
    // Clasa din care momentan
    // folosesc doar positia.
    // Exista posibilitatea sa o imbunatatesc
    // pentru a oferii rotatii si scalari
    public class Transform
    {
        public Vector3 Position { get; set; }
        //public Quaternion Rotation { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Transform() { 
            Position = new Vector3();
            Rotation = new Vector3();
            Scale = new Vector3(1f,1f,1f);
        }
        public Transform(Vector3 position)
        {
            Position = position;
            Rotation = new Vector3();
            Scale = new Vector3(1f, 1f, 1f);
        }
        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}
