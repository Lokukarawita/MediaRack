using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace MediaRack.Core.Data.Remote
{
    public sealed class TypeMap : Dictionary<Type, object>
    {
        public static TypeMap ReadMap(Type remoteStorageType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var all_res = assembly.GetManifestResourceNames();

            var res_fn = string.Format("{0}.mrm.xml", remoteStorageType.Name);

            var res = all_res.FirstOrDefault(x => x.Contains(res_fn));
            if (string.IsNullOrWhiteSpace(res))
                throw new FileNotFoundException("Cannot find embedded resource " + res_fn);


            var resourceName = res;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                using (var stringr = new StringReader(result))
                {
                    XDocument doc = XDocument.Load(stringr);
                    var mapings = doc.Descendants("mappings")
                        .Descendants()
                        .Select(e => new { k = Type.GetType(e.Attribute("type").Value), t = (object)e.Attribute("mapTo").Value })
                        .ToDictionary(k => k.k, v => v.t);

                    TypeMap map = new TypeMap(mapings);
                    return map;
                }
            }
        }

        public TypeMap()
        { }
        
        public TypeMap(Dictionary<Type, object> map)
            : base(map)
        { }
    }
}
