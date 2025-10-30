// Core Types

// unit
let unit1 = ()
//val unit1: unit = ()

// concrete
let int1 = 42
//val int1: int = 42
let float1 = 3.14
//val int1: int = 42

// generic  'a

// function type
let fun1 = fun x -> x + 1
//val fun1: x: int -> int

// tuple type
let tpl1 = ("eggs", "ham")
//val tpl1: string * string = ("eggs", "ham")

// list type
let lst1 = [ 1; 2; 3 ]
//val lst1: int list = [1; 2; 3]


// option tupe
let opt1 = Some(3)
//val opt1: int option = Some 3



// Unit
// A value signafiying noting

let xu = ()
//val xu: unit = ()

()
//val it: unit = ()

// A functions return something, if a function doesn't conceptially returnt something 
// like 'printfn' return a unit '()'

let square x = x * x

ignore (square 4)
//val it: unit = ()


// Tuples
// ordered collection of data, to easlily clump together data as a unit.





