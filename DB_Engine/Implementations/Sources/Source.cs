using DB_Engine.Factories;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DB_Engine.Implementations
{
    public class Source : ISource
    {
        public string Url { get; set; }
        public string Type => typeof(Source).AssemblyQualifiedName;
        public long SizeInBytes => new FileInfo(Url).Length;

        public IDbProviderFactory dbProviderFactory { get; set; }

        public Source()
        {
        }

        public async Task<List<List<object>>> GetDataAsync()
        {
            using (var data = File.OpenRead(Url))
            {
                var result = await JsonSerializer.DeserializeAsync<List<List<object>>>(data);

                return result;
            }
        }

        public List<List<object>> GetData()
        {
            var data = File.ReadAllText(Url);
            if (string.IsNullOrEmpty(data))
            {
                return new List<List<object>>();
            }

            var result = JsonSerializer.Deserialize<List<List<object>>>(data);

            return result;
        }

        public void WriteData(List<List<object>> data)
        {
            if (!(data == null))
            {
                var newStringData = JsonSerializer.Serialize(data);

                File.WriteAllText(Url, newStringData);
            }
        }

        public async Task WriteDataAsync(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                using (var streamData = File.OpenWrite(Url))
                {
                    await JsonSerializer.SerializeAsync(streamData, data);
                }
            }
        }

        public void SetUrl(DataBase db, Entity table)
        {
            Url = $"{db.Info.RootPath}\\{table.Name}{table.Sources.Count + 1}{GlobalSetting.Extention}";
            Create();
        }
        protected void Create()
        {
            using (File.Create(Url)) { }
        }

        public override bool Equals(object obj)
        {
            var source = (ISource)obj;

            return Type == source.Type
                && Url == source.Url;
        }

    }
}
