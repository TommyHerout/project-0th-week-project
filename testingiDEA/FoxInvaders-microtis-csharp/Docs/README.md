# Tribes

## Description

A strategy game based on the logic of Travian
- https://en.wikipedia.org/wiki/Travian
- http://travian.com/

Two teams work on this project: a Java back end team and an Android client team.


The main goals are to:
- create a working application where several developers collaborate in Scrum methodology
- clearly separate back end and mobile client functionality
- implement proper APIs
- write proper game logic


## Main features

- registration and authentication
- player overview screen (= town page)
- tribe screen
- time based buildings & troops generation and upgrade
- resources
- search & attack on other players
- leaderboard


## Technology

- Java backend
	- Spring
	- relational databases
	- HTTP request handling with REST APIs
	- endpoint testing
- Android client
	- HTTP request initiating to REST APIs
  - App Components
  - Notifications
  - Component testing
  - Data caching
- Common
	- multi-environments
	- deployment with CircleCI
	- unit testing

## Further specification
- [Api spec](api-spec.md)
- [Models](models.md)
- [Rules](rules.md)

## User Stories
- [Backend](stories-backend.md)
- [Frontend](stories-frontend.md)
