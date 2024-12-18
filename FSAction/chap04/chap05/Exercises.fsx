// Chapter 05 - Shaping data

// Exercise 5.1

// 1. Create a function, buildPerson, that takes in three
//    individual values as input arguments: forename,
//    surname, and age. As an output, return just the
//    forename and surname elements as a tuple.

let buildPerson1 forename surname age =
    forename, surname

// val buildPerson: forename: 'a -> surname: 'b -> age: 'c -> 'a * 'b


// 2. Now, ensure that all the inputs are annotated as
//    strings or ints so that the signature is no longer
//    generalized. What signature does the function
//    have?

let buildPerson2 (forename: string) (surname: string) (age: int) =
    forename, surname

// val buildPerson2:
//      forename: string -> surname: string -> age: int -> string * string   


// 3. Use a wildcard on the unused age argument.

let buildPerson3 forename surname _ =
    forename, surname


// 4. Now remove the wildcard again and return a two
//    part nested tuple. The first part should contain the
//    forename/surname. The second part should
//    contain the age and a string that is either a “child”
//    if the age is less than 18 or “adult” otherwise.

let buildPerson4 forename surname age =
    (forename, surname), age

// val buildPerson4: forename: 'a -> surname: 'b -> age: 'c -> ('a * 'b) * 'c


// 5. Call this function within your script file and
//    experiment with assigning the result to a single
//    value or a deconstructed tuple.

let person1 = buildPerson4 "isaac" "abraham" 42

let name1, _ = buildPerson4 "isaac" "abraham" 42
// val name1: string * string = ("isaac", "abraham")


// EXERCISE 5.2

// Repeat the steps in exercise 5.1, but instead of returning
// data as tuples, create a record to return the data.ty

type Person1 =
    {
        Forename : string
        Surname : string
        Age : int
    }


// 1. Create a function, buildPerson, that takes in three
//    individual values as input arguments: forename,
//    surname, and age. As an output, return just the
//    forename and surname elements as a tuple.

let buildPerson5 forename surname age =
    {
        Forename = forename
        Surname = surname
        Age = age
    }

// val buildPerson: forename: 'a -> surname: 'b -> age: 'c -> 'a * 'b


// 2. Now, ensure that all the inputs are annotated as
//    strings or ints so that the signature is no longer
//    generalized. What signature does the function
//    have?

let buildPerson6 (forename: string) (surname: string) (age: int) =
    {
        Forename = forename
        Surname = surname
        Age = age
    }

// val buildPerson2:
//      forename: string -> surname: string -> age: int -> string * string   


// 3. Use a wildcard on the unused age argument.

let buildPerson7 forename surname _ =
    {
        Forename = forename
        Surname = surname
        Age = 0
    }


// 4. Now remove the wildcard again and return a two
//    part nested tuple. The first part should contain the
//    forename/surname. The second part should
//    contain the age and a string that is either a “child”
//    if the age is less than 18 or “adult” otherwise.

let buildPerson8 forename surname age =
    (forename, surname), age

// val buildPerson4: forename: 'a -> surname: 'b -> age: 'c -> ('a * 'b) * 'c


// 5. Call this function within your script file and
//    experiment with assigning the result to a single
//    value or a deconstructed tuple.

let person2 = buildPerson3 "isaac" "abraham" 42

let name2, _ = buildPerson3 "isaac" "abraham" 42
// val name1: string * string = ("isaac", "abraham")
