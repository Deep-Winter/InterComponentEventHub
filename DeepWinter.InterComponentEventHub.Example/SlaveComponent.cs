#region Copyright

// //———————————————————————–
// 
// // <copyright file="SlaveComponent.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

using System;

namespace DeepWinter.InterComponentEventHub.Example
{
    public class SlaveComponent
    {
        public SlaveComponent()
        {
            var proxy = EventHub.Instance.CreateEventProxy();

            proxy.On("MasterCall", OnMasterCall);
        }

        private void OnMasterCall(string call)
        {
            if (call == "answer")
            {
                Console.WriteLine("Slave says: \"Yes my master! I'am here.\"");

                EventHub.Instance.EventDispatcher.SlaveAnswer("Yes my master!");
            }
        }
    }
}