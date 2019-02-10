﻿using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain.Check;
using Curds.Application.DateTimes;
using Gouda.Application.Persistence;
using Gouda.Application.Communication;

namespace Gouda.Infrastructure.Check
{
    public class Scheduler : IScheduler
    {
        public IDateTime Time { get; set; }
        public IPersistence Persistence { get; set; }
        public ISender Sender { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Add(int definitionID)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Reschedule(int definitionID, DateTimeOffset rescheduleTime)
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
