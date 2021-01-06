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

        public void CreateCommand(Command cmd)
        {
            if (cmd == null) 
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _commandContext.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null) {
                throw new ArgumentNullException();
            }
            _commandContext.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommanders()
        {
            return _commandContext.Commands.ToList();
        }

        public Command GetCommandById(int Id)
        {
            return _commandContext.Commands.FirstOrDefault(command => command.Id == Id);

        }

        public bool SaveChanges()
        {
            return (_commandContext.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            // Nothing
            _commandContext.Update(cmd);
        }
    }
}
