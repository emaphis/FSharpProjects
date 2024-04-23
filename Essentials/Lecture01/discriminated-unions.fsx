// Lesson 01 - Discriminated unions

type Boolean1 = True | False | Unknown

let iamTrue = True

let iamFalse = False

false = (iamTrue = iamFalse)


type Option1<'T> =
    | Some1 of 'T
    | None1

let someNum = Some1 5
let noNum = None1

// deconstruct with match

let aString =
    match iamFalse with
    | True -> "I am true"
    | False -> "I am false"
    | Unknown -> "I don't know"
