using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlaNium.Desktop.Driver.CommandExecutors;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver {

    internal class CommandExecutorDispatchTable {

        private Dictionary<string, Type> commandExecutorsRepository;


        public CommandExecutorDispatchTable() {
            this.ConstructDispatcherTable();
        }


        public CommandExecutorBase GetExecutor(string commandName) {
            if (this.commandExecutorsRepository.TryGetValue(commandName, out var executorType)) {
            }
            else {
                executorType = typeof(NotImplementedExecutor);
            }

            return (CommandExecutorBase)Activator.CreateInstance(executorType);
        }


        private void ConstructDispatcherTable() {
            this.commandExecutorsRepository = new Dictionary<string, Type>();

            const string executorsNamespace = "FlaNium.Desktop.Driver.CommandExecutors";

            var q =
                (from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace.Contains(executorsNamespace)
                    select t).ToArray();

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                var localField = field;
                var executor = q.FirstOrDefault(x => x.Name.Equals(localField.Name + "Executor"));
                if (executor != null) {
                    this.commandExecutorsRepository.Add(localField.GetValue(null).ToString(), executor);
                }
            }
        }

    }

}