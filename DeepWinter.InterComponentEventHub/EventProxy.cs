#region Copyright

// //———————————————————————–
// 
// // <copyright file="EventProxy.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

using System;
using System.Collections.Generic;

namespace DeepWinter.InterComponentEventHub
{
    internal class EventProxy : IEventProxy
    {
        private readonly Dictionary<string, IConcreteActionExecutor> _complexCalls;
        private readonly Dictionary<string, Action<string>> _simpleCalls;

        public EventProxy()
        {
            _simpleCalls = new Dictionary<string, Action<string>>();
            _complexCalls = new Dictionary<string, IConcreteActionExecutor>();
        }

        public void On(string method, Action<string> action)
        {
            if (_simpleCalls.ContainsKey(method.ToLower())) return;
            _simpleCalls.Add(method.ToLower(), action);
        }

        public void On<T>(string method, Action<T> action)
        {
            if (_complexCalls.ContainsKey(method.ToLower())) return;

            var executor =
                (IConcreteActionExecutor) Activator.CreateInstance(typeof(ActionWrapper<>).MakeGenericType(typeof(T)),
                    action);
            _complexCalls.Add(method.ToLower(), executor);
        }

        internal Action<string> GetSimpleAction(string method)
        {
            return _simpleCalls.ContainsKey(method.ToLower()) ? _simpleCalls[method.ToLower()] : null;
        }

        internal IConcreteActionExecutor GetComplexExecutor(string method)
        {
            return _complexCalls.ContainsKey(method.ToLower()) ? _complexCalls[method.ToLower()] : null;
        }
    }
}