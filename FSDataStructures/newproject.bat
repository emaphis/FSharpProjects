dotnet new console -lang F# -o src/xxx
dotnet sln add src/xxx/xxx.fsproj
dotnet new xunit -lang F# -o tests/xxxTests
dotnet sln add tests/xxxTests/xxxTests.fsproj
cd tests/xxxTests
dotnet add reference ../../src/xxx/xxx.fsproj
dotnet add package Xunit
dotnet add package FsUnit.XUnit
dotnet add package Swensen.Unquote
dotnet build
dotnet test

