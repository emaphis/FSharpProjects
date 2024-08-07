// Chapter 4 exercises


// Exercise 4.2

// 1. Identify the scope you wish to move into a
//    function of its own.
// 2. Cut that code out and paste it above the function
//    that it’s currently in, taking care to correct the
//    indentation so that it’s within the same scope as
//    the function you extracted it from.
// 3. Identify any required symbols and add them as
//    inputs to the function. For example, in listing 4.3,
//    this is the age symbol. Don’t worry about type
//    annotations; the compiler can normally infer them
//    for you.
// 4. In place of the code in the original function,
//    replace it with a call to the newly created function

let getAgeDescription age =
    if age < 18 then "Child"
    elif age < 65 then "Adult"
    else "DAP"

let describeAge age =
    let ageDescription = getAgeDescription age
    let greeting = "Hello"
    printfn $"{greeting}! You are a '{ageDescription}'"

describeAge 15
describeAge 45
describeAge 70


// Exercise 4.3

// 1. Instead of using a string to represent how far
//    you’ve driven, use an integer.
// 2. Instead of far, check whether the distance is more
//    than 50.
// 3. Instead of medium, check whether the distance is
//    more than 25.
// 4. If the distance is > 0, reduce gas by 1.
// 5. If the distance is 0, make no change to the gas
//    consumption. In other words, return the same
//    state that was provided.


let drive gas distance =
    if distance > 50 then  gas / 2.0
    elif distance > 25 then gas - 10.0
    elif distance > 0 then gas - 1.0
    else gas

// val drive: gas: float -> distance: int -> float

let st00 = 100.0
let st01 = drive st00 60
let st02 = drive st01 30
let st03 = drive st02 5
