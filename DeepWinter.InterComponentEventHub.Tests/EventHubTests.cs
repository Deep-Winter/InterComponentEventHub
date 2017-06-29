#region Copyright

// //———————————————————————–
// 
// // <copyright file="EventHubTests.cs" author="Lars Winter" company="medo.check">
// 
// //     Copyright (c) medo.check. All rights reserved.
// 
// // </copyright>
// 
// //———————————————————————–

#endregion

using System;
using Xunit;

namespace DeepWinter.InterComponentEventHub.Tests
{
    public class EventHubTests
    {
        public class DataObject
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
        }

        [Fact]
        public void ComplexCallTest()
        {
            var hub = EventHub.Instance;
            var id = Guid.NewGuid();
            var received = false;
            const string text = "test";

            hub.CreateEventProxy().On<DataObject>("ComplexTestCall", result =>
            {
                Assert.True(result.Id == id);
                Assert.True(result.Text == text);
                received = true;
            });

            hub.EventDispatcher.ComplexTestCall(new DataObject {Id = id, Text = text});
            Assert.True(received);
        }

        [Fact]
        public void ComplexFailedCallTest()
        {
            var hub = EventHub.Instance;
            var received = false;

            hub.CreateEventProxy().On<DataObject>("ComplexFailedTestCall", result =>
            {
                // Expects a result type "DataObject"
                // received will not be set to true
                received = true;
            });

            Assert.Throws<EventHubException>(() =>
            {
                // Wrong call => Proxy expects "DataObject" for Event "ComplextFailedTestCall"
                hub.EventDispatcher.ComplexFailedTestCall("this must fail");

                // received must be false because proxy method has not been invoked.
                Assert.False(received);
            });
        }

        [Fact]
        public void MultiReceiverTest()
        {
            var receivedCount = 0;

            var hub = EventHub.Instance;

            // register proxy 1
            hub.CreateEventProxy().On("SimpleTestCall", result =>
            {
                Assert.True(result == "test");
                receivedCount++;
            });

            // register proxy 2
            hub.CreateEventProxy().On("SimpleTestCall", result =>
            {
                Assert.True(result == "test");
                receivedCount++;
            });

            // register proxy 3
            hub.CreateEventProxy().On("SimpleTestCall", result =>
            {
                Assert.True(result == "test");
                receivedCount++;
            });

            // One calldispatched to 3 expected receivers
            hub.EventDispatcher.SimpleTestCall("test");

            // Test receivedCount == 3 
            Assert.True(receivedCount == 3);
        }

        [Fact]
        public void SimpleCallTest()
        {
            var hub = EventHub.Instance;
            var received = false;

            hub.CreateEventProxy().On("SimpleTestCall", result =>
            {
                Assert.True(result == "test");
                received = true;
            });

            hub.EventDispatcher.SimpleTestCall("test");
            Assert.True(received);
        }
    }
}