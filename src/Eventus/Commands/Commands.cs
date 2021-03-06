﻿using System;

namespace Eventus.Commands
{
    public class Command : ICommand
    {
        public Guid CorrelationId { get; }

        public Guid AggregateId { get; }

        public int TargetVersion { get; }

        public Command(Guid correlationId, Guid aggregateId, int targetVersion = -1)
        {
            if(correlationId == Guid.Empty) throw new ArgumentException(nameof(correlationId));
            if (aggregateId == Guid.Empty) throw new ArgumentException(nameof(aggregateId));

            CorrelationId = correlationId;
            AggregateId = aggregateId;
            TargetVersion = targetVersion;
        }
    }
}
