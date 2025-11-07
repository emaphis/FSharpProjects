module Records

// Example 3-8. Constructing and using records

// Define a record type
type PersonRec = { First : string; Last : string; Age : int }

// Construct a record
let steve = { First = "Steve"; Last = "Holt"; Age = 17 }

do printfn $"{steve.First} is {steve.Age} years old"
//Steve is 17 years old


module CloningRecords =

    // Records can be clonew using the `with` keyword:

    type Car =
        {
            Make  : string
            Model : string
            Year  : int
        }

    let thisYears's = { Make = "FSharp"; Model = "Luxury Sean"; Year = 2012 }
    let nextYear's = { thisYears's with Year = 2013 }



module PatternMatching =

    open CloningRecords

    let thisCoup = { Make = "Ford"; Model = "Coup"; Year = 2011 }
    let thatCoup = { Make = "Buick"; Model = "Coup"; Year = 2012 }

    let allNewCars = [ thisCoup; thisYears's; thatCoup; nextYear's ]

    let allCoups =
        allNewCars
        |> List.filter
            (function
                | { Model = "Coup"} -> true
                | _   -> false )

    do printfn $"The coups = {allCoups}"



module TypeInference =

    // .NET classes must be annotated it be used but record types can be
    // infered by field name

    // Example 3-9. Type inference for records
    type Point = { X : float; Y : float }

    // Distance between two points. (No type annotations requred)
    let distance1 pt1 pt2 =
        let square x = x * x
        sqrt <| square (pt1.X - pt2.X) + square (pt1.Y - pt2.Y)

    //val distance1: pt1: Point -> pt2: Point -> float

    let dist = distance1 { X = 0.0; Y = 0.0 } { X = 10.0; Y = 10.0 }
    //val dist: float = 14.14213562


    // Type inference for recoreds with identicle field names

    //type Point   = { X: float; Y: float;}
    type Vector3 = { X : float; Y : float; Z : float }

    // Provide a type annotation to not infer pt1 and pt2 to be Vector3
    // (since Vector3 was defined last witht fields X and Y)
    let distance2 (pt1: Point) (pt2: Point) =
        let square x = x * x
        sqrt <| square (pt1.X - pt2.X) + square (pt1.Y - pt2.Y)

    // Disambiguate a Point from the Vector3 type by
    // fully qualifying record fields.
    let origin = { Point.X = 0.0; Point.Y = 0.0 }



module MethodsProperties =

    // Add a property to a record type Vector
    type Vector =
        { X : float; Y : float; Z : float }
        member this.Length =
             sqrt <| this.X ** 2.0 + this.Y ** 2.0 + this.Z ** 2.0


    let v = { X = 10.0; Y = 20.0; Z = 30.0 }

    let len = v.Length
    //val len: float = 37.41657387
