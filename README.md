**Implementation notes** 

The solution is structured in the following projects:
- Monarchs.Api - this is where the api is located
- Monarchs.Api.UnitTests - unit tests for api 
- Monarchs.ApiClientApp - console app that calls the api
- Monarchs.Common - this is where the data models, interfaces, view models and other useful common code is
- Monarchs.Infrastructure - this were all the services and database data is located, I used the json file provided as a database

I have used a basic JWT token for authentication against some users that are stored 
in the UserConstants file. For testing you can use either admin and admin_PW or normal and
normal_PW. The workflow is as follows, go to the login endpoint and use the provided 
credentials, you will get a token back that you then have to use in the subsequent
requests for all endpoints besides GetNumberOfMonarchs. If you use Swagger you will 
have to copy the token into the Authorize form that pops up when you press the 
Authorize button in the upper right corner and press the authorize button.
There are also different roles used for authentication (Administrator and Normal). 
In our case all of the endpoints accept both roles, since
we only retrieve data but for example maybe we would want only administrators to be 
able to add data to the database.

The api project has two controllers and the following endpoints:
- Login(UserLogin userLogin) belongs to the login controller and receives an UserLogin object
in the body of the request. Returns a token that is used to authenticate in order to be able
to call some of the other endpoints.
- GetNumberOfMonarchs belongs to the monarchs controller, you can call it without authentication
and authorization, returns the nr of monarchs that we have in the database.
- GetLongestRulingMonarch belongs to the monarchs controller, you have to be authenticated to call,
returns the name of the longest ruling monarch and the nr of years ruled
- GetLongestRulingHouse belongs to the monarchs controller, you have to be authenticated to call,
returns the name of the longest ruling house and the nr of years ruled
- GetMostCommonFirstName belongs to the monarchs controller, you have to be authenticated to call,
returns the most common first name.

For testing using the ApiClientApp console app you will have to make sure that in your IDE you configure to run both
projects (Monarchs.ApiClientApp and Monarchs.Api). 

The console app has the following options that you can type:
```
##################################################
Type in one of the following options to call api:
Login
GetNumberOfMonarchs
GetLongestRulingMonarch
GetLongestRulingHouse
GetMostCommonFirstName
Exit
##################################################
Typed values are case sensitive
For the following endpoints you have to login first:
GetLongestRulingMonarch, GetLongestRulingHouse, GetMostCommonFirstName
##################################################
```
As described above, the options map the endpoints and in order to get data from all besides GetNumberOfMonarchs
you need to be authenticated.


Future improvements:
- Extend logging
- Extend unit testing, currently we only test the monarchs controller
- Add integration tests, we could say that in this scenario the console app serves as an integration test but 
we should add integration tests nevertheless
- For the following endpoint GetLongestRulingMonarch, GetLongestRulingHouse, GetMostCommonFirstName I return the first value
but maybe we want to return more than one if there is a tie?
- Data for authentication is hardcoded, should be moved to some kind of identity server/service
- The database is currently the provided json, it should be changed to a more robust solution
- For the ApiClientApp the login option uses the admin user, a future improvement can be to make it possible to type the credentials.
