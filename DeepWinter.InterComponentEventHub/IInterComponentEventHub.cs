#region Copyright

// //———————————————————————–
// 
// // <copyright file="IInterComponentEventHub.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

namespace DeepWinter.InterComponentEventHub
{
    /// <summary>
    /// The main Interface grants access to the EventDispatcher and
    /// create new HubProxies
    /// </summary>
    public interface IInterComponentEventHub
    {
        /// <summary>
        /// EventDispachter for sending events
        /// </summary>
        dynamic EventDispatcher { get; }

        /// <summary>
        /// Creates a new EventProxy
        /// </summary>
        /// <returns>new EventProxy</returns>
        IEventProxy CreateEventProxy();
    }
}