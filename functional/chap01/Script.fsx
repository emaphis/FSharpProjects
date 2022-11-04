// Chapter 01 - Getting started
// F# notions of values, expressions, types and declarations, including recursive
// function declarations.
// The main concepts needed to explain the meaning of these notions are: integers and
// floating-point numbers, bindings and environments, and step by step evaluation of expressions.


// 1.1 Values, types, identifiers and declarations

2*3 + 4


// 1.2 Simple function declarations

// circleArea(r) = πr2.

System.Math.PI

// Comments
(* Area of circle with radius *)
/// Area of circle with radius
let circleArea r = System.Math.PI * r * r

circleArea 2.0


// 1.3 Anonymous functions. Function expressions

fun r -> System.Math.PI * r * r

let circleArea2 = fun r -> System.Math.PI * r * r

// Function expressions with patterns

/// function giving the number of days in a month, where a month is given by its
/// number, that is, an integer between 1 and 12.
let daysOfMonth = function
    | 2        -> 28 // February
    | 4|6|9|11 -> 30 // April, June, September, November
    | _        -> 31 // All other months


// 1.4 Recursion

// Recursive declaration
let rec fact = function
    | 0 -> 1
    | n -> n * fact(n-1)


// 1.5 Pairs

let a = (2.0,3)

let (x,y) = a

let rec power = function
    | (x,0)  -> 1.0
    | (x,n)  -> x * power(x,n-1)


// 1.6 Types and type checking

// 1.7 Bindings and environments

let a1 = 3

let b2 = 7.0

let c2 = (2, 8)

let area r = System.Math.PI * r * r

// at this point the environment contains:

//        | "basis library"
//        | a     => 3
//        | b     => 7.0 
// env2 = | c     => (2,8)
//        | area  => "The area function"


// 1.8 Euclid's algorithm

let rec gcd = function
    | (0,n) -> n
    | (m,n) -> gcd(n % m,m)


// 1.9 Evaluations with environments
