---
description: This repo contains samples that demonstrate MIP SDK component integration.
keywords:
- MIP SDK
- Component integration
lang: en-US
title: Milestone Integration Platform Software Development Kit component integration samples
---

# Milestone Integration Platform Software Development Kit component integration samples

The Milestone Integration Platform (MIP) enables fast and flexible integration between
Milestone XProtect video management software (VMS), applications available from
[Milestone Marketplace](<https://www.milestonesys.com/community/marketplace/>),
and other third-party applications and devices.

The Milestone Integration Platform Software Development Kit (MIP SDK) offers a suite of integration options, including

- protocol integration
- component integration (stand-alone applications using MIP .NET libraries)
- plug-in integration (hosted by XProtect application environments)

This repo contains samples that demonstrate MIP SDK component integration.

The component integration samples cover these key areas of XProtect VMS functionality:

| Functionality                              | Description                                                                                                           |
| ------------------------------------------ | --------------------------------------------------------------------------------------------------------------------- |
| Accessing configuration and status         | How to read and update configuration as well as monitoring status of the VMS.                                         |
| Video Processing Service                   | Demonstrates the VPS Toolkit that allows you to forward video from an XProtect camera device to a GStreamer pipeline. |
| Access and show video, audio, and metadata | Various approaches to accessing and utilizing video, audio, and metadata.                                             |
| Control and data injection                 | How to control and push data to the VMS.                                                                              |
| Logon & environment choices                | Various logon scenarios.                                                                                              |

A Visual Studio solution file in the `src` folder includes a Visual Studio project for each sample.

## Prerequisites

Please refer to the [MIP SDK Getting Started Guide](<https://content.milestonesys.com/l/299bb22321041592/>)
for information about how to set up a development environment for Milestone XProtect integrations.

## Documentation and support

Ask questions and find answers to common questions at the
[Milestone Developer Forum](<https://developer.milestonesys.com/>).

Browse overview and reference documentation at
[MIP SDK Documentation](<https://doc.developer.milestonesys.com>).

Get access to free eLearning at the
[Milestone Learning Portals](<https://www.milestonesys.com/solutions/services/learning-and-performance/>).

Watch tutorials about how to set up and use Milestone products at
[Milestone Video Tutorials](<https://www.milestonesys.com/support/self-service-and-support/video-tutorials/>).

## Contributions

We do not currently accept contributions through pull request.

In case you want to comment on or contribute to the samples, please reach out through
the [Milestone Developer Forum](<https://developer.milestonesys.com/>).

## License

MIP SDK sample code is is licensed under the [MIT license](<LICENSE.md>).
