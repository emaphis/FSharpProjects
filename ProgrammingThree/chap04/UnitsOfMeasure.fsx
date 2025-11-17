module UnitsOfMeasure

[<Measure>]
type fahrenheit

let printTemperature (temp : float<fahrenheit>) =
    if temp < 32.0<_>  then
        printfn "Below Freezing!"
    elif temp < 65.0<_>  then
        printfn "Cold"
    elif temp < 75.0<_>  then
        printfn "Just right!"
    elif temp < 100.0<_> then
        printfn "Hot!"
    else
        printfn "Scorching"


let seattle = 59.0<fahrenheit>

do printTemperature seattle
//Cold

[<Measure>]
type celsius

let cambridge = 18.0<celsius>

// printTemperature cambridge
//error FS0001: Type mismatch. Expecting a
//  'float<fahrenheit>'
//but given a
//  'float<celsius>'

// Units of measure also can be compounded by multiplication or division.

// Define a measure for meters
[<Measure>]
type m

// Multiplication, goes to meters squared
1.0<m> * 1.0<m>
//val it: float<m ^ 2> = 1.0

// Division, drops unit entirely
1.0<m> / 1.0<m>
//val it: float = 1.0

// Repeated division, results in 1 / meters
1.0<m> / 1.0<m> / 1.0<m>
//val it: float</m> = 1.0

module DefiningUnitsOfMeasure =

    // Example 4-6. Defining new units of measure

    // Define seconds and hertz
    [<Measure>]
    type s

    [<Measure>]
    type Hz = s ^ -1

    // If Hz was not convertible to s, this
    // would result in a compile error.
    3.0<s ^ -1> = 3.0<Hz>
    //val it: bool = true

    // Adding methods to units of
    [<Measure>]
    type far =
        static member ConvertToCel(x: float<far>) =
            (5.0<cel> / 9.0<far>) * (x - 32.0<far>)

    and [<Measure>] cel =
        static member ConvertToFar(x : float<cel>) =
            (9.0<far> / 5.0<cel> * x) + 32.0<far>

    // UnitSymbols contains the abbreviated versions of SI units.
    open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols

    // In candela, the SI unit of luminous intensity.
    let flashlightIntensity = 80.0<cd>

    // The UnitNames contains the full-names of SI units.
    open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames

    // This might be worth a few dollars, euros, yen, etc.
    let world'sLargestGoldNugget = 280.0<kilogram>


module ConvertingBetweenUnitsOfMeasure =

    // Example 4-7. Converting units of measure

    // Radians
    [<Measure>]
    type rads

    let halfPI = System.Math.PI * 0.5<rads>

    // ERROR: Pass a float<_> to a function accepting a float
    //let ang1 = sin halfPI
    //error FS0001: The type 'float<rads>' does not match the type 'float'

    let ang2  = sin (float halfPI)
    //val ang2: float = 1.0


module GenericUnitsOfMeasure =

    open DefiningUnitsOfMeasure

    let squareMeter (x : float<m>) = x * x
    //val squareMeter: x: float<m> -> float<m ^
    
    let genericSquare (x : float<_>) = x * x
    //val genericSquare: x: float<'u> -> float<'u ^ 2>

    let sq1 = genericSquare 1.0<m/s>
    //val sq1: float<m ^ 2/s ^ 2> = 1.0

    let sq2 = genericSquare 9.0
    //val sq2: float = 81.0


    // Example 4-8. Creating a type that is generic with respect to a unit of measure

    // Represents a point respecting the unit of measure
    type Point< [<Measure>] 'u > (x: float<'u>, y: float<'u>) =

        member this.X = x
        member this.Y = y

        member this.UnitlessX = float x
        member this.UnitlessY = float y

        member this.Length =
            let sqr x = x * x
            sqrt <| sqr this.X + sqr this.Y

        override this.ToString () =
            $"{{%f{this.UnitlessX}, %f{this.UnitlessY}}}"

    let p = Point<m>(10.0<m>, 10.0<m>)
    //val p: Point<m> = {10.000000, 10.000000}

    let len1 = p.Length
    //val len1: float<m> = 14.14213562
