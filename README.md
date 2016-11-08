# EventsExample

An intermediate example of the Event Handling subsystem in .NET.
----------------------------------------------------------------
###Purpose:

I created this example for my CodeMentor students studying the .NET
event system.  

###Getting Started:
1. Clone or download the source code
2. Open the solution in Visual Studio
3. Make sure that the Console app is set as the startup project
4. Press F5 to build and start the project

A console window will open and the program will execute.  The
application has three separate phases:
* Raise all events in EventSender without hooking any events in EventReceiver
* Hook all EventSender events, then raise all EventSender events
* Unhook some EventSender events, then raise all EventSender events

The output indicates the chronological order in which the events are fired and
received.

###Walkthrough:
Console App
  Just runs the application - Creates an EventSender and EventReceiver and 
  calls the EventReceiver.DoWork() method.
  
EventSender:  Has a DoWorkThatRaisesEvents method that triggers the sequential
raising of all events exposed by EventSender.  

This class:
  * Illustrates different implementations for publishing events
    * Simple notification
    * Simple notification with strongly-typed data
    * Simple notification with application-specific delegate and EventArgs
    * Basic best practices when publishing events
 
EventReceiver:  Has a DoWork method that executes the DoWorkThatRaisesEvents method
multiple times, with events hooked and unhooked.

This class:
   * Illustrates the concepts behind subscribing to events
     * Programmatically subscribing to events
     * Programmatically unsubscribing from events
     * Attaching multiple handlers to the same event



###Topics Illustrated in This Example:
1. Delegates
  * Built-in .NET EventHandler and EventHandler<T> delegates
  * Custom delegates in the context of the events subsystem
2. Events
  * Publishing events
    * Using built-in .NET EventHandler and EventHandler<T> event
    signatures
    * Using custom delegate event signature
  * Subscribing and Unsubscribing from events
  * Passing data with Events
    * Built-in .NET EventArgs payload
    * Custom POCO DTO
    * Custom EventArgs implementation
    
