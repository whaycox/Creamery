﻿using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain;

namespace Gouda.Application.Check
{
    public interface IScheduler
    {
        Curds.Application.DateTimes.IDateTime Time { get; set; }
        Persistence.IPersistence Persistence { get; set; }

        void Add(int definitionID);
        void Reschedule(int definitionID, DateTimeOffset rescheduleTime);

        void Start();
        void Stop();

        void Pause();
        void Resume();
    }
}
