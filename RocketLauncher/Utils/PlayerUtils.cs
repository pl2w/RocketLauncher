using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RocketLauncher.Utils
{
    internal class PlayerUtils : MonoBehaviour
    {
        public static Rigidbody playerBody;

        void Start ()
        {
            playerBody = GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>();
        }
    }
}
