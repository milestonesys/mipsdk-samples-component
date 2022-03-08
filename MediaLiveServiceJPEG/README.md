---
description: The Media Live Service shows how to use the Media
  Toolkit features through a C++ service type application.
keywords: Component integration
lang: en-US
title: MediaLiveService C++
---

# MediaLiveService C++

The Media Live Service shows how to use the Media Toolkit features
through a C++ service type application.

In this sample we will receive frames in JPEG format and store on disk
as C:\\Data\\MyExportedImage\#.jpg

You need to modify the source code with a correct GUID for the camera
id, and a set of working credentials and server address.

The `vmsServer` and `vmsServerPort` is usually something the user types in,
while the `vmsRecorderUri` is found in the configuration for the specific
camera in the response from the `GetConfiguration()` method on the
ServerCommandService SOAP service.

~~~ cpp
// GUID for the camera: CHANGE TO YOUR GUID
utf8_string_t cameraGuid = "d3fba69e-fa92-4347-acd2-e216dcf35682";

// NOTE: YOU NEED TO SET THESE NEXT 6 LINES!
utf8_string_t vmsServer           = "localhost";             // The address you login to
short vmsServerPort               = 0;                       // ... and port - if 0, default will be used (80 for http, or 443 for https)
utf8_string_t vmsRecorderUri      = "http://localhost:7563"; // The recording server where the camera is. Must be http:// or https://, depending on recording server configuration
utf8_string_t username            = "";                      // Change to username (without domain)
utf8_string_t password            = "";                      // Change to password
AuthenticationMethod_t authMethod = WindowsAuthentication;   // Choose between BasicAuthentication or WindowsAuthentication
// Note: If BasicAuthentication is selected, HTTPS will be used, the port should (most likely) be 443 or 0.
~~~

The method used for authentication can be changed to use Basic by changing the line:

~~~ cpp
AuthenticationMethod_t authMethod = WindowsAuthentication;   // Choose between BasicAuthentication or WindowsAuthentication
~~~

The application will perform these functions:

- Load the ServerCommandServiceClient, initialize it and perform a login
- Construct an XML for connecting the toolkit to a recording server
- Create a toolkit provider instance
- Create a specific toolkit instance
- Connect the toolkit to the recording server
- Wait for a maximum of 10 seconds for a live image
- Store received live image as a jpeg
- Close stream
- Close toolkit instance
- Close provider

If any errors occur during the execution, an error message is displayed
on the console output.

## The sample demonstrates

- How to effectively retrieve JPEGs from XProtect recording servers
- Handling of renewing login token

## Using

- ServerCommandServiceClient - a small utility for login
- Media Toolkit for live reception of frames from a recording server

## Environment

- None

## Visual Studio C++ project

- [MediaLiveServiceJPEG.vcxproj](javascript:openLink('..\\\\ComponentSamples\\\\MediaLiveServiceJPEG\\\\MediaLiveServiceJPEG.vcxproj');)

 *Note* - Windows SDK 10 is required for this project to build. Install it through Visual studios "Get Tools And Features", choose "Desktop Development with C++" 

 *Note* - If you want to run this sample in debug configuration, you have to change the referenced NuGet package. To do this, select "Manage NuGet Packages" from the project right-click menu in Visual Studio, uninstall the MilestoneSystems.VideoOS.Platform.SDK-CPP package and install the MilestoneSystems.VideoOS.Platform.SDK-CPP.Debug package instead. 