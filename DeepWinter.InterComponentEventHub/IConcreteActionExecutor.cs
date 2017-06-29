#region Copyright

// //———————————————————————–
// 
// // <copyright file="IConcreteActionExecutor.cs" author="Lars Winter" company="medo.check">
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
    internal interface IConcreteActionExecutor
    {
        Type ConcreteType { get; }
        void Execute(object obj);
    }
}