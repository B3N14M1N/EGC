using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
/*
 * CIOBAN BENIAMIN
 * 3134A
 * REZOLVARI
 * * L2
 * 
 */
namespace CIOBAN
{
    class Program : GameWindow
    {
        //Laboratorul 2
        #region L2
        Vector3 poz=new Vector3();
        private float zPoz = 0;
        private float mouseSensitivity = 0.1f;

        private bool moveTriangleOnce=false;
        private bool moveTriangle = true;

        private bool renderTriangleOnce = false;
        private bool renderTriangle = true;
        #endregion
        public Program() : base(800, 600)
        {
            KeyDown += WindowSettings;
        }
        // Functie in care prelucreaza inputul pentru modificarea ferestrei
        void WindowSettings(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Fullscreen)? WindowState.Normal : WindowState.Fullscreen;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);

            // L2 preia valoarea curenta a rotitei mouse-ului
            zPoz = Mouse.GetState().WheelPrecise;
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-2.0, 2.0, -2.0, 2.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GetInput();
        }

        // L2
        // Functie petnru preluarea si prelucrarea  
        // inputului mouse-ului si a tastaturii.
        public void GetInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if(moveTriangle)
            {
                float delta = 1 / 30f;
                poz.X = mouse.X * delta * mouseSensitivity;
                poz.Y = -mouse.Y * delta * mouseSensitivity;

                if(zPoz - mouse.WheelPrecise > 0)
                {
                    poz.Z -= delta;
                }
                else if(zPoz - mouse.WheelPrecise < 0)
                    poz.Z += delta;
                if (poz.Z == 0)
                    poz.Z = .1f;
                zPoz = mouse.WheelPrecise;
            }

            // Prelucreaza variabilele boolene pentru a
            // lucra doar la o singura apasare a tasteii
            // pana cand tasta nu mai este apasata
            if (keyboard.IsKeyDown(Key.Q) && !moveTriangleOnce)
            {
                moveTriangle = !moveTriangle;
                moveTriangleOnce = true;
            }
            if (keyboard.IsKeyUp(Key.Q))
                moveTriangleOnce = false;

            if (keyboard.IsKeyDown(Key.W) && !renderTriangleOnce)
            {
                renderTriangle = !renderTriangle;
                renderTriangleOnce = true;
            }
            if (keyboard.IsKeyUp(Key.W))
                renderTriangleOnce = false;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // L2
            // Randeaza triunghiul doar daca 
            // renderTriangle = true
            if(renderTriangle)
            {
                DrawTriangle(poz.X, poz.Y, poz.Z);
            }

            this.SwapBuffers();
        }

        // L2
        // Functie in care se deseneaza un triunghi 
        // la pozitiile specificate.
        // Z determina scalarea triunghiului
        public void DrawTriangle(float x, float y, float z)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Red);
            GL.Vertex3(x - z, y + z, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(x, y - z, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(x + z, y + z, 0);
            GL.End();
        }

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
