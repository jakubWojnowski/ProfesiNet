﻿using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Shared.Contexts
{
    internal class Context : IContext
    {
        public string RequestId { get; } = $"{Guid.NewGuid():N}";
        public string TraceId { get; }
        public IIdentityContext Identity { get; }

        internal Context()
        {
        }

        public Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
        {
        }

        internal Context(string traceId, IIdentityContext identity)
        {
            TraceId = traceId;
            Identity = identity;
        }

        public static IContext Empty => new Context();
    }
}