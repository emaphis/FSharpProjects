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

let dinner = "green eggs", "ham"
//val dinner: string * string = ("green eggs", "ham")

let dinner' = ("green eggs",  "ham")
//val dinner': string * string = ("green eggs", "ham")

let zeros = (0, 0L, 0I, 0.0)
//al zeros: int * int64 * System.Numerics.BigInteger * float = (0, 0L, 0, 0.0)

let nested'6 = (1, (2.0, 3M), (4L, "5"))
//val nested'6: int * (float * decimal) * (int64 * string) =(1, (2.0, 3M), (4L, "5"))

// Extract values using 'fst' 'snd'
let nameTuple = ("John", "Smith")
//val nameTuple: string * string = ("John", "Smith")

let first = fst nameTuple
//val first: string = "John"

let last = snd nameTuple
//val last: string = "Smith"

// decomposition with pattern matching
let snacks =  ("Soda", "Cookies", "Candy")

let x, y, z = snacks
//val z: string = "Candy"
//val y: string = "Cookies"
//val x: string = "Soda"

y, z
//val it: string * string = ("Cookies", "Candy")

// passing tupels a parameter

let add x y = x + y
//val add: x: int -> y: int -> int

let tupleAdd (x, y) = add x y
//val tupleAdd: x: int * y: int -> int

let sum1 = tupleAdd(3, 4)
//val sum1: int = 7



