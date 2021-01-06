using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommandContext _commandContext;

        public SqlCommanderRepo(CommandContext commandContext)
        {
            _commandContext = commandContext;
        }
        public IEnumerable<Command> GetAllCommanders()
        {
            return _commandContext.Commands.ToList();
        }

        public Command GetCommandById(int Id)
        {
            return _commandContext.Commands.FirstOrDefault(command => command.Id == Id);

        }
    }
}
