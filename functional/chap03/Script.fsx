// Chapter 3 - Tuples, records and tagged values

// 3.1 Tuples

(10, true)
(("abc", 1), -3)

// Tuple patterns
let (x, n) = (3, 2)

// patterns with constants
let (x1, 0) = ((3, "a"), 0)

// incomplete
//let (x2, 0) = (3,  2)

// The wildcard pattern c
let ((_,x3),_,z3) = ((1,true), (1,2,3), false)

// illegal
//let (x4, x4) = (1, 1)


// 3.2 Polymorphism

let swap (x, y) = (y, x)
// val swap: x: 'a * y: 'b -> 'b * 'a
// polymorphic type

swap ('a', "ab")
swap ((1, 3), ("ab", true))

// fst: ’a * ’b -> ’a and snd: ’a * ’b -> ’b
fst ((1,"a",true), "xyz")
snd ('z', ("abc", 3.0))


// 3.3 Example: Geometric vectors

// float * float

/// Vector reversal:
let (~-.) (x:float, y:float) = (-x, -y)

/// Vector addition
let (+.) (x1, y1) (x2, y2) =  (x1+x2,y1+y2) : float * float

/// Vector subtraction
let (-.) v1 v2 = v1 +. -. v2

/// Multiplication by a scalar
let ( *.) x (x1,y1) = (x*x1, x*y1): float*float

/// Dot product:
let (&.) (x1,y1) (x2,y2) = x1*x2 + y1*y2: float

///Norm (length)
let norm(x1:float,y1:float) = sqrt(x1*x1+y1*y1)

// examples
let a = (1.0, -2.0)
let b = (3.0, 4.0)

let c = 2.0 *. a -. b  // (-1.0, -8.0)
let d = c &. a   // 15.0


// 3.4 Records

type Person = {age : int; birthday : int * int;
                name : string; sex : string }

let john = {name = "John"; age = 29
            birthday = (2,11)
            sex = "M"}

john.birthday
john.sex

// Equality and ordering

john = { age = 29; name = "John";
         sex = "M"; birthday = (2,11) }


type T1 = {a:int; b:string}

let v1 = {a=1; b="abc"}
let v2 = {a=2; b="ab"}
v1 < v2  // true

type T2 = {b:string; a:int}
let v1' = {T2.a=1; b="abc"}
let v2' = {T2.a=2; b="ab"}
v1'>v2'  // true

// Record patterns

let sue = {name="Sue"; age = 19; sex="F";
           birthday = (24,12)}
let {name = x4; age = y4; sex = s4; birthday = (d1,m)} = sue
// val y4: int = 19
// val x4: string = "Sue"
// val s4: string = "F"
// val m: int = 12
// val d1: int = 24

// Record patterns are used when defining functions
let age {age = a; name = _; sex=_; birthday=_} = a

age sue
age john

let isYoungLady {age=a; sex=s; name=_; birthday=_}
            = a < 25 && s = "F"

isYoungLady john
isYoungLady sue


// 3.5 Example: Quadratic equations

type Equation = float * float * float
type Solution = float * float

// solve: Equation -> Solution

// Error handling

exception Solve

let solve (a, b, c) =
    if b*b - 4.0*a*c < 0.0 || a = 0.0 then raise Solve
    else ((-b + sqrt(b*b - 4.0*a*c)) / (2.0*a),
          (-b - sqrt(b*b - 4.0*a*c)) / (2.0*a))

// solve (1.0, 0.0, 1.0)  // throws exception
solve (1.0, 1.0, -2.0)
solve (2.0, 8.0, 8.0)

// failwith
let solve2 (a, b, c) =
    if b*b - 4.0*a*c < 0.0 || a = 0.0
    then failwith "discriminant is negative of a=0.0"
    else ((-b + sqrt(b*b - 4.0*a*c)) / (2.0*a),
          (-b - sqrt(b*b - 4.0*a*c)) / (2.0*a))

//solve2 (0.0, 1.0, 2.0)  // failwith
solve2 (1.0, 1.0, -2.0)


// 3.6 Locally declared identifiers

let solve3 (a, b, c) =
    let d = b*b - 4.0*a*c
    if d < 0.0 || a = 0.0
    then failwith "discriminant is negative of a=0.0"
    else ((-b + sqrt d) / (2.0*a), (-b - sqrt d) / (2.0*a))

