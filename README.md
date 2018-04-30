# Fii Prezent

This project was made for the [Fii Practic](https://fiipractic.asii.ro/) training.

## Demo
[https://fiiprezent.azurewebsites.net/](https://fiiprezent.azurewebsites.net/)
## Instalation

Add the following environment variables to your project:

> Facebook authentication

    AUTHENTICATION--FACEBOOK--APPID
    AUTHENTICATION--FACEBOOK--APPSECRET

> Connection string

    CONNECTIONSTRINGS--DEFAULTCONNECTIONSTRING

or you can add a secrets.json file to your project

    {
      "Authentication": {
        "Facebook": {
          "AppId": "...",
          "AppSecret": "..."
        }
      },
      "ConnectionStrings": {
        "DefaultConnectionString": "..."
      }
    }

## Features

 - Facebook authentication
 - Real time updates
 - Continuous delivery
 - Easy conversion between ViewModel and Entity
 - Unit testing
 - Nice design

## Libs
### Front End
 - [Bulma](https://bulma.io/)
 - [jQuery](http://jquery.com/)
 - [jQuery Validation](https://jqueryvalidation.org/)
 - [SignalR](http://signalr.net/)
 ### Back End
 
 - [AutoMapper](https://automapper.org/)
 - [SignalR](http://signalr.net/)
 - [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore/)
 - [xUnit](https://xunit.github.io/)
 - [Moq](https://github.com/Moq/)
 - [Shouldly](https://github.com/shouldly/shouldly)

## Screenshots
TODO


