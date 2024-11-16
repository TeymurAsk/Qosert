# Qosert
Qosert is a web application for people to send different kinds of notifications to people by uploading xls or csv file. Qosert prioratizes delivery of every notification to users in time.

## Features
+ Users can upload of the list of contacts via .csv and .xls files
+ Users can create templates and configure list of recipients
+ The system is designed to handle tens of thousands of notifications simultaneousl
+ In the event of a server failure, the system is designed to ensure that every message reaches its intended recipient.

## Tech Stack
+ **Blazor**: The Frontend framework for handling user's input.
+ **PostgreSQL**: Relational database for user's contacts and notifications.
+ **Kafka**: Used as a message broker to support high load of notifications.
+ **Docker**: Manages Kafka and API instances of application for local deployment.
+ **Worker Services**: Background services built with .NET Core, responsible for processing and sending notifications.
+ **Google SMTP**: Configured for email notifications, allowing the system to send emails directly to recipients’ inboxes.

## Getting Started

### Clone the repository
Clone the repo to get access to the code and provide in you IDE.
### Usage
1. You will need to run docker-compose file on your machine. **(Install Docker beforehand, all used ports are mentioned in docker-compose)**
2. You might need run migrations of the Database in your IDE after cloning the code.
3. Go to the ENS_API/Services/EmailService.cs and find creatials for SMTP account **(please change them on your own)**.
4. There is the image of how your .xls or .csv might need to look for proper use.
5. ![image](https://github.com/user-attachments/assets/a6da922d-a341-4537-b4ad-5c31bf895fb6)
6. You're ready to **GO!**
 
