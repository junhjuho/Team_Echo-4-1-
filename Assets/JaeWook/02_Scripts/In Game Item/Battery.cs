using NHR;
using SeongMin;
using UnityEngine;
using System.Collections.Generic;

namespace Jaewook
{
    /// <summary>
    /// flashlight - battery ¿¬µ¿
    /// </summary>
    public class Battery : ItemObject, IItem
    {
        FlashLight flashlight;
        public virtual void OnGrab()
        {
            // SeoungMin.ItemObject.cs ->public bool isFind = false;
            // this.isFind = true;

            /*
            if (isFind)
            {
                flashlight.ChargeFlashlight();
            }
            */
           
            // base.isFind = false;
        }

        public void OnUse()
        {

        }

        public void OnRelease()
        {

        }
    }
}
