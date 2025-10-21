// Exercise 3.5

let calculateGroup age =
    //if age < "test" then "Child"  // bad expression
    if age < 18 then "Child"
    elif age < 65 then "Adult"
    else "Pensioner"

let sayHello someValue =
    let group =
        if someValue < 10.0 then calculateGroup 15
        else calculateGroup 35
    "Hello " + group

let result = sayHello 10.5
