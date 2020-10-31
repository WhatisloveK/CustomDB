using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Parser
    {
        private readonly Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>()
        {
            { "create-db", new List<string>{ "-d","-s"} },
            { "get-tables", new List<string>{ "-d"} },
            { "create-tbl", new List<string>{ "-d",  "-t"} },
            { "delete-tbl", new List<string>{ "-d",  "-t"} },
            { "add-clmn", new List<string>{ "-d",  "-t", "-cn", "-ct"} },
            { "add-clmn-wth-vldtr", new List<string>{ "-d",  "-t", "-cn", "-ct", "-cmprt", "-v"} },
            { "insert", new List<string>{ "-d",  "-t", "-r"} },
            { "select", new List<string>{ "-d",  "-t", "-sh"} },
            { "update", new List<string>{ "-d",  "-t", "-r"} },
            { "inner-join", new List<string>{ "-d",  "-t", "-t2","-c","-c2", "-sh" } }
        };

        private ComparsonType GetComparson(string comparsonString)
        {
            return comparsonString switch
            {
                ">" => ComparsonType.Greater,
                ">=" => ComparsonType.GreaterOrEqual,
                "<" => ComparsonType.Less,
                "<=" => ComparsonType.LessOrEqual,
                "==" => ComparsonType.Equal,
                "!=" => ComparsonType.NotEqual,
                var t when t.ToLower() == "contains" => ComparsonType.Contains,
                var t when t.ToLower() == "notcontains" => ComparsonType.NotContains,
                var t when t.ToLower() == "startswith" => ComparsonType.StartsWith,
                var t when t.ToLower() == "notstartswith" => ComparsonType.NotStartsWith,
                var t when t.ToLower() == "endswith" => ComparsonType.EndsWith,
                var t when t.ToLower() == "notendswith" => ComparsonType.NotEndsWith,
                var t when t.ToLower() == "isnull" => ComparsonType.IsNull,
                var t when t.ToLower() == "isnotnull" => ComparsonType.IsNotNull,
                _ => throw new Exception($"Invalid compartion operation {comparsonString}"),
            };
        }

        private readonly DatabaseService databaseService = new DatabaseService();
        private readonly TableService tableService = new TableService();


        public async Task Parse(string input)
        {
            try
            {
                List<string> words = input.Split(' ').ToList();
                List<string> parameters;

                var parametersDictionary = new Dictionary<string, string>();
                if (commands.ContainsKey(words[0]))
                {
                    parameters = commands[words[0]];

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (!words.Contains(parameters[i]))
                        {
                            throw new Exception("Incorrect Command");
                        }
                        else
                        {
                            parametersDictionary.Add(parameters[i], words[words.IndexOf(parameters[i]) + 1]);
                        }
                    }

                    await ExecuteCommand(words[0], parametersDictionary);
                }
                else
                {
                    throw new Exception("Incorrect Command");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task ExecuteCommand(string command, Dictionary<string,string> parametersDictionary)
        {
            switch (command)
            {
                case "create-db":
                    await databaseService.CreateDatabase(parametersDictionary["-d"], int.Parse(parametersDictionary["-s"]));
                    break;
                case "get-tables":
                    await databaseService.GetTableList(parametersDictionary["-d"]);
                    break;
                case "create-tbl":
                    await databaseService.CreateTable(parametersDictionary["-d"], parametersDictionary["-t"]);
                    break;
                case "add-clmn":
                    await tableService.AddColumn(parametersDictionary["-d"], parametersDictionary["-t"], parametersDictionary["-cn"], DataValueType.GetDataValueType(parametersDictionary["-ct"]).ToString(), null);
                    break;
                case "add-clmn-wth-vldtr":
                {
                        var validators = new List<Validator>();
                        validators.Add(new Validator
                        {
                            DataValueTypeId = DataValueType.GetDataValueType(parametersDictionary["-ct"]).ToString(),
                            Value = parametersDictionary["-v"],
                            ComparsonType = GetComparson(parametersDictionary["-cmprt"])
                        });
                        await tableService.AddColumn(parametersDictionary["-d"], parametersDictionary["-t"], parametersDictionary["-cn"], validators[0].DataValueTypeId, validators);
                    break;
                }
                case "insert":
                { 
                    var list = new List<Row>();
                    Row row = new Row();
                    row.Items.Add(parametersDictionary["-r"].Split(","));
                    list.Add(row);
                    await tableService.Insert(parametersDictionary["-d"], parametersDictionary["-t"], list);
                    break;
                }
                case "select":
                    await tableService.Select(parametersDictionary["-d"], parametersDictionary["-t"],  bool.Parse(parametersDictionary["-sh"]));
                    break;
                case "update":
                {
                    var list = new List<Row>();
                    Row row = new Row();
                    row.Items.Add(parametersDictionary["-r"].Split(","));
                    list.Add(row);
                    await tableService.Update(parametersDictionary["-d"], parametersDictionary["-t"], list);
                    break;
                }
                case "inner-join":
                    await tableService.InnerJoin(parametersDictionary["-d"], parametersDictionary["-t"], parametersDictionary["-t2"], parametersDictionary["-c"], parametersDictionary["-c2"]);
                    break;
                case "delete-tbl":
                    break;
            }

        }
    }
}
