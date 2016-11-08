//  --------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Copper Star Systems, LLC">
//     Copyright 2016 Copper Star Systems, LLC. All Rights Reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------

using System;

namespace EventsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sender = new EventSender();
            var receiver = new EventReceiver(sender);
            receiver.DoWork();
            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }
    }
}