#region Copyright

// //———————————————————————–
// 
// // <copyright file="Program.cs" author="Lars Winter" company="medo.check">
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
    internal class Program
    {
        private static void Main(string[] args)
        {
            var slaves = new[]
            {
                new SlaveComponent(),
                new SlaveComponent(),
                new SlaveComponent(),
                new SlaveComponent(),
                new SlaveComponent(),
                new SlaveComponent()
            };

            var master = new MasterComponent {ExpectedSlaveCount = slaves.Length};

            master.CallYourSlaves();

            Console.ReadLine();
        }
    }
}