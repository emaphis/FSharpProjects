// Interfaces

module Interfaces

module DefiningInterfaces =

    // Here’s an interface definition:

    type MyInterface =
        // abstract method
        abstract member Add: int -> int -> int

        // abstract immutable property
        abstract member Pi: float

        // abstract read/write property
        abstract member Area: float with get, set


    // And here’s the definition for the equivalent abstract base class:

    [<AbstractClass>]
    type AbstractBaseClass() =
        // abstract method
        abstract member Add: int -> int -> int

        // abstract immutable property
        abstract member Pi: float

        // abstract read/write property
        abstract member Area: float with get, set



module ImplementingInterfaces =

    // Explicit and implicit interface implementations
    // In F#, all interfaces must be explicitly implemented.

    type IAddingService =
        abstract member Add: int -> int -> int


    type MyAddingService() =

        interface IAddingService with
            member this.Add x y = x + y

        interface System.IDisposable with
            member this.Dispose() =
                printfn "... disposed"


    // Using interfaces

    let mas = new MyAddingService()
    //let num1 = max.Add 3 4
    //  error FS0039: The field, constructor or member 'Add' is not defined.

    // Should cast to interface
    let adder = mas :> IAddingService
    let num2 = adder.Add 3 4

    let adder2 : IAddingService = mas
    let num3 = adder2.Add 7 9


    // function that requires an interface
    // casting is automatic
    let testAddingService(addr: IAddingService) =
       let output = addr.Add 1 2
       printfn $"1 + 2 = {output}"

    let mas2 = new MyAddingService()
    do testAddingService mas2


    // `use` keyword for IDisposable
    let testDispose =
        use mas = new MyAddingService()
        printfn "testing ..."
        // Dispose() is called here.
