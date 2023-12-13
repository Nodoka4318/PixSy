using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PixSy.IO.Save {
#pragma warning disable SYSLIB0011 // 型またはメンバーが旧型式です
    internal static class SaveManager {
        public static ISaveData Load(string path) {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                fs.Position = 0;

                var serializer = new XmlSerializer(typeof(ImplSaveData));
                var obj = serializer.Deserialize(fs);
                return (ISaveData)obj;
            }
        }

        public static void Save(string path, ImplSaveData saveData) {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                var serializer = new XmlSerializer(typeof(ImplSaveData));
                serializer.Serialize(fs, saveData);
            }
        }
    }
}
