#region Copyright

// //———————————————————————–
// 
// // <copyright file="EventDispatcher.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace DeepWinter.InterComponentEventHub
{
    internal class EventDispatcher : DynamicObject
    {
        private readonly List<EventProxy> _proxies;

        public EventDispatcher()
        {
            _proxies = new List<EventProxy>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            foreach (var proxy in _proxies)
            {
                var simple = proxy.GetSimpleAction(binder.Name);
                if (simple != null)
                {
                    InvokeSimpleCall(args, simple);
                    continue;
                }

                var complex = proxy.GetComplexExecutor(binder.Name);
                if (complex == null) continue;
                try
                {
                    InvokeComplexCall(args, complex);
                }
                catch (Exception error)
                {
                    throw new EventHubException(Properties.Resources.EventDispatcher_TryInvokeMember_Error_while_invoking_complex_call__See_InnerException, error);
                }
            }

            return true;
        }

        private static void InvokeComplexCall(IReadOnlyList<object> args, IConcreteActionExecutor complex)
        {
            if (args == null || args.Count == 0)
            {
                complex.Execute(null);
                return;
            }

            if (args[0] is string)
            {
                complex.Execute(JsonConvert.DeserializeObject(args[0].ToString(), complex.ConcreteType));
                return;
            }

            complex.Execute(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(args[0]),
                complex.ConcreteType));
        }

        private static void InvokeSimpleCall(IReadOnlyList<object> args, Action<string> simple)
        {
            if (args == null || args.Count == 0)
            {
                simple.Invoke(null);
                return;
            }
            simple.Invoke(args[0].ToString());
        }

        internal void AddProxy(EventProxy proxy)
        {
            _proxies.Add(proxy);
        }
    }
}