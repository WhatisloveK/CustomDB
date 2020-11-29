using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class DataBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dbInfo")]
        public DatabaseInfo Info{ get; set; }

        [JsonPropertyName("entities")]
        public List<Entity> Entities { get; set; }

        public DataBase()
        {
            Entities = new List<Entity>();
            Info = new DatabaseInfo();
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            var db = (DataBase)obj;

            return Name == db.Name
                && Info.Equals(db.Info)
                && new Func<bool>(() =>  //Compare table
                {
                    if (Entities.Count == db.Entities.Count)
                    {
                        for (int i = 0; i < Entities.Count; i++)
                        {
                            if (!Entities[i].Equals(db.Entities[i]))
                                return false;
                        }
                        return true;
                    }
                    return false;
                }).Invoke();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
