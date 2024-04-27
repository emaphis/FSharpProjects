// Lecture 4 - recap Option, Result, map bind.

// Option type instead of null

let isNone: int option  = None

let isValue = Some 42

let val1 =
    match isValue with
    | Some value -> $"Value is {value}"
    | None -> "Has no value"

// Option value

// Option.ofObj Option.ofNullable
// Option.toObj  Option.toNullable


// Function Compostion with Effects


let tryDivide x y =
    if x = 0 then None
    else Some (x / y)


// Option

// declaration:  Optionzzz<'T> ot 'T option

// Option.map  (('a -> 'b) -> 'a option -> 'b option)
// Option.bind (('a -> 'b option) -> 'a option -> 'b option)


// Result

// Result.map  (('a -> 'b) -> Result<'a,'c> -> Result<'b,'c>)
// Result.bind (('a -> Result<'b,'c>) -> Result<'a,'c> -> Result<'b,'c>)
// Option
