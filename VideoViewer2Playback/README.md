---
description: The VideoViewer2Playback sample can show live and recorded
  video for two selected cameras. After connecting to a server, you
  select the two cameras in the ItemPickerWpfWindow.
keywords: Component integration
lang: en-US
title: Video Viewer - Individual Playback
---

# Video Viewer - Individual Playback

The VideoViewer2Playback sample can show live and recorded video for two
selected cameras. After connecting to a server, you select the two
cameras in the ItemPickerWpfWindow.

This sample is similar to the VideoViewer sample, but can control
multiple cameras with respect to mode (live or playback) and time stamp.
It is also possible to select the same camera twice and display both
live and playback video from that camera.

The sample shows one camera for each playback control, but it is
possible to have multiple cameras and audio sources controlled by the
same playback control.

![Video Viewer with 2 playbacks](VideoViewer2playback.jpg)

## The sample demonstrates

- Connection and login
- Initialization of MIP .NET Library, when video display is required
- Video display -- live and playback
- Video display -- individual control of live and playback
- Digital zoom
- How to use the ItemPickerWpfWindow to select cameras
- Message system to control playback operations

## Using

- VideoOS.Platform.ClientControl
- VideoOS.Platform.UI.ItemPickerWpfWindow
- VideoOS.Platform.Client.ImageViewerWpfControl
- VideoOS.Platform.Messaging

## Environment

- MIP .NET library

## Visual Studio C\# project

- [VideoViewer2playback.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
