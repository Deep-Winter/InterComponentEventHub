#region Copyright

// //———————————————————————–
// 
// // <copyright file="ActionWrapper.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

using System;

namespace DeepWinter.InterComponentEventHub
{
    internal class ActionWrapper<T> : IConcreteActionExecutor
    {
        private readonly Action<T> _action;

        public ActionWrapper(Action<T> action)
        {
            _action = action;
        }

        public Type ConcreteType => typeof(T);

        public void Execute(object obj)
        {
            _action?.Invoke((T) obj);
        }
    }
}