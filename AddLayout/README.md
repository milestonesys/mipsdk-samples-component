---
description: A view layout is the template for creating views in the
  Smart Client. The layout controls how many items can be displayed, how
  the items are sized, and how they share the space in the view.
keywords: Component integration
lang: en-US
title: Adding view layout using Configuration API
---

# Adding view layout using Configuration API

A view layout is the template for creating views in the Smart Client.
The layout controls how many items can be displayed, how the items are
sized, and how they share the space in the view.

The AddLayout sample demonstrates how to add a view layout using strongly typed Configuration API classes. 

![](AddLayoutSC.png)

*Screen capture showing a Smart Client setup, creating new view dialogs.*

## How to use the sample

1. Build and run the IconToString tool that can be found in the
    `AddLayout` folder. When running it, you must supply two
    parameters:
    - `"C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout\IconToString\icon.png"`
    - `"C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout\IconToString\Layout.xml"`
2. Build AddLayout sample.
3. Copy the newly created file
    (`C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout\IconToString\LayoutNEW.xml`)
    to the bin folder of AddLayout sample
    (`C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout\bin\<Platform>\<Configuration>\`)
4. Run AddLayout sample.
5. In XProtect Smart Client, go into setup mode and see that you have a
    new layout named Layout.

### Layout definition XML file

The layout is defined by specifying the layout in XML format. The
definition XML file (before adding an icon) looks like this:

~~~ xml
<ViewLayout>
  <ViewItems>
    <ViewItem>
      <Position><X>0</X><Y>0</Y></Position>
      <Size><Width>1000</Width><Height>200</Height></Size>
    </ViewItem>
    <ViewItem>
      <Position><X>0</X><Y>200</Y></Position>
      <Size><Width>1000</Width><Height>800</Height></Size>
    </ViewItem>
  </ViewItems>
</ViewLayout>
~~~

The layout can be modified by changing this XML file.

### Adding an icon

The layout can be improved by adding an icon. The icon graphics must be
converted to Base64String and then inserted into the XML. The
IconToString tool sample does this. The IconToString tool sample takes
two parameters:

- The path and name of an icon graphics file
- The path and name of the layout definition XML file

The tool will output a new XML file. The name of this new file will be
the name of the input XML file extended with \"NEW\". For example, the
input file Layout.xml results in a new file named LayoutNEW.xml.

### Removing layouts

It should be considered that if you make a tool to add layouts,
you will probably need a tool that can remove layouts.
The sample only demonstrates how to add layout but for initial testing
you can use the Config API Client sample to remove layouts.

![](ConfigAPIClientLayout.png)

*Screen capture showing the Config API Client showing the layout added
by the sample.*

## The sample demonstrates

- Login default Windows credentials
- Usage of strongly typed Configuration API classes to add a
    view-layout to the VMS

## Using

- VideoOS.Platform.ConfigurationItems
- VideoOS.Platform.ConfigurationItems.ManagementServer
- VideoOS.Platform.ConfigurationItems.LayoutFolder.AddLayout

## Environments

- .NET library MIP Environment

## Visual Studio C\# project

- [AddLayout.csproj](javascript:openLink('..\\\\ComponentSamples\\\\AddLayout\\\\AddLayout.csproj');)
- [IconToString.csproj](javascript:openLink('..\\\\ComponentSamples\\\\AddLayout\\\\IconToString\\\\IconToString.csproj');)
