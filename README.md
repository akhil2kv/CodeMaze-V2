# ASP.NET Core Web API Project: Implementation Guide

Welcome to the **ASP.NET Core Web API Implementation Guide**! 🚀

In this comprehensive guide, we'll walk you through the key points and features of our project up to Chapter 14. Whether you're a beginner or an experienced developer, this README will help you understand the project, get started, and contribute effectively.

## Table of Contents

1. [Introduction](#introduction)
2. [Project Configuration](#project-configuration)
3. [Logging Service](#logging-service)
4. [Onion Architecture](#onion-architecture)
5. [Handling GET Requests](#handling-get-requests)
6. [Global Error Handling](#global-error-handling)
7. [Content Negotiation](#content-negotiation)
8. [Method Safety and Idempotency](#method-safety-and-idempotency)
9. [Creating Resources](#creating-resources)
10. [Working with DELETE Requests](#working-with-delete-requests)
11. [Working with PUT Requests](#working-with-put-requests)
12. [Working with PATCH Requests](#working-with-patch-requests)
13. [Validation](#validation)
14. [Asynchronous Code](#asynchronous-code)

## Introduction
A README is like a project's front door – it's the first thing users see. Let's make ours informative and engaging!

## Project Configuration
- Set up your environment for writing the README.
- All GitHub READMEs use Markdown – a lightweight syntax for styling text¹.
- Familiarize yourself with Markdown – it's as easy as pie!

## Logging Service
- Create a custom logging service using NLog.
- Register it in the Program class.
- Use it in controllers and services.

## Onion Architecture
- Understand the layered Onion Architecture.
- Create models, repositories, and services.
- Separate core business logic from infrastructure and presentation layers.

## Handling GET Requests
- Create controllers and routes for GET requests.
- Use DTOs and AutoMapper for mapping.
- Implement content negotiation and custom formatters.

## Global Error Handling
- Handle errors globally using middleware.
- Create an ErrorDetails class.
- Test error handling with Postman.

## Content Negotiation
- Support different media types (JSON, XML, CSV).
- Use formatters for serialization and deserialization.

## Method Safety and Idempotency
- Understand safe and idempotent methods.
- Choose the right HTTP methods for your operations.

## Creating Resources
- Handle POST requests.
- Validate request models.
- Create parent and child resources.

## Working with DELETE Requests
- Implement DELETE methods.
- Delete single and parent resources.

## Working with PUT Requests
- Update resources using PUT.
- Insert resources while updating.

## Working with PATCH Requests
- Apply partial updates using PATCH.
- Use JsonPatchDocument.

## Validation
- Validate request models.
- Use built-in and custom attributes.
- Improve project documentation.

## Asynchronous Code
- Understand asynchronous programming.
- Use async/await and Task types.
- Optimize performance with asynchronous methods.

Remember, a good README not only explains your project but also helps you stand out among other developers. Happy coding! 🌟

¹: [How to Write a Good README File for Your GitHub Project](https://www.freecodecamp.org/news/how-to-write-a-good-readme-file/)

Source: Conversation with Bing, 16/2/2024
(1) How to Write a Good README File for Your GitHub Project - freeCodeCamp.org. https://www.freecodecamp.org/news/how-to-write-a-good-readme-file/.
(2) How to write a perfect README for your GitHub project. https://dev.to/mfts/how-to-write-a-perfect-readme-for-your-github-project-59f2.
(3) Quickstart for writing on GitHub - GitHub Docs. https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/quickstart-for-writing-on-github.
