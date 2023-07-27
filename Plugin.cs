using System;
using BepInEx;
using Steamworks;
using UnityEngine;
using Utilla;

namespace GorillaTagModTemplateProject
{
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;
		public GameObject cube;
		public bool init;

		void Start()
		{
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			if (init && inRoom)
			{
				SetupCube();
			}

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			if (cube != null && init)
			{
				DestroyCube();
			}
            HarmonyPatches.RemoveHarmonyPatches();
        }

		void SetupCube()
		{
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(GorillaLocomotion.Player.Instance.rightHandFollower.transform, false);
            cube.GetComponent<MeshRenderer>().material.color = Color.blue;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.transform.localPosition = new Vector3(-0.045f, 0.0004f, -0.01f);
        }

		void DestroyCube()
		{
			if (cube != null)
			{
				Destroy(cube);
		    }
			else
			{
				Debug.Log("Cube is null. Please try again");
			}
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			init = true;
        }

		void Update()
		{

		}

		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
            inRoom = true;

            SetupCube();
		}

		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			DestroyCube();

			inRoom = false;
		}
	}
}
