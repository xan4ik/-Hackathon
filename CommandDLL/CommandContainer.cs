using System;
using System.Collections.Generic;
using CommandDLL.Commands;

namespace CommandDLL
{
    public class CommandContainer
    {
        private Dictionary<string, ICommand> commands;

        public CommandContainer()
        {
            commands = new Dictionary<string, ICommand>
            {
                { "auth", new AuthenticateCommand() },
                { "ion_time", new IonTimeGetCommand() },
                { "ion_info", new IonShortInfoGetCommand() },
                { "session_report", new SessionReportGetCommand() },
                { "session_begin", new SessionBeginGetCommand() },
                { "total_tb", new TotalTechnicalBreakGetCommand() },
                { "contracts_timework", new ContractWorksByIonGetCommand() },
                { "contracts_begin", new ContractsBeginsGetCommand() },
                { "ion_names", new IonNamesGetCommand() },
                { "session_count", new SessionCountGetCommand() },
                { "email", new SendEmailCommand()}
            };
        }


        public void AddCommand(string key, ICommand command) 
        {
            if (commands.ContainsKey(key)) 
            {
                throw new Exception(key + " is alredy used");
            }
            commands.Add(key, command); 
        }

        public T RequareCommand<T>(string name)
        {
            if (commands.ContainsKey(name)) 
            {
                return (T)commands[name];
            }

            throw new KeyNotFoundException(name);
        }
    }
}
