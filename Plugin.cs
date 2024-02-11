using BepInEx;
using BepInEx.Bootstrap;
using System;
using System.Diagnostics;
using UnityEngine;


namespace DeveloperTools
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        // tf is this?: well its a shitty on gui that just makes it better to code/make mods idk if i can do this without utilla but hopefully you see this in source code
        // Pages
        public bool Mainpg = true;
        public bool InstalledModspg = false;
        public bool Open = true;



        public int objectCount;

        public void restart()
        {
            Application.Quit();
        }
        public void OnGUI()
        {
            // GUI skin stuff
            GUI.skin.label.fontSize = 35;
            GUI.skin.box.fontSize = 27;





            // GUI



            if (Open)
            {

                if (GUI.Button(new Rect(401, 20, 50, 50), "Close"))
                {
                    Open = false;
                }

                GUI.Box(new Rect(-20f, 20f, 400f, 400f), "Dev Tools");
                if (Mainpg)
                {
                    if (GUI.Button(new Rect(30, 50, 50, 50), "Mods"))
                    {
                        Mainpg = false;
                        InstalledModspg = true;
                    }
                    if (GUI.Button(new Rect(100, 50, 140, 50), "Restart (steam)"))
                    {
                        Process.Start("steam://rungameid/1533390");
                        Invoke("restart", 1f);
                    }

                    GUI.Label(new Rect(50f, 150f, 400, 400), "Fps: " + 1 / Time.deltaTime);

                    GUI.Label(new Rect(50, 250, 400, 400), "Objects: " + objectCount);
                }
                if (InstalledModspg)
                {
                    if (GUI.Button(new Rect(30, 50, 50, 50), "Back"))
                    {
                        Mainpg = true;
                        InstalledModspg = false;
                    }
                    GUI.skin.label.fontSize = 25;

                    GUILayout.BeginArea(new Rect(20, 100, 300, 500));
                    GUILayout.Label("Installed BepInEx Mods:");
                    if (loadedPlugins != null)
                    {
                        foreach (var pluginName in loadedPlugins)
                        {
                            GUILayout.Label(pluginName);
                        }
                    }
                    else
                    {
                        GUILayout.Label("No BepInEx mods found.");
                    }
                    GUILayout.EndArea();
                }
            }
            else
            {
                if (GUI.Button(new Rect(20, 20, 50, 50), "Open"))
                {
                    Open = true;
                }
            }



            
        }



            public void Update()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            objectCount = allObjects.Length;
        }
        private string[] loadedPlugins;

        public void Awake()
        {
            // Get the BepInEx plugin manager
            var manager = Chainloader.PluginInfos;

            if (manager != null)
            {
                // Initialize the array to store loaded plugin names
                loadedPlugins = new string[manager.Count];

                // Iterate through all loaded plugins
                int i = 0;
                foreach (var plugin in manager)
                {
                    // Store the name of each loaded plugin
                    loadedPlugins[i] = plugin.Value.Metadata.Name;
                    i++;
                }
            }
            else
            {
                Logger.LogError("Failed to get plugin manager!");
            }
        }

    }



}


