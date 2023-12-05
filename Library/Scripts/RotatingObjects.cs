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
    public class RotatingObjects : GameObject
    {
        // L5
        // Citeste din fisiere OBJ si randeaza obiectele 
        // in modul solid si contur
        #region Parametrii
        FileModel teapot;
        FileModel pumpkin;
        private readonly float rotatingSpeed = 25f;
        public List<Vector3> transforms = new List<Vector3>();
        private float angle = 0;
        #endregion

        #region Metode
        public override void Draw()
        {
            for(int i = 0; i<transforms.Count; i++)
            {
                GL.PushMatrix();
                GL.Translate(transforms[i]);
                GL.Rotate(angle, Transform.Rotation);
                if (i % 2 == 0)
                {
                    GL.Translate(new Vector3(0,7f,0));
                    GL.Rotate(-90, 1, 0, 0);
                    GL.Scale(new Vector3(.05f,.05f,.05f));
                    if (i >= transforms.Count / 2)
                        pumpkin.meshData.wireframe = true;
                    else pumpkin.meshData.wireframe = false;
                    pumpkin.Draw();
                }
                else
                {
                    if (i >= transforms.Count / 2)
                        teapot.meshData.wireframe = true;
                    else teapot.meshData.wireframe= false;
                    teapot.Draw();
                }
                GL.PopMatrix();
            }
        }

        public override void Start()
        {
            teapot = new FileModel("teapot.obj");
            pumpkin = new FileModel("pumpkin.obj");
            for(int i = -20; i <= 20; i += 11)
            {
                transforms.Add(new Vector3(i,0f,-20f));
            }
            Transform.Rotation = new Vector3(0f, 1f, 0f);
        }

        public override void Update()
        {
            angle += rotatingSpeed * (float)Time.deltaTime;
        }
        #endregion
    }
}
