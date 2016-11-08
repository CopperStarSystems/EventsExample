//  --------------------------------------------------------------------------------------
//  <copyright file="BasicEventData.cs" company="Copper Star Systems, LLC">
//     Copyright 2016 Copper Star Systems, LLC. All Rights Reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------

namespace EventsExample
{
    public class BasicEventData
    {
        public BasicEventData(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}