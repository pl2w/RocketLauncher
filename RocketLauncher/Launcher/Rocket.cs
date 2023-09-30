using RocketLauncher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RocketLauncher.Launcher
{
    internal class Rocket : MonoBehaviour
    {
        public float speed;
        Rigidbody body;

        Vector3 prevPos;

        Rocket()
        {
            body = GetComponent<Rigidbody>();
            prevPos = transform.position;
            speed += PlayerUtils.playerBody.velocity.magnitude;
        }
   
        void Update()
        {
            if (!RocketLauncherWeapon.instance.inModded || !RocketLauncherWeapon.instance.modEnabled)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }
            transform.position += transform.forward * speed * Time.deltaTime;

            if (Physics.CheckSphere(transform.position, 0.25f, ~gameObject.layer, QueryTriggerInteraction.Ignore))
            {
                float calculateSizeOfExplosion = 2f;
                calculateSizeOfExplosion += Mathf.Clamp(PlayerUtils.playerBody.velocity.magnitude / 20, 2, 6.5f);

                float explosionStrength = 200f;
                explosionStrength += Mathf.Clamp(PlayerUtils.playerBody.velocity.magnitude / 10f, 100f, 500);
                AudioSource.PlayClipAtPoint(RocketLauncherWeapon.instance.rocketExplodeClip, transform.position);

                if(Vector3.Distance(PlayerUtils.playerBody.transform.position, transform.position) <= calculateSizeOfExplosion)
                {
                    RocketLauncherWeapon.instance.isRocketJumping = true;
                    PlayerUtils.playerBody.AddExplosionForce(
                        explosionStrength,
                        transform.position,
                        calculateSizeOfExplosion,
                        0, ForceMode.Impulse);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }
}
