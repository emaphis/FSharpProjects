// Lesson 10  - Interfaces

// Define

type IFizzBuzz =
    abstract Calculate : int -> string


// Implement

type FizzBuzz(mapping) =

    let calculate n =
        mapping
        |> List.map (fun (v, s) ->
                if n% v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s <> "" then s else string n

    interface IFizzBuzz with
        member _.Calculate (input: int) = calculate input


let run =
    let fzzBzz = FizzBuzz([(3, "Fizz"); (5, "Buzz")]) :> IFizzBuzz
    [1..20]
    |> List.map fzzBzz.Calculate

// :>   -- Upcast
// :?>  -- Downcast

// As a Parameter

let doStuff (fizzBuzz:IFizzBuzz) =
    [1..20]
    |> List.map fizzBuzz.Calculate

let fb = FizzBuzz([(3, "Fizz"); (5, "Buzz")])
let buzz = doStuff fb
