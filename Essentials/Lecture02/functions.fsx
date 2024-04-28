// Lesson 2 - Functions

// Tupled functions
// infering types
// default for '+' is int
// val addxx: x: int * y: int -> int

let add00 (x: int, y: int): int = x + y  // takes a tuple, not a C# method call.

let add01 (x, y) = x + y  // type inference.

let add03 = fun (x, y) -> x + y  // tupled lambda

let assert01 = add00 (3, 4) = 7
let assert02 = add01 (3, 4) = 7
let assert03 = add03 (3, 4) = 7


// Currying and partial application
// val addxx: x: int -> y: int -> int

let add04 (x: int) (y: int) : int = x + y

let add05 x y = x + y  // infered

let add06 = fun x y -> x + y  // infered lambda

// Return a new function with partial application
// val add07: (int -> int)
let add07 = add06 3   // partial evaluation

let assert04 = add04 3 4 = 7
let assert05 = add05 3 4 = 7
let assert06 = add06 3 4 = 7
let assert07 = add07 4 = 7
