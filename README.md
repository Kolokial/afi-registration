# AFI Registration API

Welcome to the AFI Registration API.

## Running the API

If you're running the API inside the DevContainer, then all dependencies should be installed and you can go ahead and run `dotnet run` from inside the `./AFIRegistrationApi/` directory.

Once the API is running, you can navigate to http://127.0.0.1:5047/swagger/index.html to use the SwaggerUI to make calls.

If you're not running the API inside the DevContainer, then you'll need the .NET Core SDK and SQLite installed.

## Running the tests

Either inside the DevContainer or outside, the tests can be run with a simple `dotnet test` command inside the `./AFIRegistrationAPITests/` directory.