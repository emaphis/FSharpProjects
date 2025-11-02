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



// Lists

// Defining
let vowels = [ 'a'; 'e'; 'i'; 'o'; 'u' ]
let emptyList = []


// primative operators

// cons
let sometimes = 'y' :: vowels 
//val sometimes: char list = ['y'; 'a'; 'e'; 'i'; 'o'; 'u']

// applend '@'
let odds = [ 1; 3; 5; 7; 9 ]
let evens = [ 2; 4; 6; 8; 10 ]

let ints = odds @ evens
//val ints: int list = [1; 3; 5; 7; 9; 2; 4; 6; 8; 10]


// ranges

let x2 = [ 1 .. 10 ]
//val x2: int list = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]

let tens = [ 0 .. 10 .. 50 ]
//val tens: int list = [0; 10; 20; 30; 40; 50]

let countdown = [ 5L .. -1L .. 0L ]
//val countdown: int64 list = [5L; 4L; 3L; 2L; 1L; 0L]


// List comprehensions

// Simple list comprehensin
let numbersNear x =
    [
        yield x - 1
        yield x
        yield x + 1
    ]
//val numbersNear: x: int -> int list

let near3 = numbersNear 3
//val near3: int list = [2; 3; 4]

// More complex example
let x4 =
    [  let negate x = -x
       for i in 1 .. 10 do
            if i % 2 = 0 then
                yield negate i
            else 
                yield i ]
//val x4: int list = [1; -2; 3; -4; 5; -6; 7; -8; 9; -10]

//  using for loops within list comprehensions, you can simplify the code by
//  using -> insteadof do

/// Generates the first ten multiples of a number
let multiplesOfa x = [ for i in 1 .. 10 do yield x * i ]

/// Generates the first ten multiples of a number simplified
let multiplesOfb x = [ for i in 1 .. 10 -> x * i ]

let multiplesOf = multiplesOfb 3
//val multiplesOf: int list = [3; 6; 9; 12; 15; 18; 21; 24; 27; 30]


// Example 2-3. Using list comprehensions to compute primes

/// List comprehension for prime numbers
let primesUnder max =
    [
        for n in 1 .. max do
            let factorOfN =
                [
                    for i in 1 .. n do
                        if n % i = 0 then
                            yield i
                ]

            // n is prime if its only factors are 1 and n
            if List.length factorOfN = 2 then
                yield n
    ]
//val primesUnder: max: int -> int list

let primesUnder50 = primesUnder 50
//val primesUnder50: int list =
//   [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47]


//  List module functions

// Using List.partition
let isMultipleOf5 x = (x % 5 = 0)

let list5 = [ 1 .. 15 ]

let multOf5, nonMultOf5 =
    List.partition isMultipleOf5 list5

//val nonMultOf5: int list = [1; 2; 3; 4; 6; 7; 8; 9; 11; 12; 13; 14]
//val multOf5: int list = [5; 10; 15]


// Aggregate Operators

List.map // each new element of the list is the result of applying the passed function
//val it: (('a -> 'b) -> 'a list -> 'b list)

//  Example 2-4. Using List.map to square numbers in a list

let square' x = x * x

let squares = List.map square' [ 1 .. 10 ]
//val squares: int list = [1; 4; 9; 16; 25; 36; 49; 64; 81; 100]


List.reduce
//val it: (('a -> 'a -> 'a) -> 'a list -> 'a)

// Example 2-5. Comma separating a list of strings using List.reduce
let insertCommas (acc: string) item = acc + ", " + item

let text1 = List.reduce insertCommas  ["Jack"; "Jill"; "Jim"; "Joe"; "Jane"]
//al text1: string = "Jack, Jill, Jim, Joe, Jane"


List.fold
//val it: (('a -> 'b -> 'a) -> 'a -> 'b list -> 'a)

let addAccToListItem acc i = acc + i

let sum = List.fold addAccToListItem 0 [1; 2; 3]
//val sum: int = 6

// The accumulator doesn't have to be the same type as the list.

/// Count the numbers of vowels in a string
let countVowels (str: string) =
    let charList = List.ofSeq str

    let accFunct (As, Es, Is, Os, Us) letter =
        if   letter = 'a' then (As + 1, Es, Is, Os, Us)
        elif letter = 'e' then (As, Es + 1, Is, Os, Us)
        elif letter = 'i' then (As, Es, Is + 1, Os, Us)
        elif letter = 'o' then (As, Es, Is, Os + 1, Us)
        elif letter = 'u' then (As, Es, Is, Os, Us + 1)
        else                   (As, Es, Is, Os, Us)

    List.fold accFunct (0, 0, 0, 0, 0) charList

let counts =  countVowels "The quick brown fox jumps over the lazy dog"


// Folding right-to-left

List.reduceBack
//val it: (('a -> 'a -> 'a) -> 'a list -> 'a)

List.foldBack
//val it: (('a -> 'b -> 'b) -> 'a list -> 'b -> 'b)


List.iter
//val it: (('a -> unit) -> 'a list -> unit)

// Example 2-7. Using List.iter to print numbers in a list


let printNumber x = printfn "Printing %d" x

do List.iter printNumber [1..5]



// Options

//  Example 2-8. The option type storing if a string parses as an integer

open System

/// tries to parse an integer using the Int32.TryParse function.
/// If the parsing is successful, the function will return Some(re
/// sult); otherwise it will return None.
let isInteger (str : string) =
    let successful, result = Int32.TryParse(str)
    if successful then Some result
    else None

let nun = isInteger "This is not and integer"
//val nun: int option = Non
let num = isInteger "400"
// val num: int option = Some 400


// Using Option.get
let isLessThanZero x = (x < 0)

let containsNegativeNumbers intList =
    let filteredList = List.filter isLessThanZero intList
    if List.length filteredList > 0
    then Some filteredList
    else None

let negativeNumbers = containsNegativeNumbers [6; 20; -8; 45; -5]
//val negativeNumbers: int list option = Some [-8; -5]

let negativeNumbers2 = Option.get negativeNumbers
//val negativeNumbers2: int list = [-8; -5]



// Printf

do printf "Hello, "
do printfn "World!"

//Hello, World!
//val it: unit = ()

// format specifiers

let mountain = "K2"
let height = 8611
let units = 'm'

do printfn "%s is %d%c high" mountain height units
//K2 is 8611m high
//val it: unit = ()

// type inference will give you and error of the types don't match

//do printfn "An integer = %d" 1.23
//CoreTypes.fsx(336,30): error FS0001: The type 'float' is not compatible with any of the
//types byte,int16,int32,int64,sbyte,uint16,uint32,uint64,nativeint,unativeint, arising
//from the use of a printf-style format string

// types of this function are inferred from the format specifiers
let inferParams x y z =
    printfn "x = %f, y = %s, z = %b"
//val inferParams: x: 'a -> y: 'b -> z: 'c -> (float -> string -> bool -> unit)

// `sprintf` is used when you need the result of the formatting as a string
let location = "World"

let greeting = sprintf "Hello, %s" location
//val greeting: string = "Hello, World"
