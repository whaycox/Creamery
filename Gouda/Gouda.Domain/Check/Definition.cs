using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Check
{
    using Enumerations;
    using Communication;

    public abstract class Definition : NamedEntity
    {
        public Satellite Satellite { get; }
        public Status Status { get; private set; }
        public Request Request => new Request(Name, Arguments);

        public abstract Dictionary<string, string> Arguments { get; }

        public Definition(Satellite satellite)
        {
            Satellite = satellite;
        }

        public void Update(Status newStatus) => Status = newStatus;

        public override Entity Clone()
        {
            throw new NotImplementedException();
        }

        public abstract Status Evaluate(Response response);
    }

    public abstract class Definition<T> : Definition where T : Response
    {
        public Definition(Satellite satellite)
            : base(satellite)
        { }

        public sealed override Status Evaluate(Response response) => Evaluate(BuildResponse(response));

        public abstract T BuildResponse(Response response);
        protected abstract Status Evaluate(T response);
    }
}
