// 10 - Object Programming

// Class Types

type IFizzBuzz =
    abstract member Calculate : int -> string

type FizzBuzz(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    interface IFizzBuzz with
        member _.Calculate(value) = calculate value


let fizzBuzz = FizzBuzz([(3, "Fizz");(5, "Buzz")]) :> IFizzBuzz //Upcast 
let fifeteen = fizzBuzz.Calculate(15)

let doFizzBuzz mapping range =
    let fizzBuzz = FizzBuzz(mapping) :> IFizzBuzz //Upcast 
    range
    |> List.map  fizzBuzz.Calculate

let output = doFizzBuzz [(3, "Fizz");(5, "Buzz")] [1..15]
