// Lesson 11 - Recap Object Programming

type FizzBuzz() =
    let calculate n =
        [(3, "Fizz"); (5, "Buzz")]
        |> List.map (fun (v, s) ->
                 if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    
    member _.Calculate(n:int) = calculate n


let fizzBuzz = FizzBuzz()

[1..20] |> List.map fizzBuzz.Calculate
