﻿using System;
using Eventus.Commands;

namespace Eventus.Samples.Core.Commands
{
    public class CreateAccountCommand : Command
    {
        public string Name { get; }

        public CreateAccountCommand(Guid correlationId, Guid accountId, string name)
            : base(correlationId, accountId)
        {
            Name = name;
        }
    }
}