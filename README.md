# Qosert
Qosert is a web application for people to send different kinds of notifications to people by uploading xls or csv file. Qosert prioratizes delivery of every notification to users in time.

## Features
+ Users can upload of the list of contacts via .csv and .xls files
+ Users can create templates and configure list of recipients
+ The system is designed to handle tens of thousands of notifications simultaneousl
+ In the event of a server failure, the system is designed to ensure that every message reaches its intended recipient.

## Tech Stack
+ Blazor: The Frontend framework for handling user's input.
+ PostgreSQL: Relational database for user's contacts and notifications.
+ Kafka: Used as a message broker to support high load of notifications.
+ Docker: Manages Kafka and API instances of application for local deployment.
+ Worker Services: Background services built with .NET Core, responsible for processing and sending notifications.
+ Google SMTP: Configured for email notifications, allowing the system to send emails directly to recipients’ inboxes.
