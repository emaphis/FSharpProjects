dotnet new slnx
dotnet new console -lang F# -o src/Stack
dotnet slnx add src/Stack/Stack.fsproj
dotnet new nunit -lang F# -o tests/Stack.Tests
dotnet sln add tests/StackTests/StackTests.fsproj
cd tests/StackTests
dotnet add reference ../../src/Stack/Stack.fsproj
dotnet build
dotnet test
cd ../..
