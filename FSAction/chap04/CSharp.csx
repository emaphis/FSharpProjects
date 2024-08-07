using System;

public void DescribeAge(int age)
{
    string ageDescription = null;
    var greeting = "Hello";
    if (age < 18)
        ageDescription = "Child"
    else if (age < 65)
        greeting = "Adult"

    Console.WrtieLine($"{greeting}! You are '{ageDescription}'.");
}

DescribeAge(45);