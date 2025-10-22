using System;

// Working with statements.

static void DescribeAge(int age)
{
    string ageDescription = null;

    var greeting = "Hello";

    if (age < 18)
        ageDescription = "Child";
    else if (age < 65)
        ageDescription = "Adult";
    else
        ageDescription = "DAP";

    Console.WriteLine($"{greeting}! You are '{ageDescription}'.");
}

DescribeAge(45);

// Run with:  dotnet-script CSharp.csx
