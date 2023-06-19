---
description: This sample shows how the BookmarkService is used for
  creating and retrieving bookmarks.
keywords: Component integration
lang: en-US
title: Bookmark Creator
---

# Bookmark Creator

This sample shows how the BookmarkService is used for creating and
retrieving bookmarks.

The sample only works with XProtect Corporate, Expert, and
Professional+.

This sample is similar to the Bookmark sample under protocols, except
this sample is using the .NET Library for login and camera selection.

![](BookmarkCreator1.png)

After the login is successful, it will automatically perform these
bookmark operations:

- Create first bookmark
- Create a second bookmark
- Looking for bookmarks using BookmarkSearchTime
- Looking for bookmarks using bookmarkListsFromBookmark
- Looking for the first bookmarks using BookmarkGet
- Deleting 2 newly created bookmarks

## MIP Environment - Component

## The sample demonstrates

- Use of BookmarkService for performing bookmark create, get and
  delete

## Using

- VideoOS.Platform.Data.BookmarkService
- VideoOS.Platform.Data.Bookmark

## Environment

- MIP .NET library

## Visual Studio C\# project

- [BookmarkCreator.csproj](javascript:clone('https://github.com/milestonesys/mipsdk-samples-component','src/ComponentSamples.sln');)
