// 2 - Values, operators, expressions and functions


// 2.1 Numbers. Truth values. The unit type

// Numbers
0
0.0
0123
-7.235
-388890
1.23e-17

// Operators
2 - - -1
13 / -5
13 % -5

// Truth values
true
false
let even n = n % 2 = 0

1 <> 2  // structural inequality
not true <> false

//1 = 2 && fact -1 = 0

// The 'unit' type
()  // dummy value


// 2.2 Operator precedence and association

// Arithmetic operator
// + unary plus
// - unary minus
//-------------------
// + addition
// - subtraction
// * multiplication
// / division
// % modulo (remainder)
// ** exponentiation

// Operator precedence and associatio

// **             Associates to the right
// * / %          Associates to the left
// + -            Associates to the left
// = <> > >= < <= No association
// &&             Associates to the left
// ||             Associates to the left


// 2.3 Characters and strings

// char
'a'
' '

// Character escape sequences
// \'   Apostrophe
// \"   Quote
// \\   Backslash
// \b   Backspace
// \n   Newline
// \r   Carriage return
// \t   Horizontal tab

let isLowerCaseVowel ch =
    ch='a' || ch='e' || ch='i' || ch='o' || ch='u'

let isLowerCaseConsonant ch =
    System.Char.IsLower ch && not (isLowerCaseVowel ch)

isLowerCaseVowel 'i' && not (isLowerCaseConsonant 'i')

isLowerCaseVowel 'I' || isLowerCaseConsonant 'I'

not (isLowerCaseVowel 'z') && isLowerCaseConsonant 'z'

// Strings

"abcd---"
"\"1234\""
""
// verbatim string notation
@"\\\\"
"\\\\"

// Functions on strings

String.length "1234"
String.length "\"1234\""
String.length ""

let text = "abcd---"
text + text
text + " " + text
text + "" = text

"abc"[0]
"abc"[2]
//"abc"[3]

"abc" + string 'd'
string -4
string 7.89
string true

let nameAge (name, age) =
    name + " is " + (string age) + " years old"

nameAge("Diana", 15+4)
nameAge("Philip", 1-4)

string (12, 'a')
string nameAge


// 2.4 If-then-else expressions

// 'even' see above

let adjString s =
    if even (String.length s)
    then s else " " + s

adjString "123"
adjString "1234"

let rec gcd(m,n) =
    if m=0 then n
    else gcd(n % m, m)


// 2.5 Overloaded functions and operators

// infered from context
let square1 x = x * x
square1 4.0

// declared
let square2 (x: float) = x * x
square2 4.0

// determined from context
// abs, acos, atan, atan2, ceil, cos, cosh, exp, floor, log
// log10, pow, pown, round, sin, sinh, sqrt, tan, tanh

abs -1
abs -1.0
abs -3.2f


// 2.6 Type inference

let rec power = function
    | (x, 0) -> 1.0   // x and n type determined here
    | (x, n) -> x * power(x, n-1)  // tuple type determined here



// 2.7 Functions are first-class citizens

// The value of a function can be a function

(+)
// al it: (int -> int -> int) = <fun:it@34>

(+) 5
// val it: (int -> int) = <fun:it@36-2>

(+) 5 4
// val it: int = 9

// The argument of a function can be a function

// compostion
let f = fun y -> y+3
let g = fun x -> x*x

let h = f << g   // h = (f o g)
h 4

// or using function expressions
((fun y -> y+3) << (fun x -> x*x)) 4

// Declaration of higher-order functions

let weight ro = fun s -> ro * s ** 3.0

// partial evaluations to create new function

let waterWeight = weight 1000.0

waterWeight 1.0
waterWeight 2.0

let mentholWeight = weight 786.5

mentholWeight 1.0
mentholWeight 2.0

// supplying all the parameters  - normal
let weight1 ro s = ro * s ** 3.0 


// 2.8 Closures

let pi = System.Math.PI
let circleArea r = pi * r * r

//  | pi  -> 3.14....
//  | circleArea -> (r, pi*r*r, [pi -> 3.14...])

// but
//let pi = 0 
circleArea 1.0


// 2.9 Declaring prefix and infix operators

// infix
///  exclusive-or operator
let (.||.) p q = (p || q) && not (p && q)

(1 > 2) .||. (2 + 3 < 5)

// prefix
let (~%%) x = 1.0 / x

%% 0.5


// Equality and ordering

// equality and inequality operators = and <>
3.5 = 2e-3
"abc" <> "ab" 

// not defined on function
//cos = sin

// equality is automatically extended when a new type is defined
let eqText x y =
    if x = y then "equal" else "not equal"

eqText 3 4
eqText ' ' (char 32)

// Ordering
// ordering operators: >, >=, <, and <= a

'A' < 'a'
"automobile" < "car"
"" < " "

// ordering on arbitrary types
let ordText1 x y =
    if x > y then "greater"
    else if x = y then "equal"
    else "less"

// ordering with compare operator
let ordText2 x y =
    match compare x y with
    | t when t > 0 -> "greater"
    | 0            -> "equal"
    | _            -> "less"


// 2.11 Function application operators |> and <|

// arg |> fct means fct arg
// fct <| arg means fct arg


// 2.12 Summary of the basic types
