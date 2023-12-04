using CIOBAN.Librarie.Basic;
using CIOBAN.Library.Models;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    // Un model 3d pentru teapot
    public class Pumpkin : IRenderer
    {
        #region Parametri
        public MeshData meshData;
        private readonly string fileName = "pumpkin.obj";

        #endregion
        #region Constructori
        public Pumpkin()
        {
            string solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string completePath = solutionPath + "\\Library\\Models\\" + fileName;
            Administrare_Date date = new Administrare_Date(completePath);
            meshData = date.ReadOBJ();
        }
        #endregion
        #region Metode
        public void Draw()
        {
            meshData?.Draw();
        }

        public void SetColors(List<Color> colors)
        {
            meshData.SetColors(colors);
        }
        #endregion
    }
}
