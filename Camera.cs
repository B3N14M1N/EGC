using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;

namespace CIOBAN
{
    public class Camera
    {
        public Vector3 position { get; set; }
        private Vector3 direction;


        private float _zNear;
        private float _zFar;
        public float zNear {
            get { return _zNear; }
            set { _zNear = (value <= 0) ? 0.1f : (value>=_zFar)? 0.1f: value; }
        }
        public float zFar {
            get { return _zFar; }
            set { _zFar = (value <= _zNear) ? _zNear + 1f :value; }
        }

        public Vector3 Forward { get { return new Vector3((float) Math.Sin(direction.X), 0, (float)Math.Cos(direction.X)); } }
        public Vector3 Right { get { return new Vector3(-Forward.Z, 0, Forward.X); } }

        public Camera(Vector3 Position)
        {
            _zNear = 0.1f;
            _zFar = 1000f;
            position = Position;
            direction = new Vector3((float)Math.PI, 0f, 0f);
        }

        public Matrix4 GetMatrix()
        {
            Vector3 lookat = new Vector3((float)(Math.Sin(direction.X) * Math.Cos(direction.Y)),
                                            (float)Math.Sin(direction.Y),
                                            (float)(Math.Cos(direction.X) * Math.Cos(direction.Y)));
            return Matrix4.LookAt(position, position + lookat, Vector3.UnitY);
        }
        public void MoveCamera(Vector3 Direction)
        {

            Vector3 offset = Vector3.Zero;

            offset += Direction.X * Right;
            offset += Direction.Y * Forward;
            offset.Y += Direction.Z;

            offset.NormalizeFast();

            position += offset;
        }
        public void AddRotation(float x, float y)
        {
            direction.X = (direction.X + x) % ((float)Math.PI * 2.0f);
            direction.Y = Math.Max(Math.Min(direction.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
        public void UpdateCamera()
        {
            Matrix4 lookat = GetMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }
        public void ChangePerspectiveFieldOfView(float aspect_ratio)
        {
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, _zNear, _zFar);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
    }
}
