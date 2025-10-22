// Listing 4.2 Working with expressions in a statement
// oriented language

using System;

// An expression
static string GetDescription(int age)
{
    if (age < 18) return "Child!";
    else if (age < 65) return "Adult!";
    else return "OAP!";
}

void DescribeAge(int age)
{
    var ageDescription = GetDescription(age);

    var greeting = "Hello";
    Console.WriteLine($"{greeting}! You are a '{ageDescription}'.");
}

DescribeAge(45);

// Run with:  dotnet-script CSharp2.csx