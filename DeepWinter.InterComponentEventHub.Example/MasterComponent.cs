#region Copyright

// //———————————————————————–
// 
// // <copyright file="MasterComponent.cs" author="Lars Winter" company="medo.check">
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
    public class MasterComponent
    {
        private int _slaveCount;

        public MasterComponent()
        {
            var proxy = EventHub.Instance.CreateEventProxy();

            proxy.On("SlaveAnswer", OnSlaveCall);
        }

        public int ExpectedSlaveCount { get; set; }

        public void CallYourSlaves()
        {
            Console.WriteLine("Master says: \"I'm your master!\"");
            Console.WriteLine("Master says: \"Are you listen to me?\"");

            EventHub.Instance.EventDispatcher.MasterCall("answer");
        }

        private void OnSlaveCall(string anwser)
        {
            _slaveCount++;
            if (_slaveCount == ExpectedSlaveCount)
                Console.WriteLine("Master says: \"You are all there.\"");
        }
    }
}