using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

