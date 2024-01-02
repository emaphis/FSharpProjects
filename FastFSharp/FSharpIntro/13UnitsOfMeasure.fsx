// Intro to Units of Measure

// Simple Units

[<Measure>] type cm
[<Measure>] type sec

let x1 = 1.0<cm>
let x2 = 3.2<sec>

//let y = x1 + x2 
let y = x1 / x2

// Compound units

[<Measure>] type speed = cm / sec
[<Measure>] type acceleration = speed / sec
[<Measure>] type carrot = cm * sec

let s1 = x1 / x2
let s2 = 2.1<speed>
let s3 = s1 + s2
let s4 = s1 - s2

// UOM Constructiors
module Domain =
    module private Units =  // force use the 'create' function

        [<Measure>] type PackWeight

    module PackWeight =

        let create (x: float) =
            if x <= 0.0 then
                invalidArg (nameof x) "Cannot have non-Positive PackWeights"
            
            x * 1.0<Units.PackWeight>

open Domain

let m1 = PackWeight.create 10.0


// Custom Types

[<Measure>] type Cow
[<Measure>] type Chicken

type Animal<[<Measure>] 'Measure> =
    {
        Size : float
    }

    static member (+) (a: Animal<'Measure>, b: Animal<'Measure>) : Animal<'Measure> =
        { Size = a.Size + b.Size }

    static member ( * ) (a: Animal<'LMeasure>, b: Animal<'RMeasure>)  =
        let result: Animal<'LMeasure * 'RMeasure> = { Size = a.Size * b.Size }
        result

module Animal =
    
    let create (x: float<'Measure>) : Animal<'Measure> =
        { Size = float x }


let c1 = Animal.create 1.0<Chicken>
let c2 = Animal.create 2.0<Chicken>

let wut1 = c1 + c2

let c4 = Animal.create 1.0<Cow>
let wut2 =  c1 * c2
let wut3 = c1 * c4

// Hint
let q1 = 1.0<Cow>
let q2 = 2.1<Chicken>
let q3 = q1 + q2 * 1.0<_>
