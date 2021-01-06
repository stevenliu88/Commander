﻿using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommanders()
        {
            var commands = new List<Command> 
            {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle" },
                new Command { Id = 0, HowTo = "Cut bread", Line = "Get a knife", Platform = "chopping" },
                new Command { Id = 0, HowTo = "cooking", Line = "teabag", Platform = "testing" }
            };

            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle" };
        }
    }
}