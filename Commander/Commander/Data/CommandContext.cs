﻿using Commander.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class CommandContext: DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options): base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
