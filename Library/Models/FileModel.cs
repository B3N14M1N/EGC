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
    // L5
    // Citeste un model 3d dintr-un fisier OBJ
    // si creaza un obiect care contine datele
    // citite.
    public class FileModel : IRenderer
    {
        #region Parametri
        public MeshData meshData;

        #endregion
        #region Constructori
        public FileModel(string fileName)
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
            meshData?.SetColors(colors);
        }
        #endregion
    }
}
