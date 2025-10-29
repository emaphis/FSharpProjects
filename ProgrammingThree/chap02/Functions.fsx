// Functions

let square x = x * x

let sqr1 = square 4 
//val sqr1: int = 16

let addOne x = x + 1

let add1 = addOne 5
//val add1: int = 6

let add x y = x + y
// val add: x: int -> y: int -> int

let add2 = add 1 2
//val add2: int = 3


// Type Inference

//let add3 = add 1.0 2.0 
//Functions.fsx(22,16): error FS0001: This expression was expected to have type
//    'int'
//but here has type
//    'float'

// create a new scope
let add3 =
    let add x y = x + y
    add 1.0 2.0
//val add3: float = 3.0

let add' (x: float) y = x + y
//val add': x: float -> y: float -> float

let add4 = add' 1.0 2.0
//al add': x: float -> y: float -> float


// Generic Functions

let ident x = x
//val ident: x: 'a -> 'a

let str1 = ident "a string"
//val str1: string = "a string"

let long1 = ident 1234L
//val str1: string = "a string"

let ident' (x: 'a) = x
// val ident': x: 'a -> 'a


// Scope

// module scope
let moduleValue = 10
let functionA x =
    x + moduleValue

let funA1 = functionA 5
//val funA1: int = 15

// Error case
let functionB x =
    let functionValue = 20
    x + functionValue

//functionValue // 
//  error FS0039: The value or constructor 'functionValue' is not defined. 

// Nested functions
let moduleValue' = 1

let f fParam =
    let g gParam = fParam + gParam + moduleValue'

    let a = g 1
    let b = g 2
    a + b

//val moduleValue': int = 1
//val f: fParam: int -> int

let int5 = f 5
//val int5: int = 15

// several 'x' value definitions
open System.Numerics

// Convert bytes to gigabytes
let bytesToGB x =
    let x = x / 1024I  // B to KB
    let x = x / 1024I  // KB to MB
    let x = x / 1024I  // MB to GB
    x

let hardDriveSize = bytesToGB  268435456000I
//val hardDriveSize: BigInteger = 250


// Control Flow

// If statements
let printGreeting shouldGreet greeting =
    if shouldGreet then
        printfn "%s" greeting

do printGreeting true "Hello!"
//Hello!

do printGreeting false "Hello again!"
//val it: unit = ()

// if expression
let isEven x =
    let result =
        if x % 2 = 0 then
            "Yes it is"
        else
            "No it is not"
    result

let even1 = isEven 5
//val even1: string = "No it is not"

// more complicated branching

let isWeekend day =
    if day = "Sunday" then
        true
    else
        if day = "Saturday" then
            true
        else
            false

// clean up using the 'elif' keyword
let isWeekday day =
    if   day = "Monday"    then true
    elif day = "Tuesday"   then true
    elif day = "Wednesday" then true
    elif day = "Thursday"  then true
    elif day = "Friday"    then true
    else false
