using CIOBAN.Librarie;
using CIOBAN.Librarie.Basic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * CIOBAN BENIAMIN
 * 3134A
 */
namespace CIOBAN.Scripturi
{
    public class CameraController : GameObject
    {

        // Folosesc clasa creata "Camera"
        // pentru a putea controla view-ul scenei
        public Camera camera = new Camera(new Vector3(0, 5, 25));
        // "movementSpeed" si "mouseSensitivity"
        // determina sensivitatea controlului camerei
        private float mouseSensitivity = 0.2f;
        private float movementSpeed = 3f;
        // Un set de key pentru determinarea inputului
        // care va misca camera
        private readonly Key cameraForwardKey = Key.W;
        private readonly Key cameraBackwardsKey = Key.S;
        private readonly Key cameraLeftKey = Key.A;
        private readonly Key cameraRightKey = Key.D;
        private readonly Key cameraUpKey = Key.LShift;
        private readonly Key cameraDownKey = Key.LControl;
        private readonly Key lockCameraKey = Key.Number1;
        // "lockCamera" blocheaza controlul camerei
        // daca este setat pe True
        private bool lockCamera = false;
        private KeyboardState lastFrameKeyboard;
        // mouseRot este folosit pentru a calcula rotatia camerei
        // folosind diferenta de la mouse .X si .Y in baza de timp

        // Parametrii pentru camera fixa cu 2 pozitii.
        private Vector3 nearCameraPos = new Vector3(12f, 5f, 12f);
        private Vector3 farCameraPos = new Vector3(30f, 10f, 30f);
        private Vector3 target = Vector3.Zero;

        private Key changeCameraPositionKey = Key.P;
        private Vector2 mouseRot = new Vector2();
        public override void Start()
        {
            lastFrameKeyboard = Keyboard.GetState();
            mouseRot = new Vector2(Mouse.GetState().X, Mouse.GetState().X);
            Transform.Position = nearCameraPos;
        }

        public override void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // L3
            // Blocheaza sau nu controlul camerei
            lockCamera = (keyboard.IsKeyDown(lockCameraKey) && lastFrameKeyboard.IsKeyUp(lockCameraKey)) ? !lockCamera : lockCamera;

            // L3
            // Prelucreaza inputul pentru a misca camera
            if (!lockCamera)
            {
                // cameraPosition este directia in care trebuie mers
                // Reprezinta un vector3 (este vizualizat ca un vector local)
                Vector3 cameraPosition = Vector3.Zero;
                cameraPosition.X -= keyboard.IsKeyDown(cameraLeftKey) ? 1f : 0f;
                cameraPosition.X += keyboard.IsKeyDown(cameraRightKey) ? 1f : 0f;
                cameraPosition.Z -= keyboard.IsKeyDown(cameraDownKey) ? 1f : 0f;
                cameraPosition.Z += keyboard.IsKeyDown(cameraUpKey) ? 1f : 0f;
                cameraPosition.Y -= keyboard.IsKeyDown(cameraBackwardsKey) ? 1f : 0f;
                cameraPosition.Y += keyboard.IsKeyDown(cameraForwardKey) ? 1f : 0f;
                cameraPosition *= (float)Time.deltaTime * movementSpeed;

                // Apeleaza metodele de miscare a pozitiei si a rotatiei
                // prelucreaza vectorul local pentru a aplica directiei sensului camerei
                camera.MoveCamera(cameraPosition);
                // calculeaza discrepanta dintre miscarea mouse-ului in functie de timpul parcurs in frame
                camera.AddRotation((mouseRot.X - mouse.X) * (float)Time.deltaTime * mouseSensitivity, -(mouse.Y - mouseRot.Y) * (float)Time.deltaTime * mouseSensitivity);
            }
            // L3
            // Updateaza rotatiile precedente cu a mouse-ului
            mouseRot = new Vector2(mouse.X,mouse.Y);

            // L5
            // Modifica pozitia camerei
            if (lockCamera && lastFrameKeyboard[changeCameraPositionKey] && keyboard.IsKeyUp(changeCameraPositionKey))
            {
                if (Transform.Position == nearCameraPos)
                    Transform.Position = farCameraPos;
                else
                    Transform.Position = nearCameraPos;
            }
            lastFrameKeyboard = keyboard;
        }
        public override void Draw()
        {
            if(lockCamera)
            {
                Matrix4 lookat = Matrix4.LookAt(Transform.Position, target, Vector3.UnitY);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref lookat);
            }
            else
                camera.UpdateCamera();

        }
        // Returneaza un string cu controalele camerei
        public override string ToString()
        {
            string s = "Camera Controls:" +
                "\n\tMovement - " + cameraForwardKey + cameraLeftKey + cameraBackwardsKey + cameraRightKey +
                ",\n\tUp - " + cameraUpKey + 
                ",\n\tDown - " + cameraDownKey +
                ",\n\tUn/block camera - " + lockCameraKey +
                ",\n\tChange camera position - "+ changeCameraPositionKey+".\n";
            return s;
        }
    }
}
