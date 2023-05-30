using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Subset.Tests
{
    public class JsonCollectionReader
    {
        public JsonCollectionReader() { }

        public IEnumerable<T> ReadJsonCollection<T>(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        reader.SupportMultipleContent = true;

                        JsonSerializer serializer = new JsonSerializer();

                        while (true)
                        {
                            if (!reader.Read())
                                break;

                            var item = serializer.Deserialize<T>(reader)!;

                            yield return item;
                        }
                    }
                }
            }

        }
    }
}
