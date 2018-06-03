using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Core360.Accounting.Services.Scripting;

namespace A.Core.Scripting
{
    public partial interface IScriptingAPI
    {
        Task<T> RunAsync<T, TGlobal>(Type caller, string script, GlobalContainer<TGlobal> scriptData, IList<Assembly> additionalAssemblies = null, params string[] imports);

        Task RunAsync<TGlobal>(Type caller, string script, GlobalContainer<TGlobal> scriptData, IList<Assembly> additionalAssemblies = null, params string[] imports);
    }
}
