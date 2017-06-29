#region Copyright

// //———————————————————————–
// 
// // <copyright file="IEventProxy.cs" author="Lars Winter" company="medo.check">
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
    /// <summary>
    /// Event Proxy
    /// </summary>
    public interface IEventProxy
    {
        /// <summary>
        /// Register a action called by event dispatcher
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="action">Action to be called</param>
        void On(string method, Action<string> action);

        /// <summary>
        /// Register a action called by event dispatcher
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="action">Action to be called</param>
        /// <typeparam name="T">Type of data load object</typeparam>
        void On<T>(string method, Action<T> action);
    }
}