//  --------------------------------------------------------------------------------------
//  <copyright file="EventSender.cs" company="Copper Star Systems, LLC">
//     Copyright 2016 Copper Star Systems, LLC. All Rights Reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------

using System;
using System.Threading;

namespace EventsExample
{
    // This class illustrates the 'Publish' side of .NET event handlers.  This class exposes
    // three different events with various payloads, and also demonstrates using the built-in 
    // default and generic EventHandler delegates provided by .NET.
    public class EventSender
    {
        // This is a delegate defining a handler for a custom event that passes an application-
        // specific EventArgs to subscribers when the event fires.
        public delegate void CustomEventHandler(object sender, CustomEventArgs e);

        const int DelayFactor = 1000;

        // EventHandler is a delegate provided by the .NET framework that matches a signature of:
        // void HandlerMethod(object sender, EventArgs e)
        public event EventHandler BuiltInDefaultHandlerEvent;

        // EventHandler<T> is a delegate provided by the .NET framework that matches a signature of:
        // void HandlerMethod(object sender, T args)
        // This delegate allows you to specify the type of the event's payload (BasicEventData in this example)
        // Note that T does not have to derive from EventArgs.
        public event EventHandler<BasicEventData> BuiltInGenericEvent;

        // This is a completely custom event based on the CustomEventHandler delegate defined above.
        // Per the delegate's specification, the handler's signature must be:
        // void HandlerMethod(object sender, CustomEventArgs e)
        public event CustomEventHandler CustomEvent;

        public void DoWorkThatRaisesEvents()
        {
            NotifyConsole("EventSender:  Preparing to fire individual events");
            // We don't have any real work to do, so we're just going to fire off all our events like
            // it's the 4th of July finale :)
            RaiseDefaultHandlerEvent();
            GetSomeRest();
            RaiseBuiltInGenericEvent();
            GetSomeRest();
            RaiseCustomEvent();
            GetSomeRest();
            NotifyConsole("EventSender:  Firing Built In Generic Event 5 times.");
            // Events can be raised repeatedly
            for (var i = 0; i < 5; i++)
            {
                RaiseBuiltInGenericEvent($"Repeated Invocation {i}");
                GetSomeRest();
            }
        }

        void GetSomeRest()
        {
            // This just slows things down so the output doesn't just barf to the console.
            Thread.Sleep(DelayFactor);
        }

        void NotifyConsole(string message)
        {
            Console.WriteLine(message);
        }

        void RaiseBuiltInGenericEvent(string message = "BasicEventData Message")
        {
            NotifyConsole("EventSender:  Raising Built In Generic Event");

            // This is the correct pattern for invoking events in order to avoid a possible
            // race condition that could occur if the last event subscriber unsubscribes 
            // from a different thread.
            var handler = BuiltInGenericEvent;

            // If nobody has attached to the event, the handler will be null so don't
            // try to execute the handler.
            if (handler != null)
                handler(this, new BasicEventData(message));
        }

        void RaiseCustomEvent()
        {
            NotifyConsole("EventSender:  Raising Custom Event");
            var handler = CustomEvent;
            if (handler != null)
                // The CustomEventHandler specifies a delegate that takes a CustomEventArgs
                handler(this, new CustomEventArgs("Some Data", "Some other data."));
        }

        void RaiseDefaultHandlerEvent()
        {
            NotifyConsole("EventSender:  Raising Built In Default Event");
            var handler = BuiltInDefaultHandlerEvent;
            if (handler != null)
                // The default EventHandler provided by .NET uses an EventArgs object to
                // pass data back to the caller.
                handler(this, new EventArgs());
        }
    }
}