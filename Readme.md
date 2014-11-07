#Dashen

##What is it?
A a self hosted web dashboard for any .net project.
It produces dashboards like this:

![Dashen Webui](resources/img/dashen.png "Dashen Webui")

##How do I install it?

```powershell
install-package Dashen
```

##How do I configure it?

```csharp
//everything has a sensible default
var ui = new Dashboard(new DashenConfiguration());

//add a model to display.
ui.RegisterModel(new Definition
{
	//The type of widget you want.  Text, List, Graph or BarGraph.
	Create = () => new TextControlViewModel { Content = "Test" },

	//The title to widget for this box.
	Heading = "Some Text",

	//how often to update this widget, default is 10 seconds.
	Interval = new TimeSpan(0,0,1),

	//How many columns to take up. Number between 1 and 12.
	Width = 2,
});

```

You have two options for starting the dashboard, either self hosted, or attaching to an existing WebApi configuration:

###SelfHost:
```csharp
var ui = new Dashboard(new DashenConfiguration {
	ListenOn = new Uri("http://localhost:8080"),
	Prefix = "Dashen"
});

ui.Start();
//Visit http://localhost:8080/Dashen/ to see the dashboard
```

###WebApi
```csharp
var ui = new Dashboard(new DashenConfiguration {
	Prefix = "Dashen"
});

ui.HookTo(config);
//visit your api /dashen to see the dashboard
```

Note for hooking to an existing WebApi, if you are overriding the DependencyResolver, you **must** place the `.HookTo(config)` after your override.

##Detailed Configuration

* [ListenOn](#listenon)
* [Prefix](#prefix)
* [Title](#title)
* [Version](#version)
* [HighlightColor](#highlightcolor)
* [LowlightColor](#lowlightcolor)
* [EnableConsoleLog](#enableconsolelog)
* [DisableConsoleLog](#disableconsolelog)
* [MessageHandlers](#messagehandlers)
* [AddResource](#addresource)
* [AddWidgetTypeAndView](#addwidgettypeandview)

###ListenOn
>Specifies the uri and port for the web dashboard to run under.  Only applicable when selfhosting (`ui.Start()`)

```
config.ListenOn = new Uri("http://localhost:8080")
```

###Prefix
>Specifies a route prefix to use to view the dashboard.

```
config.ListenOn = new Uri("http://localhost:8080");
config.Prefix = "Dashen";
```
Would mean navigating to `http://localhost:8080/dashen` to see the dashboard.

###Title
>Sets the main header text of the dashboard.

```
config.Title = "My Awesome Service";
```

###Version
>Sets the version header of the dashboard.

```
config.Version = typeof(Program).Assembly.Version.ToString();
```

###HighlightColor
>A bright colour for highlighting in the dashboard.

```
config.HighlightColor = Color.LightGreen;
```

###LowlightColor
>A dark colour for highlighting in the dashboard.  Used by Graphs, Progress Circles.

```
config.LowlightColor = Color.DarkGreen;
```

###EnableConsoleLog
>Adds a MessageHandler which outputs to the console.  Only has an effect before `ui.Start()` or `ui.HookTo()` is called.

```
config.EnableConsoleLog()
```

###DisableConsoleLog
>Removes the MessageHandler which outputs to the console.  Only has an effect before `ui.Start()` or `ui.HookTo()` is called.

```
config.DisableConsoleLog()
```

###MessageHandlers
>Allows adding of custom MessageHandlers.
>Only has an effect before `ui.Start()` or `ui.HookTo()` is called.
>You probably only need to use this for self hosting.

```
config.MessageHandlers
```

###AddResource
>Adds a static resource into Dashen.  Generally used in conjunction with the HtmlWidget.

```
config.AddResource("/img/good.png", fileStream, "image/png");
```

###AddWidgetTypeAndView
>Used to register custom widgets with Dashen.

```
config.AddWidgetTypeAndView<FakeWidgetModel>(Encoding.UTF8.GetBytes(view));
```
