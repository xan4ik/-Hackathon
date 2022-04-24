using System;
using System.Collections.Generic;

namespace Command
{
    public class CommandContainer
    {
        private Dictionary<string, ICommand> commands;

        public CommandContainer()
        {
            commands = new Dictionary<string, ICommand>();

            commands.Add("auth", new AuthenticateCommand());
            commands.Add("ion_time", new IonTimeGetCommand());
            commands.Add("ion_info", new IonShortInfoGetCommand());
            commands.Add("session_report", new SessionReportGetCommand());
            commands.Add("session_begin", new SessionBeginGetCommand());
            commands.Add("total_tb", new TotalTechnicalBreakGetCommand());
            commands.Add("contracts_timework", new ContractWorksByIonGetCommand());
            commands.Add("contracts_begin", new ContractsBeginsGetCommand());
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
