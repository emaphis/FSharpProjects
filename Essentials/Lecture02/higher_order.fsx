// Lesson 2  - Higher Order

let add x y = x + y

let mult x y = x * y

// val calculate: f: ('a -> 'b -> 'c) -> x: 'a -> y: 'b -> 'c
let calculate f x y = f x y

let result1 = calculate add 5 4  // 9

let result2 = calculate mult 5 4  // 20

let result3 = calculate (fun x y -> x - y) 5 4  // 1


// Unit - ()

open System

let now = DateTime.Now

now.ToString()

// nowF :: unit -> DateTime
let nowF () = DateTime.Now

let now1 = nowF()

// write :: message: 'a -> unit
let write message =
    printfn $"{message}"

write "A Message."

