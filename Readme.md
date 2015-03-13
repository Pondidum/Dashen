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
var dashboard = DashboardBuilder.Create(new DashboardConfiguration
{
	ListenOn = new Uri("http://localhost:3030"),
	ApplicationName =  "UnitTestRunner",
	ApplicationVersion = "1.3.3.7"
});

//add a model to display.
dashboard.Add<TextModel>(model =>
{
	model.Title = "Header";
	model.Columns = 6;	//how many columns wide to be, between 1 and 12
	model.Text = "Testing";
});

dashboard.Start();
```

##Detailed Configuration

* [ListenOn](#listenon)
* [ApplicationName](#applicationname)
* [ApplicationVersion](#applicationversion)

###ListenOn
>Specifies the uri and port for the web dashboard to run under.

```
config.ListenOn = new Uri("http://localhost:8080");
```

###ApplicationName
>Sets the main header text of the dashboard.

```
config.ApplicationName = "My Awesome Service";
```

###ApplicationVersion
>Sets the version header of the dashboard.

```
config.ApplicationVersion = typeof(Program).Assembly.Version.ToString();
```

###Resources
>Allows specifying custom resources, such as images

```
//this resource will be available under /static/user/test.png
config.Resources = new[] {
	new Resrouce("test.png", "image/png", new FileStream(@"c:\test.png"))
};
```
