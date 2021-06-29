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

The two samples, AddLayout and AddLayout_PS, demonstrates how to do that
using C\# and PowerShell scripts respectively.

![](AddLayoutSC.png)

*Screen capture showing a Smart Client setup, creating new view
dialogs.*

## How to use the samples (AddLayout and AddLayout_PS)

1.  Build and run the IconToString tool that can be found in the
    AddLayout_ps folder. When running it, you must supply two
    parameters:
    -   \"C:\\Program
        Files\\Milestone\\MIPSDK\\ComponentSamples\\AddLayout_PS\\icon.png\"
    -   \"C:\\Program
        Files\\Milestone\\MIPSDK\\ComponentSamples\\AddLayout_PS\\Layout.xml\"
2.  Copy the three files, `VideoOS.Platform.dll`, `VideoOS.Platform.Common.dll`, `VideoOS.IdentityServer.Common.dll`,  `Autofac.dll` and
    `VideoOS.Platform.SDK.dll` from
    `C:\Program Files\Milestone\MIPSDK\Bin` to
    `C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout_PS\`
3.  Run the PowerShell script
    `C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout_PS\AddLayout.ps1`
4.  In XProtect Smart Client, go into setup mode and see that you have a
    new layout named LayoutPS.
5.  Copy the newly created file
    (`C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout_PS\LayoutNEW.xml`)
    to the folder of the C\# sample
    (`C:\Program Files\Milestone\MIPSDK\ComponentSamples\AddLayout\`)
6.  Build and run the AddLayout C\# sample.
7.  In XProtect Smart Client, go into setup mode and see that you have a
    new layout named LayoutC.

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

-   The path and name of an icon graphics file
-   The path and name of the layout definition XML file

The tool will output a new XML file. The name of this new file will be
the name of the input XML file extended with \"NEW\". For example, the
input file Layout.xml results in a new file named LayoutNEW.xml.

### Samples

Both samples can work with modified input files rather than those
supplied, but because the samples have all parameters hard-coded, they
will not be practical. A tool should be developed based on either of the
two samples, but with added functionality. It should also be considered
that if you make a tool to add layouts, you will probably need to have a
tool that can remove layouts. It has not been the intention to develop a
tool, but to make a sample that can be used as inspiration and as a
starting point.

For initial testing you will be able to use the Config API Client sample
to remove layouts.

![](ConfigAPIClientLayout.png)

*Screen capture showing the Config API Client showing the layouts added
by the samples.*

## The sample demonstrates

-   Login default Windows credentials
-   Usage of strongly typed Configuration API classes to add a
    view-layout to the VMS

## Using

-   VideoOS.Platform.ConfigurationItems
-   VideoOS.Platform.ConfigurationItems.ManagementServer
-   VideoOS.Platform.ConfigurationItems.LayoutFolder.AddLayout

## Environments

-   .NET library MIP Environment
-   PowerShell

## Visual Studio C\# project

-   [AddLayout.csproj](javascript:openLink('..\\\\ComponentSamples\\\\AddLayout\\\\AddLayout.csproj');)
-   [IconToString.csproj](javascript:openLink('..\\\\ComponentSamples\\\\AddLayout\\\\AddLayout_PS\\\\IconToString\\\\IconToString.csproj');)
