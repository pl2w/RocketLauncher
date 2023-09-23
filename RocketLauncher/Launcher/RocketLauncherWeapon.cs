using Photon.Pun;
using RocketLauncher.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RocketLauncher.Launcher
{
    internal class RocketLauncherWeapon : MonoBehaviourPunCallbacks
    {
        public static RocketLauncherWeapon instance;
        GameObject rocketLauncher, rocket, shootPoint;
        public AudioClip rocketShootClip, rocketExplodeClip;

        public bool testFire, onceFire, isRocketJumping;
        GameObject explosion;
        public bool inModded
        {
            get
            {
                return _modded;
            }
            set
            {
                rocketLauncher.SetActive(value);
                _modded = value;
            }
        }

        bool _modded;

        public void Start()
        {
            instance = this;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RocketLauncher.Resources.launcher");
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();

            rocketLauncher = Instantiate(bundle.LoadAsset<GameObject>("RocketLauncher"));
            rocket = rocketLauncher.transform.Find("rocket").gameObject;
            rocket.AddComponent<Rocket>().speed = 20f;
            rocket.SetActive(false);

            shootPoint = rocketLauncher.transform.Find("ShootPoint").gameObject;

            rocketShootClip = bundle.LoadAsset<AudioClip>("rocket_shoot");
            rocketShootClip.LoadAudioData();

            rocketExplodeClip = bundle.LoadAsset<AudioClip>("rocket_explode");
            rocketExplodeClip.LoadAudioData();

            rocketLauncher.transform.SetParent(GorillaLocomotion.Player.Instance.rightControllerTransform, false);

            rocketLauncher.transform.localEulerAngles = new Vector3(35f, 0, 0);
            rocketLauncher.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            bundle.Unload(false);
            rocketLauncher.SetActive(false);
        }

        void Update()
        {
            if(!inModded) return;
            if ((ControllerInputPoller.instance.rightGrab && !onceFire) || testFire)
            {
                onceFire = true;
                testFire = false;
                GameObject rocketTemp = GameObject.Instantiate(rocket);
                rocketTemp.transform.position = shootPoint.transform.position;
                rocketTemp.transform.rotation = shootPoint.transform.rotation;
                rocketTemp.SetActive(true);

                AudioSource.PlayClipAtPoint(rocketShootClip, shootPoint.transform.position);
            } else if (!ControllerInputPoller.instance.rightGrab && onceFire)
            {
                onceFire = false;
            }
        }

        public override void OnJoinedRoom() => inModded = ModdedLobbyCheck.IsThisAModdedLobby();
        public override void OnLeftRoom() => inModded = false;
    }
}
