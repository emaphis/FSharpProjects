// Lesson 5 - recap of lesson 4 

// List and ResizeArray

// List.map : (('a -> 'b) -> 'a list -> 'b list)

// List.filter : (('a -> bool) -> 'a list -> 'a list)

// List.reduce : (('a -> 'a -> 'a) -> 'a list -> 'a)

// List.fold : (('a -> 'b -> 'a) -> 'a -> 'b list -> 'a)

// List.iter : (('a -> unit) -> 'a list -> unit)


// Comosition  - FizzBuzz

let FizzBuzz input =
    [(3, "Fizz"); (5, "Buzz")]
    |> List.map (fun (div, msg) ->
                            if input % div = 0 then msg else "")
    |> List.reduce (fun acc item -> acc + item)
    |> fun str -> if str = "" then string input else str


let fizz =
    [1..20]
    |> List.map (fun str ->  FizzBuzz str)








