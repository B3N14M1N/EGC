using CIOBAN.Librarie;
using CIOBAN.Librarie.Basic;
using CIOBAN.Library.Models;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
/*
 * CIOBAN BENIAMIN
 * 3134A
 */
namespace CIOBAN.Library.Scripts
{
    public class RotatingTeapot : GameObject
    {
        Teapot teapot;
        private readonly float rotatingSpeed = 25f;
        public List<Vector3> transforms = new List<Vector3>();
        private float angle = 0;

        public override void Draw()
        {
            for(int i = 0; i<transforms.Count; i++)
            {
                GL.PushMatrix();
                GL.Translate(transforms[i]);
                GL.Rotate(angle, Transform.Rotation);
                if(i%2 == 0)
                    teapot.meshData.wireframe = true;
                else
                    teapot.meshData.wireframe=false;
                teapot.Draw();
                GL.PopMatrix();
            }
        }

        public override void Start()
        {
            teapot = new Teapot();
            for(int i = -20; i <= 20; i += 10)
            {
                transforms.Add(new Vector3(i,0f,-20f));
            }
            Transform.Rotation = new Vector3(0f, 1f, 0f);
            //Transform.Scale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public override void Update()
        {
            angle += rotatingSpeed * (float)Time.deltaTime;
        }
    }
}
