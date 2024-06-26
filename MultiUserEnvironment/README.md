---
description: When the same set of XProtect servers needs to be accessed
  by multiple different users through the same application, a
  UserContext class can be utilized for separating each user\'s
  configuration and logon.
keywords: Component integration
lang: en-US
title: Multi-User Environment
---

# Multi-User Environment

When the same set of XProtect servers needs to be accessed by multiple
different users through the same application, a UserContext class can be
utilized for separating each user\'s configuration and logon.

This can be useful e.g. when you are making a server service that
provide some web service support for many users.

Each user can login and have a different view of the configuration,
depending on access rights, while still maintaining login credentials
and token separately.

For information about login, please refer to <a href="https://doc.developer.milestonesys.com/html/index.html?base=gettingstarted/intro_environments_login.html&tree=tree_4.html" target="_top">Introduction to MIP Environments and Login</a>

![](MultiUserEnvironment1.png)

In this application you type the XProtect server address just once, and afterwards you can login as two different users to the defined server.

Login can be performed as an Active Directory (AD) user or basic user. All logins can be done through OAuth or legacy login functionality.

Below the logon buttons, you can then select a camera and view video.

Next to each ImageViewerControl is a listbox containing the cameras each
user has access to. In this screen shot, the right hand user has access
to 5 cameras while the left hand user only has access to 3 cameras.


## The sample demonstrates

- How a single instance of the .NET Library can hold multiple users\' configuration at one time
- Login with AD and basic users with OAuth tokens or legacy functionality

## Using

- VideoOS.Platform.SDK.MultiUserEnvironment
- VideoOS.Platform.Live.JPEGLiveSource

## Environment

- .NET library MIP Environment

## Visual Studio C\# project

- [MultiUserEnvironment.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
