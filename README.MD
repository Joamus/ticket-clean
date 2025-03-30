# TicketSharp #

A basic ticketing system API (demo project)

## Design decisions ##

The architecture is inspired by Clean Architecture with the project being separated into the following layers:

* Presentation: The API endpoints are registered here
* Application: Has all the handlers for API endpoint (using Request-Endpoint-Response pattern)
* Domain: Handling of ticketing/support ogic
* Infrastructure: Used for communicating with external systems, e.g the database, potential mailproviders, etc.

## Technologies ##

* Minimal API: Used for the Rest API - did consider using FastAPI, maybe next time.
* Entity framework: For interacting with the SQLite database
* Microsoft's Identity package: mostly used out-of-the-box
* FluentResults: Most API operations are results-based, instead of handled by exceptions
* FluentValidation: Validation used for writing/updating entities, etc.
.. and more :-)

## Design descisions ##

* Used SQLite for simplicity
* REPL pattern seems to make the most sense for designing API endpoints, with having separate request/responses for each endpoint.
It does lead to more files, but it allows us to only query the exact data we need for different responses, which is both performant,
and more secure (less likely to over-share data from our domain model).
* Using mostly off-the-shelf identity: For this type of project, I decided to not focus too much on the authentication parts.

## Known trade-offs/YAGNI ##

* The User entity inherits from the IdentityUser - our domain model is essentially also our database entity/persistence model.
It seems better to just embrace EF fully, if you choose to use it, even if that can lead to a bit more "muddying" the separation between the domain and infrastructure layer.
As with so many choices in software development, it's a trade-off of pragmatism and purity.

## Could have done ##

* Made the ticket multi-tenant: Would be interesting, but ultimately not what I wanted to learn from this project - I was more interested in learning the technologies, frameworks, and design patterns which
make the most sense in .NET projects.
* Made a working emailer, and not a mock-implementation
