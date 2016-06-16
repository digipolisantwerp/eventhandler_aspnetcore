# Eventhandler Toolbox

Eventhandler toolbox

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Installation](#installation)
- [Configuration in Startup.ConfigureServices](#configuration-in-startupconfigureservices)
  - [Json config file](#json-config-file)
  - [Code](#code)
- [Registering service](#registering-service)
- [Events](#events)
- [Methods](#methods)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

To add the toolbox to a project, you add the package to the project.json :

``` json
"dependencies": {
    "Digipolis.Toolbox.Eventhandler":  "1.0.1"
 }
```

In Visual Studio you can also use the NuGet Package Manager to do this.

## Configuration in Startup.ConfigureServices

The Event toolbox is registered in the _**ConfigureServices**_ method of the *Startup* class.

There are 2 ways to configure the Event toolbox :
- using a json config file
- using code

### Json config file

The path to the Json config file has to be given as argument to the _*AddEventHandler*_ method :

``` csharp
  services.AddEventHandler(opt => opt.FileName = ConfigPath + "/eventhandlerconfig.json");

```

The Event toolbox will read the given section of the json file with the following structure :

``` json
{
  "EventHandler": {
    "Version": "1",
     "MessageVersion": "1",
     "AppId": "666",
     "AppName": "SampleApi",
     "InstanceId": "123",
     "InstanceName": "MySampleInstance",
     "EventEndpointUrl": "https://myendpoint.com",
     "EventEndpointNamespace": "EventNamespace",
     "EventEndpointApikey": "6546-6544-6464-6654",
     "EventEndpointOwnerkey": "dgpls",
     "HideEventHandlerErrors": true
   }
}

```

### Code

You can also call the _*AddEventHandler*_ method, passing in the needed options directly :

``` csharp

services.AddDataAccess<MyEntityContext>(opt => opt.Version = 1, (...) );
```



## Registering service


``` csharp
public static IServiceCollection AddBusinessServices(this IServiceCollection services)
		{
            // Register your business services here, e.g. services.AddTransient<IMyService, MyService>();
            services.AddTransient<IEventHandler, EventHandler>();
           return services;
		}

```


## Events

﻿This toolbox was made to be able to send events from ASP.NET Core applications.  This allows the developers to send out events in a quick and uniform way.


The event message has the following structure :

<table>
  <tr>
    <td>Header</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>Timestamp</td>
    <td>2012-01-01T12:00:00Z</td>
    <td>Timestamp of the event.</td>
  </tr>
  <tr>
    <td>Version</td>
    <td>1</td>
    <td>Version of the event handler.</td>
  </tr>
  <tr>
    <td>Correlation</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>  CorrelationId</td>
    <td>8e82e349-b430-4c7b-a86a-b8b6133382e9</td>
    <td>Unique id of the request.</td>
  </tr>
  <tr>
    <td>  Application</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>    ApplicationId</td>
    <td>b02ea486-506a-47e4-b136-cd658eb8f805</td>
    <td>Unique id of the application that initiated the request.</td>
  </tr>
  <tr>
    <td>    ApplicationName</td>
    <td>MyUserInterface</td>
    <td>Display-name of the application that initiated the request.</td>
  </tr>
  <tr>
    <td>  Instance</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>    InstanceId</td>
    <td>24213d47-1c26-46d5-a0d1-52d9c3913935</td>
    <td>Unique id of the instance of the  application that initiated the request.</td>
  </tr>
  <tr>
    <td>    InstanceName</td>
    <td>MyUserInterface-instance3</td>
    <td>Display-name of the instance of the application that initiated the request.</td>
  </tr>
  <tr>
    <td>Source</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>  Application</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>    ApplicationId</td>
    <td>1df240eb-7d0e-42c0-9bb3-f22ad30fd353</td>
    <td>Unique id of the application that publishes the event.</td>
  </tr>
  <tr>
    <td>    ApplicationName</td>
    <td>myApp</td>
    <td>Display-name of the application that publishes the event.</td>
  </tr>
  <tr>
    <td>  Instance</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>    InstanceId</td>
    <td>e75ab07e-e96c-462c-962c-393ecfdc5726</td>
    <td>Unique id of the instance of the application that publishes the event.</td>
  </tr>
  <tr>
    <td>    InstanceName</td>
    <td>myApp-instance1</td>
    <td>Display-name  of the instance of the application that publishes the event.</td>
  </tr>
  <tr>
    <td>  Component</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>    ComponentId</td>
    <td>9da00df0-6bb3-4b22-be90-3f3bc70329ad</td>
    <td>Unique id of the component in the application that publishes the event.</td>
  </tr>
  <tr>
    <td>    ComponentName</td>
    <td>Invoicer</td>
    <td>Display-name of the components in the application that publishes the event.</td>
  </tr>
  <tr>
    <td>Host</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>  IPAddress</td>
    <td>126.35.45.2</td>
    <td>IP adress of the server where the event is being published.</td>
  </tr>
  <tr>
    <td>  ProcessId</td>
    <td>15984</td>
    <td>Process id from where the event is being sent.</td>
  </tr>
  <tr>
    <td>  ThreadId</td>
    <td>6875</td>
    <td>Thread id from where the event is being sent.</td>
  </tr>
  <tr>
    <td>Body</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>EventVersion</td>
    <td>1</td>
    <td>Version of the event (can be used by parsers).</td>
  </tr>
  <tr>
    <td>User</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>  UserName</td>
    <td>theuser</td>
    <td>User that sends the event.</td>
  </tr>
  <tr>
    <td>  IPAddress</td>
    <td>168.237.25.91</td>
    <td>IP adress of that user.</td>
  </tr>
  <tr>
    <td>Event</td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>  Type</td>
    <td>invoice:created</td>
    <td>Type of the event.</td>
  </tr>
  <tr>
    <td>  Content</td>
    <td>{"id":45987}</td>
    <td>Content of the event (can be empty if the event type speaks for itself).</td>
  </tr>
  <tr>
    <td>  Format</td>
    <td>json</td>
    <td>Format of the content (can be used by parsers).</td>
  </tr>
</table>


The **_Header_** contains the metadata, the **_Body_** the real message.

The **_display-friendly_** names are a tool for the subscribers of the events. Id’s alone are usually not very user-friendly.  By using the display-friendly names subscribes can get the name right away.

The **_version_** fields can be used when logic needs to be executed on the messages (eg by parsers or (de)serializers). Between versions the structure or content could have changed.  


## Methods

The toolbox offers methods to send out events in a user-friendly manner.  The metadata fields are filled in by the toolbox.

(Refer to the samples project to see a working demo)

* Publish<DataType>(topic, eventtype, [principal], componentId, componentname, optional eventFormat)

    	The toolbox will serialize the datatype to JSON and send it as content.

* PublishString(topic, eventtype, string, componentId, componentname, optional eventFormat)

  	The toolbox will convert the string to a json with 1 field and send it as content.

* PublishJson(topic, eventtype, json-string, componentId, componentname, optional eventFormat)

  	The toolbox will pass the json-string literally as content.

For **event types** the following format is recommended : **_entity:operation_** (eg person:changed, invoice:created, payment:recieved)
