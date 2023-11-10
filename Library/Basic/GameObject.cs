using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* CIOBAN BENIAMIN
* 3134A
*/
namespace CIOBAN.Librarie.Basic
{
    // O clasa abstracta care permite crearea unor obiecte 
    // care pot fi modificate si randate
    public abstract class GameObject: IBehaviour, IRenderer
    {
        #region Parametri
        public Transform Transform { get; set; }
        #endregion

        #region Constructori
        public GameObject(){
            Transform = new Transform();
        }
        public GameObject(Transform transform)
        {
            Transform = transform;
        }
        #endregion
        #region Methode

        public abstract void Start();

        public abstract void Update();

        public abstract void Draw();

        public void SetColors(List<Color> colors)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

