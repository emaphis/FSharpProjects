// Advanced F#
// Units of Measure


// Use Cases:
// Statically Checked Type Conversions
// Decorating Data With Contextual Information


// Defining Units

[<Measure>] type m                  (* meter *)
[<Measure>] type s                  (* second *)
[<Measure>] type kg                 (* kilogram *)
[<Measure>] type N = (kg * m)/(s^2) (* Newtons *)
[<Measure>] type Pa = N/(m^2)       (* Pascals *)


// Creating instances

let distance = 100.0<m>
let time = 5.0<s>
let speed = distance / time


// Converting between Unit.

[<Measure>] type C
[<Measure>] type F

let to_fahrenheit (x : float<C>) = x * (9.0<F>/5.0<C>) + 32.0<F>
let to_celsius (x : float<F>) = (x - 32.0<F>) * (5.0<C>/9.0<F>)


// UOM are statically checked an compile time

let calcSpeed (x: float<m>) (y: float<s>) = x / y

let spd1 = calcSpeed 20.0<m> 4.0<s>
//val spd1: float<m/s> = 5.0

//let spd2 = calcSpeed 20.0<m> 4.0<m>
//  42  29 error F# Compiler 1    Type mismatch. Expecting a


// Units can be defined for integral types too.

[<Measure>] type col
[<Measure>] type row
let colOffset (a : int<col>) (b : int<col>) = a - b
let rowOffset (a : int<row>) (b : int<row>) = a - b


// Dimensionless Values

/// covert a scalar to a unit
let to_meters (x: float) = x * 1.0<m>

let of_meters (x : float<m>) = float x


// Generalizing Units of Measure
// Since measures and dimensionless values are (or appear to be) generic types,
//we can write functions which operate on both transparently:

let vanillaFloats = [10.0; 15.5; 17.0]
let lengths = [ for a in [2.0; 7.0; 14.0; 5.0] -> a * 1.0<m> ]
let masses = [ for a in [155.54; 179.01; 135.90] -> a * 1.0<kg> ]
let densities = [ for a in [0.54; 1.0; 1.1; 0.25; 0.7] -> a * 1.0<kg/m^3> ]


// Generic over UOM
let average (l : float<'u> list) =
    let sum, count = l |> List.fold (fun (sum, count) x -> sum + x, count + 1.0<_>) (0.0<_>, 0.0<_>)
    sum / count

let avVF = average vanillaFloats
// val avVF: float = 14.16666667

let avLen = average lengths
//  val avVF: float = 14.16666667

let avMass = average masses
// val avMass: float<kg> = 156.8166667

let avDens = average densities
// val avDens: float<kg/m ^ 3> = 0.718

// Since units are erased from compiled code, they are not considered a real
// data type, so they can't be used directly as a type parameter in generic
// functions and classes.

//type triple<'a> = { a : float<'a>; b : float<'a>; c : float<'a>}
//F# Compiler 702 xpected unit-of-measure parameter, not type parameter. Explicit
// unit-of-measure parameters must be marked with the [<Measure>] attribute.

// but

type triple<[<Measure>] 'a> = { a : float<'a>; b : float<'a>; c : float<'a>}

let trpl = { a = 7.0<kg>; b = -10.5<_>; c = 0.5<_> }
// val trpl: triple<kg> = { a = 7.0
//                         b = -10.5
//                         c = 0.5 }
