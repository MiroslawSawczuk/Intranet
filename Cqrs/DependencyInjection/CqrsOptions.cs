using System;

namespace Cqrs.DependencyInjection
{
    public class CqrsOptions
    {
        public bool AutoRegister { get; set; }
        public string HandlersProjectName { get; set; }
    }
}
