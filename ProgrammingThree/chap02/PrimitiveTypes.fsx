// Primitive Types

let x = 1
//val x: int = 1


// Numeric Primitives

let answerToEverything = 42UL
//val answerToEverything: uint64 = 42UL

let pi = 3.1415926M
//val pi: decimal = 3.1415926M

let avogadro = 6.022e-23
//val avogadro: float = 6.022e-23

// Primitives

let hex = 0xFCAF
//val hex: int = 64687

let oct = 0o7771L
//val oct: int64 = 4089L

let bin = 0b00101010y
//val bin: sbyte = 42y

// specify floating point numbers using hex, octal, or binary.

let flt1 = 0x401E000000000000LF
// val flt1: float = 7.5

let flt2 = 0x00000000lf
// val flt2: float32 = 0.0f


// Arithmetic

// overflow
let short1 = 32767s + 1s
//val short1: int16 = -32768s

let short2 = -32768s + -1s
//val short2: int16 = 32767s


// Conversion Routines

let sbyte1 = sbyte -5
//val sbyte1: sbyte = -5y

let byte1 = byte "42"
//val byte1: byte = 42uy

let int16a = int16 'a'
//val int16a: int16 = 97s

let uint16a = uint16 5
//val uint16a: uint16 = 5us

let inta = int 2.5
//val inta: int = 2

let unit32a = uint32 0xFF
//val unit32a: uint32 = 255u

let int64a = int64 -8
//val int64a: int64 = -8L

let uint64a = uint64 "0xFF"
//val uint64a: uint64 = 255U

let floata = float 3.1415M
//val floata: float = 3.1415

let float32a = float32 8y
//val float32a: float32 = 8.0f

let decimala = decimal 1.25
//val decimala: decimal = 1.25M


// Big Integer

open System.Numerics

// Data storage units
let megabyte  = 1024I    * 1024I
let gigabyte  = megabyte * 1024I
let terabyte  = gigabyte * 1024I
let petabyte  = terabyte * 1024I
let exabyte   = petabyte * 1024I
let zettabyte = exabyte  * 1024I;;

(*
val megabyte: System.Numerics.BigInteger = 1048576
val gigabyte: System.Numerics.BigInteger = 1073741824
val terabyte: System.Numerics.BigInteger = 1099511627776
val petabyte: System.Numerics.BigInteger = 1125899906842624
val exabyte: System.Numerics.BigInteger = 1152921504606846976
val zettabyte: System.Numerics.BigInteger = 1180591620717411303424
*)


// Bitwise Operations

// And
let and1 = 0b1111 &&& 0b0011
// val and1: int = 3 - 0b0011

// Of
let or1 = 0xFF00 ||| 0x00FF
// val or1: int = 65535 - 0xFFFF

// Exclusive Or
let xor1 = 0b0011 ^^^ 0b0101
//val xor1: int = 6 - 0b0110

// Left Shift
let left1 = 0b001 <<< 3
//val left1: int = 8 - 0b1000

// Right Shift
let right1 = 0b1000 >>> 3


// Characters

let vowels = ['a'; 'e'; 'i'; 'o'; 'u']

printfn "hex u0061 = '%c'" '\u0061'
//hex u0061 = 'a'

// Convert value of 'C; to an integer
let int1 = int 'C'
//val int1: int = 67

// Convert value of 'C' to a byte
let byte2 = 'C'B
//val byte1: byte = 67uy


// Strings

let password = "abracadabra"

let multiline = "This string
takes up
multiple lines"

let chr1 = multiline[0]
//val chr1: char = 'T'

let chr2 = multiline[1]
//val chr2: char = 'h'

multiline[2]
//val it: char = 'i'

multiline[3]
//val it: char = 's'

// Include double quotes without escaping.
let xmlFragment = """<Ship Name="Prometheus"></foo>"""

let hello = "Hello"B
//let hello = "Hello"B;;


// Boolean Values

// And
let and2 = true && false
//val and2: bool = false

// Or
let or2 = true || false
//val or2: bool = true

// Not
let not2 = not false
//val not2: bool = true

// Example 2-2 Printing truth tables

/// Print the truth table for the given function
let printTruthTable  f =
    printfn "       |true   | false |"
    printfn "       +-------+-------+"
    printfn " true  | %5b | %5b |" (f true  true) (f true  false)
    printfn " false | %5b | %5b |" (f false true) (f false false)
    printfn "       +-------+-------+"
    printfn ""
    ()
//val printTruthTable: f: (bool -> bool -> bool) -> unit

printTruthTable (&&)
//       |true   | false |
//       +-------+-------+
// true  |  true | false |
// false | false | false |
//       +-------+-------+

printTruthTable (||)
//       |true   | false |
//       +-------+-------+
// true  |  true |  true |
// false |  true | false |
//       +-------+-------+


// Conparison and Equality

let lessThan1 = 1 < 2
//val lessThan1: bool = true

let lessThamOrEqualTo = 4.0 <= 4.0
//val lessThamOrEqualTo: bool = true

let greaterThan = 1.4E3 > 1.0e2
//val greaterThan: bool = true

let  greaterThanOrEqualTo = 0I >= 2I
//val GreaterThanOrEqualTo: bool = false

let  equalToFalse = "abc" = "abc"
//val EqualToFalse: bool = true

let  notEqualTo=  'a' <> 'b'
//val notEqualTo: bool = true

let compareTwoValues =  compare 31 31
//val compareTwoValues: int = 0
