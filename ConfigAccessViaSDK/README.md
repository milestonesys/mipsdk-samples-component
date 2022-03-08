---
description: The ConfigAccessViaSDK sample can connect to an XProtect
  server, get the entire configuration, and dump it into a listbox. For
  each item it finds, it will ask for the children belonging to that
  item.
keywords: Component integration
lang: en-US
title: Configuration Access
---

# Configuration Access

The ConfigAccessViaSDK sample can connect to an XProtect server, get the
entire configuration, and dump it into a listbox. For each item it
finds, it will ask for the children belonging to that item.

![Configuration Access](config_access.jpg)

## The sample demonstrates

-   Initialization of MIP .NET Library, when only configuration is
    required
-   Access to Milestone-owned configuration, for example, servers and
    cameras groups
-   A simple drill down of all Items via the Item.GetChildren() method.
-   Launching the ItemPicker dialog to select one or more cameras
    from the loaded configuration.

## Using

-   VideoOS.Platform.Configuration
-   VideoOS.Platform.UI.ItemPickerUserControl

## Environment

-   MIP .NET library

## Visual Studio C\# project

-   [ConfigAccessViaSDK.csproj](javascript:openLink('..\\\\ComponentSamples\\\\ConfigAccessViaSDK\\\\ConfigAccessViaSDK.csproj');)

## Special notes

This sample only needs access to configuration and the simple
ItemPickerForm, and no need to display live video, then only these 2
files are required for deployment:

-   VideoOS.Platform.dll
-   VideoOS.Platform.SDK.dll

![ItemPickerUserControl](itempickerusercontrol.jpg)
