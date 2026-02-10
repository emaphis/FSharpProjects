// Understanding Classes
module UnderstandingClasses

    // Constructors, Generic classes


module ExplicitConstruction =

    // Example 5-3. Explicit class construction syntax

    type Point1 =
        val m_x : float
        val m_y : float

        // Construcion 1 - Takes two parameters
        new (x, y) = { m_x = x; m_y = y }

        // Construction 2 - Takes no parameters
        new () = { m_x = 0.0; m_y = 0.0 }

        member this.length =
            let sqr x = x * x
            sqrt <| sqr this.m_x + sqr this.m_y


    let p1 = Point1(1.0, 1.0)  // Don't need `new` in modern F#
    let p2 = Point1()

    printfn $"pt1 = {p1.length}"
    printfn $"pt2 = {p2.length}"


    // Example 5-4. Arbitrary execution before explicit constructor
    open System

    type Point2 =
        val m_x : float
        val m_y : float

        // Parse a string, e.g. "1.0, 2.0"
        new (text: string) as this =
            // Do any required pre-processing
            if text = null then
                raise <| ArgumentException("text")

            let parts = text.Split([| ',' |])
            let successX, x = Double.TryParse(parts[0])
            let successY, y = Double.TryParse(parts[1])

            if not successX || not successY then
                raise <| ArgumentException("text")

            // Initialize class fields
            { m_x = x; m_y = y }
            then
                // Do any post processing
                printfn $"Initialized to [%f{this.m_x}, %f{this.m_y}]"


    let p3 = Point2("1.0, 2.0")

    printfn $"p3 = %f{p3.m_x}, %f{p3.m_y}"


module ImplicitClassConstruction =

    // Example 5-5. Implicit class construction

    open System

    type  Point3(x : float, y : float) =

        let length =
            let sqr x = x * x
            sqrt <| sqr x + sqr y

        do printfn $"Initialized to [%f{x}, %f{y}]"

        member this.Y = y
        member this.Length = length

        // Define custom constructors, these must
        // call the `main` constructor
        new() = Point3(0.0, 0.0)

        // Define a second constructor
        new(text : string) =
            // Do any required pre-processing
            if text = null then
                raise <| ArgumentException("text")

            let parts = text.Split([| ',' |])
            let successX, x = Double.TryParse(parts[0])
            let successY, y = Double.TryParse(parts[1])

            if not successX || not successY then
                raise <| ArgumentException("text")

            // Calls the primary constructor
            Point3(x, y)  // redundant `new`


    let p6 = Point3(1.0, 1.0)
    let p7 = Point3("1.0, 2.0")

    printfn $"p6 = {p6.Length}"
    printfn $"p7 = {p7.Length}"


module GenericClasses =

    // Example 5-6. Defining generic classes

    // Define a generic class
    type Arrayify<'a>(x : 'a) =
        member this.EmptyArray : 'a[] = [|  |]
        member this.ArraySize1 : 'a[] = [| x |]
        member this.ArraySize2 : 'a[] = [| x; x |]
        member this.ArraySize3 : 'a[] = [| x; x; x |]


    let arrayifyTuple = Arrayify<int * int>( (10, 27))

    let arrytup1 = arrayifyTuple.ArraySize3
    //val arrytup1: (int * int) array = [|(10, 27); (10, 27); (10, 27)|]

    let inferred = Arrayify<_>( "a string" )

    // Generic discriminated union
    type GenDU<'a> =
        | Tag1 of 'a
        | Tag2 of string * 'a list

    let tag2 = Tag2("Primary Colors", [ 'R', 'G', 'B' ])
    // val tag2: GenDU<char * char * char> = Tag2 ("Primary Colors", [('R', 'G', 'B')])

    // A generic record type
    type GenRec<'a, 'b> =
        { Field1 : 'a
          Field2 : 'b }

    let x3 = { Field1 = "Blue"; Field2 = 'C' }
    // val x3: GenRec<string,char> = { Field1 = "Blue"  Field2 = 'C' }


module TheSelfIdentifier =

    // The `self` identifier doesn't have to be named self in F#

    // Example 5-7. Naming the this-pointer
    open System

    type Circle =
        val m_radius : float

        new(r) = { m_radius = r }
        member foo.Radius = foo.m_radius
        member bar.Area = Math.PI * bar.Radius * bar.Radius

    let cir1 = Circle(10.0)

    printfn $"Cir1 radius = %f{cir1.Area}"
    // Cir1 radius = 314.159265
