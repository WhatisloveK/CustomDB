using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Parser
    {
        private static Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>()
        {
            { "create-db",    new List<string>{ "-d","-s"} },
            { "get-tables",   new List<string>{ "-d"} },
            { "create-tbl",   new List<string>{ "-d",  "-t"} },
            { "delete-tbl",  new List<string>{ "-d",  "-t"} },
            { "add-clmn",   new List<string>{ "-d",  "-t", "-cn", "-ct"} },
            { "insert",   new List<string>{ "-d",  "-t", "-r"} },
            { "select", new List<string>{ "-d",  "-t"} },
            { "update",  new List<string>{ "-d",  "-t", "id"} },
            { "inner-join",   new List<string>{ "-d",  "-t", "-t", "-t" } }
        };

        private DatabaseService databaseService = new DatabaseService();
        private TableService tableService = new TableService();


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
            catch(Exception)
            {
                Console.WriteLine("Incorrect Command");
            }
            
        }

        public  async Task ExecuteCommand(string command, Dictionary<string,string> parametersDictionary)
        {
            switch (command)
            {
                case "crtdb":
                    await databaseService.CreateDatabase(parametersDictionary["-d"], int.Parse(parametersDictionary["-s"]));
                    break;
                case "get-tables":
                    await databaseService.GetTableList(parametersDictionary["-d"]);
                    break;
                case "create-tbl":   
                    await databaseService.CreateTable(parametersDictionary["-d"], parametersDictionary["-t"]);
                    break;
                case "add-clmn": 
                        await tableService.AddColumn(parametersDictionary["-d"], parametersDictionary["-t"], parametersDictionary["-cn"], DataValueType.GetDataValueType(parametersDictionary["-ct"]).ToString(),null);
                        break;
                case "insert":
                    {
                        var values = parametersDictionary["-r"].Split(",");
                        var list = new List<Row>();
                        Row row = new Row();
                        row.Items.Add(parametersDictionary["-r"].Split(","));
                        list.Add(row);
                        await tableService.Insert(parametersDictionary["-d"], parametersDictionary["-t"], list);                        
                        break;
                    }
                case "select":
                    await tableService.Select(parametersDictionary["-d"], parametersDictionary["-t"]);
                    break;
                //case "update":
                //    {
                //        if (parameters[0] == "-d" && parameters[1] == "-t" && parameters[2] == "id")
                //        {
                //            string row = "";
                //            for (int i = 7; i < words.Count; i++)
                //            {
                //                row += words[i];
                //                row += '&';
                //            }
                //            await RequestSender.EditRow(words[2], words[4], Convert.ToInt32(words[6]), row);
                //        }
                //        break;
                //    }
                //case "intabl":
                //    {
                //        if (parameters[0] == "-d" && parameters[1] == "-t" && parameters[2] == "-t" && parameters[3] == "-t")
                //        {
                //            await RequestSender.IntersectTables(words[2], words[4], words[6], words[8]);
                //        }
                //        break;
                //    }
                //case "delete-tbl":
                //    {
                //        if (parameters[0] == "-d" && parameters[1] == "-t")
                //        {
                //            await RequestSender.DeleteTable(words[2], words[4]);
                //        }
                //        break;
                //    }
            }

        }
    }
}
