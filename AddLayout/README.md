---
description: A view layout is the template for creating views in the
  Smart Client. The layout controls how many items can be displayed, how
  the items are sized, and how they share the space in the view.
keywords: Component integration
lang: en-US
title: Adding a view layout using the Configuration API
---

# Adding a view layout using the Configuration API

A view layout is the template for creating views in the Smart Client.
The layout controls how many items can be displayed, how the items are
sized, and how they share the space in the view.

The AddLayout sample demonstrates how to add a view layout using strongly typed Configuration API classes. 

![Smart Client setup, creating a new view](AddLayoutSC.png)

## How to use the sample

1. Build and run the IconToString tool that can be found in the
   `AddLayout` folder. When running it, you must supply two
   parameters:
   - `"path\to\AddLayout\IconToString\icon.png"`
   - `"path\to\AddLayout\IconToString\Layout.xml"`
2. Build the AddLayout sample.
3. Copy the newly created file
   (`AddLayout\IconToString\LayoutNEW.xml`)
   to the bin folder of the AddLayout sample
   (`AddLayout\bin\<Platform>\<Configuration>\`)
4. Run the AddLayout sample.
5. In XProtect Smart Client, go into setup mode and see that you have a
   new view layout named "Layout".

### Layout definition XML file

The layout is defined by specifying the layout in XML format. The
definition XML file (before adding an icon) looks like this:

~~~xml
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

- The pathname of an icon graphics file
- The pathname of the layout definition XML file

The tool will output a new XML file. The name of this new file will be
the name of the input XML file extended with \"NEW\". For example, the
input file `Layout.xml` results in a new file named `LayoutNEW.xml`.

### Removing layouts

This sample only demonstrates how to add a new layout.
You can use the Config API Client sample to remove layouts.

![Config API Client showing the layout added by the AddLayout sample](ConfigAPIClientLayout.png)

## The sample demonstrates

- Login default Windows credentials
- Usage of strongly typed Configuration API classes to add a view layout to the VMS

## Using

- VideoOS.Platform.ConfigurationItems
- VideoOS.Platform.ConfigurationItems.ManagementServer
- VideoOS.Platform.ConfigurationItems.LayoutFolder.AddLayout

## Environments

- .NET library MIP Environment

## Visual Studio C\# project

- [AddLayout.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
- [IconToString/IconToString.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','AddLayout/IconToString/IconToString.sln');)