solve3 (1.0, 1.0, -2.0)

let solve4 (a, b, c) =
    let sqrtD =
        let d = b*b - 4.0*a*c
        if d < 0.0 || a = 0.0
        then failwith "discriminant is negative of a=0.0"
        else sqrt d
    ((-b + sqrtD) / (2.0*a), (-b - sqrtD) / (2.0*a))

solve4 (1.0, 1.0, -2.0)


// 3.7 Example: Rational numbers. Invariants

// Representation. Invariant
type Qnum = int*int

let rec gcd (m, n) =
    if m=0 then n
    else gcd (n % m, m)

/// cancels common divisors
let canc (p, q) =
    let sign = if p*q < 0 then -1 else 1
    let ap = abs p
    let aq = abs q
    let d = gcd(ap, aq)
    (sign * (ap /d), aq / d)

let mkQ = function
    | (_, 0) -> failwith "Division by zero"
    | pr     -> canc pr

let (.+.) (a, b) (c, d) = canc (a*d + b*c, b*d)

let (.-.) (a,b) (c,d) = canc(a*d-b*c, b*d)

let (.*.) (a, b) (c, d) = canc(a*c, b*d)

let (./.) (a, b) (c, d) = (a, b) .*. mkQ(d, c)

let (.=.) (a,b ) (c, d) = (a,b) = (c,d)

let toString(p:int,q:int) = (string p) + "/" + (string q)

let q1 = mkQ(2, -3)
let q2 = mkQ(5, 10)
let q3 = q1 .+. q2
toString(q1 .-. q3 ./. q2)


// 3.8 Tagged values. Constructors

type Shape = | Circle of float
             | Square of float
             | Triangle of float * float * float

// Constructors and values

Circle 1.2
Circle (8.0 - 2.0 * 3.4)

// Equality and ordering

Circle 1.2 = Circle (1.0 + 0.2)
Circle 1.2 = Square 1.2

Circle 1.2 < Circle 1.0
Circle 1.2 < Square 1.2
Triangle(1.0,1.0,1.0) > Square 4.0

// Constructors in patterns

let area = function
    | Circle r           -> System.Math.PI * r * r
    | Square s           -> s * s
    | Triangle (a, b, c) ->
        let s = (a + b + c) / 2.0
        sqrt(s * (s-a) * (s-b) * (s-c))

area (Circle 1.2)
area (Triangle (3.0, 4.0, 5.0))

// Invariant for the representation of shapes

/// invariants for Shape
let isShape = function
    | Circle r   -> r > 0.0
    | Square s   -> s > 0.0
    | Triangle (a,b,c) ->
        a > 0.0 && b > 0.0 && c > 0.0
        && a < b + c && b < c + a && c < a + b


let area2 x =
    if not (isShape x)
    then failwith "not a legal shape"
    else match x with
        | Circle r -> System.Math.PI * r * r
        | Square a -> a * a
        | Triangle(a,b,c) ->
            let s = (a + b + c)/2.0
            sqrt(s*(s-a)*(s-b)*(s-c))

area2 (Triangle(3.0,4.0,5.0))

area2 (Triangle(3.0,4.0,7.5))


// 3.9 Enumeration types

type Colour = Red | Blue | Green | Yellow | Purple

Green

let niceColour = function
    | Red   -> true
    | Blue  -> true
    | _     -> false

niceColour Purple

type Month = January | February | March | April
            | May | June | July | August | September
            | October | November | December

let daysOfMonth = function
    | February -> 28
    | April | June | September | November -> 30
    | _ -> 31


// 3.10 Exceptions

let solveText eq =
    try
        string(solve eq)
    with
    | Solve -> "No solutions"

solveText (1.0, 1.0, -2.0)

solveText (1.0, 0.0, 1.0)

// failwith
try
    toString (mkQ(2,0))
with
| Failure s -> s


// 3.11 Partial functions. The option type

Some false
Some (1, "a")

None

Option.get(Some (1, "a"))
Option.get(Some 1)

//Option.get None + 1

//let optFact n = if n < 0 then None else Some(fact n)

let rec optFact = function
  | 0          -> Some 1
  | n when n>0 -> Some (n * Option.get(optFact (n-1)))
  | _          -> None

optFact 5
optFact -2
