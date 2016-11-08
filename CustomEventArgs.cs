//  --------------------------------------------------------------------------------------
//  <copyright file="CustomEventArgs.cs" company="Copper Star Systems, LLC">
//     Copyright 2016 Copper Star Systems, LLC. All Rights Reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------

using System;

namespace EventsExample
{
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string eventData, string otherEventData)
        {
            EventData = eventData;
            OtherEventData = otherEventData;
        }

        // These are just fields containing additional data to pass back
        // to subscribers.  They don't have to be strings, they can be any
        // data type, just like a regular class.
        public string EventData { get; private set; }
        public string OtherEventData { get; private set; }
    }
}