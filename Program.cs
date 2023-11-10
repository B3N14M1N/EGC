﻿using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using CIOBAN.Librarie;
using System.Configuration;
using OpenTK.Graphics;
using CIOBAN.Librarie.RandomThings;
using CIOBAN.Librarie.Basic;
using CIOBAN.Scripturi;
/*
* CIOBAN BENIAMIN
* 3134A
*/
namespace CIOBAN
{
    // L2,3,4(in progres),
    class Program : GameWindow
    {
        #region Parametrii
        // Laboratorul 2
        #region L2
        private KeyboardState lastFrameKeyboard;
        private float rotX = 0;
        private float rotY = 0;

        private Key moveTriangleKey = Key.Q;
        private bool moveTriangle = true;
        private Key renderTriangleKey = Key.E;
        private bool renderTriangle = true;

        Vector3 poz = new Vector3();
        // zPoz folosit pentru scalarea tiunghiului folosind rotita
        private float zPoz = 0;
        private float mouseSensitivity = 0.1f;

        #endregion
        // Laboratorul 3
        #region L3
        CameraController cameraController = new CameraController();
        Triangle triangle = new Triangle(); 
        #endregion
        // Laboratorul 4 ---- in progres
        #region L4
        // L4
        FallingObject fallingObject = new FallingObject(RandomThings.GetRandomVector3(new Vector3(-1,1,-1), new Vector3(1,5,1)),0.5f);
        #endregion
        #endregion
        #region Constructor
        public Program() : base(800, 600,new GraphicsMode(32, 24, 0, 8))
        {
            KeyDown += WindowSettings;


            // Ascunde cursorul si il limiteaza doar in window
            CursorGrabbed = true;
            CursorVisible = false;

            Console.Clear();
            Console.WriteLine("Student: Cioban Beniamin\nGrupa: 3134a\n\n" +
                "Console Clear + Show Controls: F2\n"+
                "Window Controls:\n\tWindow - F11, Cursor - F1\n");
            Console.Write(cameraController.ToString() +
                fallingObject.ToString() +
                triangle.ToString() +
                "\tTriunghi randat - Mouse (miscare axe X,Y), Rotita (scalarea)\n" +
                "\t\t Q (blocare miscare), E (Randare).\n");
        }
        // Functie in care prelucreaza inputul pentru modificarea ferestrei
        #endregion
        #region Evenimete
        void WindowSettings(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
            if (e.Key == Key.F1)
            {
                CursorGrabbed = !CursorGrabbed;
                CursorVisible = !CursorVisible;
            }
            if(e.Key == Key.F2)
            {
                Console.Clear();
                Console.WriteLine("Student: Cioban Beniamin\nGrupa: 3134a\n\n" +
                    "Console Clear + Show Controls: F2\n" +
                    "Window Controls:\n\tWindow - F11, Cursor - F1\n");
                Console.Write(cameraController.ToString() +
                    fallingObject.ToString() +
                    triangle.ToString() +
                    "\tTriunghi randat - Mouse (miscare axe X,Y), Rotita (scalarea)\n" +
                    "\t\t Q (blocare miscare), E (Randare).\n");
            }
        }
        #endregion
        #region Metode
      
        // L2,3
        // Functie petnru preluarea si prelucrarea  
        // inputului mouse-ului si a tastaturii.
        public void GetInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // Prelucreaza variabilele boolene pentru a
            // lucra doar la o singura apasare a tasteii
            moveTriangle = (keyboard.IsKeyDown(moveTriangleKey) && lastFrameKeyboard.IsKeyUp(moveTriangleKey)) ? !moveTriangle : moveTriangle;
            renderTriangle = (keyboard.IsKeyDown(renderTriangleKey) && lastFrameKeyboard.IsKeyUp(renderTriangleKey)) ? !renderTriangle : renderTriangle;

            // Misca triunghiul dupa inputul mouse-ului
            if (moveTriangle)
            {
                poz.X -= (rotX - mouse.X) * (float)Time.deltaTime * mouseSensitivity;
                poz.Y -= (mouse.Y - rotY) * (float)Time.deltaTime * mouseSensitivity;

                if (zPoz - mouse.WheelPrecise > 0)
                {
                    poz.Z -= (float)Time.deltaTime;
                }
                else if (zPoz - mouse.WheelPrecise < 0)
                    poz.Z += (float)Time.deltaTime;
                if (poz.Z == 0)
                    poz.Z = .1f;
                zPoz = mouse.WheelPrecise;
            }
            // L3
            // Updateaza rotatiile precedente cu a mouse-ului
            rotX = mouse.X;
            rotY = mouse.Y;

            lastFrameKeyboard = keyboard;
        }

        // L2
        // Functie in care se deseneaza un triunghi 
        // la pozitiile specificate X si Y,
        // Z determina scalarea triunghiului
        public void DrawTriangle(float x, float y, float z)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Purple);
            GL.Vertex3(x - z, y + z, 0);
            GL.Vertex3(x, y - z, 0);
            GL.Vertex3(x + z, y + z, 0);
            GL.End();
        }

        // L3
        // Afiseaza axele de coordonate 
        public void DrawAxes()
        {
            GL.LineWidth(6);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(2, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 2, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 2);
            GL.End();
            GL.LineWidth(0.1f);
        }
        
        // L3
        // Randeaza un grid cu o anumita
        // culoare si un nr de linii
        public void DrawGrid(Color gridColor, int halfSize)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(gridColor);
            for (int i = -halfSize; i <= halfSize; i++)
            {
                GL.Vertex3(i, 0, -halfSize);
                GL.Vertex3(i, 0, halfSize);

                GL.Vertex3(-halfSize, 0, -i);
                GL.Vertex3(halfSize, 0, -i);
            }
            GL.End();
        }
        #endregion
        #region Metode de baza
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            // L2 preia valoarea curenta a rotitei mouse-ului
            zPoz = Mouse.GetState().WheelPrecise;
            //L4 Apeleaza metodele de start
            triangle.Start();
            fallingObject.Start();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnLoad(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;
            // L3-4 Seteaza perspectiva si camera
            cameraController.camera.ChangePerspectiveFieldOfView((float)aspect_ratio);
            cameraController.Draw();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Time.deltaTime = e.Time;
            GetInput();
            triangle.Update();
            fallingObject.Update();
            cameraController.Update();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // L3
            // Updateaza view-ul camerei
            // Randeaza un grid
            // Randeaza axele de coordonate
            cameraController.Draw();
            DrawGrid(Color.White,16);
            DrawAxes();
            // L2
            // Randeaza triunghiul doar daca 
            // renderTriangle = true
            if(renderTriangle)
            {
                DrawTriangle(poz.X, poz.Y, poz.Z);
            }
            fallingObject.Draw();
            triangle.Draw();
            SwapBuffers();
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            using (Program example = new Program())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
