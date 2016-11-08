//  --------------------------------------------------------------------------------------
//  <copyright file="EventReceiver.cs" company="Copper Star Systems, LLC">
//     Copyright 2016 Copper Star Systems, LLC. All Rights Reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------

using System;

namespace EventsExample
{
    // This class represents the "Subscriber" side of the .NET event system.  This class knows about
    // another class (EventSender) that exposes several events to which EventReceiver wishes to subscribe.
    // EventReceiver does so by:
    // - Declaring appropriate EventHandlers matching the delegate signature for each event
    // - Hooking each EventHandler to the appropriate event exposed by EventRaiser.
    //
    // It's important to note that a class can not only subscribe to events from another class, but can also
    // publish its own events for other classes to consume.
    public class EventReceiver
    {
        readonly EventSender eventSender;

        public EventReceiver(EventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public void DoWork()
        {
            NotifyConsole("EventReceiver:  Starting DoWork");

            ExecuteCodeWithoutHookingAnyEvents();
            Pause();
            ExecuteCodeWithEventsHooked();
            Pause();
            UnhookSomeEvents();
            ExecuteCodeAfterUnhookingEvents();
            Pause();
            // After we're done working with the sender, unhook our eventhandlers
            // This would usually be done as part of the Dispose() pattern when 
            // finished working with a class.
            UnhookAllEvents();
        }

        void DoWorkThatRaisesEvents()
        {
            eventSender.DoWorkThatRaisesEvents();
        }

        void ExecuteCodeAfterUnhookingEvents()
        {
            NotifyConsole("EventReceiver:  Invoking DoWorkThatRaisesEvents after unhooking some events.\n");

            DoWorkThatRaisesEvents();

            NotifyConsole("\nEventReceiver:  After invoking DoWorkThatRaisesEvents with some unhooked events.");
            NotifyConsole("\n===== Notice above that only one Event above was handled, the CustomEvent handler. " +
                          " It was only handled once because we unhooked the HandleCustomEvent handler. =====");
        }

        void ExecuteCodeWithEventsHooked()
        {
            HookEvents();
            NotifyConsole("EventReceiver:  Invoking DoWorkThatRaisesEvents after hooking events.\n");

            DoWorkThatRaisesEvents();

            NotifyConsole("\nEventReceiver:  After invoking DoWorkThatRaisesEvents with hooked events.");
            NotifyConsole(
                "\n===== Notice above that each event raised by EventSender above has a corresponding Received message from EventReceiver. =====");
            NotifyConsole(
                "\n===== Also notice that there are two Received messages for only one firing of the CustomEvent.  This is because we configured" +
                " multiple handlers for that event. =====\n");
        }

        void ExecuteCodeWithoutHookingAnyEvents()
        {
            // Do work that raises events, but without having hooked the
            // events.  Basically we won't receive any notifications for this 
            // invocation.
            NotifyConsole("EventReceiver:  Invoking DoWorkThatRaisesEvents without hooking events.\n");

            DoWorkThatRaisesEvents();

            NotifyConsole("EventReceiver:  After invoking DoWorkThatRaisesEvents with unhooked events.");
            NotifyConsole(
                "\n===== Notice above that EventSender raised its events but EventReceiver did not listen to them. =====\n");
        }

        // This is the handler for the BuiltInDefaultHandlerEvent exposed by EventSender.
        // Note that the signature is (object sender, EventArgs e), matching the
        // default .NET EventHandler delegate.
        void HandleBuiltInDefaultHandlerEvent(object sender, EventArgs e)
        {
            // The console line for this will look weird, ending in "...Event:  System.EventArgs".
            // This is because the default .NET EventArgs class doesn't really have any useful properties
            // on it, and its ToString() just returns the default type name.
            Console.WriteLine("EventReceiver:  Received Built in Default Handler Event: {0}\n", e);
        }

        // This is the handler for the BuiltInGenericEvent exposed by EventSender.
        // Note that the signature is (object, BasicEventData), matching the
        // EventHandler<BasicEventData> declaration.
        void HandleBuiltInGenericEvent(object sender, BasicEventData e)
        {
            Console.WriteLine("EventReceiver:  Received Built In Generic Event:  {0}\n", e.Message);
        }

        // This is the handler for the CustomEvent exposed by EventSender.
        // Note that the signature is (object, CustomEventArgs), matching
        // the CustomEventHandler delegate definition in EventSender
        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine("EventReceiver:  Received Custom Event, Primary Handler: {0}", e.EventData);
        }

        // Just a little method illustrating a secondary event handler that acts on different data
        // from the CustomEventArgs payload.
        void HandleCustomEvent2(object sender, CustomEventArgs e)
        {
            Console.WriteLine("EventReceiver:  Received Custom Event, Secondary Handler:  {0}\n", e.OtherEventData);
        }

        void HookEvents()
        {
            NotifyConsole("EventReceiver:  Hooking events");
            // This is how we attach a block of code (our EventHandler) to an 
            // Event for which we wish to receive notifications.
            //
            // The += syntax reflects the fact that multiple handlers may be bound
            // to a given Event.
            eventSender.BuiltInDefaultHandlerEvent += HandleBuiltInDefaultHandlerEvent;
            eventSender.BuiltInGenericEvent += HandleBuiltInGenericEvent;

            // Multiple handlers can be attached to a given event.  In this case, when you see
            // the EventSender fire a CustomEvent, both HandleCustomEvent and HandleCustomEvent2
            // will be executed.
            eventSender.CustomEvent += HandleCustomEvent;
            eventSender.CustomEvent += HandleCustomEvent2;
        }

        void NotifyConsole(string message)
        {
            Console.WriteLine(message);
        }

        void Pause()
        {
            NotifyConsole("\nPress any key to continue.");
            Console.ReadKey();
        }

        // It's important to remember to unhook your events in order to avoid memory leaks.
        void UnhookAllEvents()
        {
            NotifyConsole("EventReceiver:  Unhooking events\n");
            // Event hooks must be removed individually, there is no way to just say:
            // "Hey, eventSender, Stop sending custom events to anyone!"
            // This makes sense because other classes may also have hooked into that event
            // and might still be interested in its messages.  If we could do the "Stop sending"
            // functionality above, it would likely break those other classes' functionality.
            eventSender.BuiltInDefaultHandlerEvent -= HandleBuiltInDefaultHandlerEvent;
            eventSender.BuiltInGenericEvent -= HandleBuiltInGenericEvent;
            eventSender.CustomEvent -= HandleCustomEvent;
            eventSender.CustomEvent -= HandleCustomEvent2;
        }

        void UnhookSomeEvents()
        {
            // Just unhooks some of the bound events to illustrate that hooking/unhooking 
            // can occur at runtime.
            eventSender.BuiltInDefaultHandlerEvent -= HandleBuiltInDefaultHandlerEvent;
            eventSender.BuiltInGenericEvent -= HandleBuiltInGenericEvent;
            eventSender.CustomEvent -= HandleCustomEvent;
        }
    }
}