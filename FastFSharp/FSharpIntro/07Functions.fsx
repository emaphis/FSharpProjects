// Intro to Functions - Defining functions


let myAdder a b =
    a + b

myAdder 3 4

let myAdder2 (a: float) (b: float) : float =
    if a < 1.0 then
        a + b
    else
        a

myAdder2 3.0 4.0

// function literal
let myAdder3 = fun a b -> a + b

myAdder3 3 4

let x =
    [1 .. 10]
    |> List.map (fun x -> x * 2)


// recursion

let rec myFunc x =
    if x < 10 then
        printfn $"{x}"
        myFunc (x + 1)
    else
        ()

myFunc 3


[<RequireQualifiedAccess>]
type Animal =
    | Chicken of int
    | Cow of string


let myAnimalFunction (anim: Animal) =
    match anim with
    | Animal.Chicken c -> printfn $"Chicken: {c}"
    | Animal.Cow c ->  printfn $"Cow: {c}"

let myChicken = Animal.Chicken 42

myAnimalFunction myChicken
