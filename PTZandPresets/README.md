---
description: The PTZ and Presets sample enables the operator to do PTZ
  control while showing live video for a selected camera. The camera is
  selected using the ItemPickerForm which will show only the available
  PTZ cameras on the VMS.
keywords: Component integration
lang: en-US
title: PTZ and Presets
---

# PTZ and Presets

The PTZ and Presets sample enables the operator to do PTZ control while
showing live video for a selected camera. The camera is selected using
the ItemPickerForm which will show only the available PTZ cameras on the
VMS.

The detected presets are listed in a drop-down list. When you select a
preset in the combo box, the camera will be instructed to go to that
preset. When you press any of the arrow keys, the camera will be
instructed to move up, down, left, or right.

![PTZ and Presets](ptzandpresets.jpg)

## The sample demonstrates

- Initialization of MIP .NET Library, when video display is required
- Video display -- live
- PTZ control
- Enumerate and go to presets

## Using

- VideoOS.Platform.ClientControl
- VideoOS.Platform.UI.ImageViewerControl
- VideoOS.Platform.Messaging

## Environment

- MIP .NET library

## Visual Studio C\# project

- [PTZandPresets.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
