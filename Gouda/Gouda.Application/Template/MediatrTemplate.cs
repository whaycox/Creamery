using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Gouda.Application.Template
{
    public abstract class MediatrTemplate
    {
        protected CancellationToken TestCancellationToken = new CancellationToken();
    }
}
