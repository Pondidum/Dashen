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
