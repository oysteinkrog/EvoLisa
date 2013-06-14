using System;
using System.Collections.Generic;

using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using GenArt.AST;
using System.Runtime.Serialization.Formatters.Binary;

namespace GenArt.Classes
{
    public static class Serializer
    {
        public static void Serialize(Settings settings)
        {
            if (string.IsNullOrEmpty(Application.LocalUserAppDataPath))
                return;

            string fileName = Application.LocalUserAppDataPath + "\\Settings.xml";
            Serialize(settings, fileName);
        }

        public static void Serialize(Settings settings, string fileName)
        {
            if (fileName == null)
                return;

            try
            {
                XmlSerializer serializer = new XmlSerializer(settings.GetType());
                using (FileStream writer = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception ex) { ; }
        }

        public static void Serialize(DnaDrawing drawing, string fileName)
        {
            if (fileName == null)
                return;

            if (fileName.EndsWith("xml"))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(drawing.GetType());
                    using (FileStream writer = new FileStream(fileName, FileMode.Create))
                    {
                        serializer.Serialize(writer, drawing);
                    }
                }
                catch (Exception ex) { ; }
            }
            else
            {
                SerializeBinary(drawing, fileName);
            }
        }

        public static void SerializeBinary(DnaDrawing drawing, string fileName)
        {
            if (fileName == null)
                return;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream writer = new FileStream(fileName, FileMode.Create))
                {
                    formatter.Serialize(writer, drawing);
                }
            }
            catch (Exception ex) { ; }
        }

        public static Settings DeserializeSettings()
        {
            if (string.IsNullOrEmpty(Application.LocalUserAppDataPath))
                return null;

            string fileName = Application.LocalUserAppDataPath + "\\Settings.xml";
            return DeserializeSettings(fileName);
        }

        public static Settings DeserializeSettings(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (FileStream reader = new FileStream(fileName, FileMode.Open))
                {
                    return (Settings)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex) { ; }
            return null;
        }

        public static DnaDrawing DeserializeDnaDrawing(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            if (fileName.EndsWith("xml"))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DnaDrawing));
                    using (FileStream reader = new FileStream(fileName, FileMode.Open))
                    {
                        return (DnaDrawing)serializer.Deserialize(reader);
                    }
                }
                catch (Exception ex) { ; }
                return null;
            }
            else
            {
                return DeserializeDnaDrawingBinary(fileName);
            }
        }

        public static DnaDrawing DeserializeDnaDrawingBinary(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream reader = new FileStream(fileName, FileMode.Open))
                {
                    return (DnaDrawing)formatter.Deserialize(reader);
                }
            }
            catch (Exception ex) { ; }
            return null;
        }
    }
}
