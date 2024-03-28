using Game.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Resolution_Extension
{
        [Serializable]
        public class GlobalVariables
        {
            /// <summary>
            /// PostExposure
            /// </summary>
            [XmlElement]
            public int ResolutionHeight { get; set; }

            [XmlElement]
            public int ResolutionWidth { get; set; }


 


            public static void SaveToFile(string filePath)
            {
                try
                {
                    // Create an XmlSerializer for the GlobalVariables type.
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalVariables));

                    // Create or open the file for writing.
                    using (TextWriter writer = new StreamWriter(filePath))
                    {
                        // Serialize the current static object to the file.
                        serializer.Serialize(writer, Instance);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving GlobalVariables to file: {ex.Message}");
                }
            }

            public static GlobalVariables LoadFromFile(string filePath)
            {
                try
                {
                    // Create an XmlSerializer for the GlobalVariables type.
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalVariables));

                    // Open the file for reading.
                    using (TextReader reader = new StreamReader(filePath))
                    {
                        // Deserialize the object from the file.
                        GlobalVariables loadedVariables = (GlobalVariables)serializer.Deserialize(reader);

                        // Set the loaded values to the corresponding properties.
                        GlobalVariables.Instance.ResolutionHeight = loadedVariables.ResolutionHeight;
                        GlobalVariables.Instance.ResolutionWidth = loadedVariables.ResolutionWidth;



                        return loadedVariables;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load ColorAdjustments settings. Ensure that at least one setting is set.");
                    return null;
                }
            }

        internal static void CheckForIntegrity()
        {
            string localLowDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localLowDirectory = Path.Combine(localLowDirectory, "..", "LocalLow");
            string assemblyDirectory = Path.Combine(localLowDirectory, "Colossal Order", "Cities Skylines II", "Mods", "Resolution Extension");
            string resetFilePath = Path.Combine(assemblyDirectory, ".reset");

            Mod.log.Info("Checking for file at: " + resetFilePath);

            try
            {
                if (File.Exists(resetFilePath))
                {
                    ApplyRes();
                    Mod.log.Info("Integrity check passed. Resolution settings applied.");
                }
                else
                {
                    Mod.log.Info("Integrity check failed. .reset file not found.");
                }
            }
            catch (Exception ex)
            {
                Mod.log.Error("An error occurred while checking for file integrity: " + ex.Message);
            }
        }



        public static ScreenResolution ScreenResolution = new ScreenResolution();

        public static void ApplyRes()
        {
            ScreenResolution.width = 1080;
            ScreenResolution.height = 1920;

            RefreshRate refreshRate = new RefreshRate { numerator = 60, denominator = 1 };

            // Apply the resolution
            Screen.SetResolution(ScreenResolution.width, ScreenResolution.height, FullScreenMode.Windowed, refreshRate);
        }


        // Singleton pattern to ensure only one instance of GlobalVariables exists.
        private static GlobalVariables instance;
            public static GlobalVariables Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new GlobalVariables();
                    }
                    return instance;
                }
            }

        }
    }

