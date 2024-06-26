---
description: The MediaRestriction sample show how to use the RestrictedMedia service
  for managing media restrictions for both live and playback.
keywords: Component integration
lang: en-US
title: Media restriction
---

# Media Restriction

A media restriction (also known as a video restriction) for a camera is simply a
time period when special permissions are required to retrieve video.

A live restriction prevents users without special permission to view live and to
play back recorded video recorded after the start time of the live restriction.

A playback restriction prevent user from playing back recorded video for a
given time period for all cameras included in the playback restriction.

The MediaRestriction sample shows how to use create live restrictions,
retrieve a list of live restrictions, end multiple live restrictions
and thereby turn them into a playback restriction.
Finally it also show how to retrieve existing playback restrictions,
how to update and finally delete a playback restriction.

## The sample demonstrates

- Use of RestrictedMedia service for managing live- and playback restrictions.

## Using

- VideoOS.Platform.SDK

## Environment

- .NET library MIP Environment

## Visual Studio C\# project

- [MediaRestriction.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
