#region Copyright

// //———————————————————————–
// 
// // <copyright file="EventHub.cs" author="Lars Winter" company="medo.check">
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
    /// EventHub Class
    /// </summary>
    public class EventHub : IInterComponentEventHub
    {
        private static EventHub _instance;

        /// <summary>
        /// Singleton Instance of the event hub
        /// </summary>
        public static IInterComponentEventHub Instance => _instance ?? (_instance = new EventHub());

        private EventHub()
        {
            EventDispatcher = new EventDispatcher();
        }

        /// <summary>
        /// The EventDispatcher
        /// </summary>
        public dynamic EventDispatcher { get; }

        /// <summary>
        /// Create a new EventProxy
        /// </summary>
        /// <returns>The EventProxy</returns>
        public IEventProxy CreateEventProxy()
        {
            var proxy = new EventProxy();
            EventDispatcher.AddProxy(proxy);
            return proxy;
        }
    }
}