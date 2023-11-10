using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIOBAN.Librarie.Basic
{
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

        public abstract void Start();

        public abstract void Update();

        public abstract void Draw();
        #endregion
        #region Methode

        #endregion
    }
}

