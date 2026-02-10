// Chapter 5 - Methods and

module MethodsAndProperties

    // The user interface to classes are provided by methods and properites

module Properties =

    // Example 5-8. Defining class properties
    // Define a WaterBottle as a type with two properties
    [<Measure>]
    type ml

    type WaterBottle() =
        let mutable m_amount = 0.0<ml>

        // Read-only property
        member this.Empty = (m_amount = 0.0<ml>)

        // Read/write property
        member this.Amount with get ()  = m_amount
                                    and  set newAmt = m_amount <- newAmt

    let bottle = WaterBottle()

    let bool1 = bottle.Empty  // true
    do bottle.Amount <- 1000.0<ml>
    let bool2 = bottle.Empty  // false
    let amt = bottle.Amount
    do printfn $"Ammount = %f{amt}"


module Methods =

    type Television =

        val mutable m_channel : int
        val mutable m_turnedOn : bool

        new() = { m_channel = 3; m_turnedOn = true }

        member this.TurnOn() =
            printfn "Tuning on .."
            this.m_turnedOn <- true

        member this.TrunOff() =
            printfn "Turning off..."
            this.m_turnedOn <- false

        member this.ChangeChannel(newChannel : int) =
            if this.m_turnedOn = false then
                failwith "Cannot change channel, the TV is not on."

            printfn $"Changing channel to %d{newChannel}..."
            this.m_channel <- newChannel

        member this.CurrentChannel = this.m_channel


    // Curry class methods
    type Adder() =
        // Curried method arguments
        member this.AddTwoParams x y = x + y
        // Normal arguments
        member this.AddTwoTupledParams (x, y) = x + y

    let adder = Adder()
    let add10 = adder.AddTwoParams 10
    // val add10: (int -> int)
    let num1 = add10
    // val num1: int = 14

    let num2 = adder.AddTwoParams 10 4
    // val num2: int = 14


module StaticMethodsPropertiesFields =

    // Class methods, properties and fields

    // Example 5-10. Static methods

    // Declaring a static method
    type SomeClass() =
        static member StaticMember() = 5

    let num3 = SomeClass.StaticMember

    let sc1 = SomeClass()
    //let wut2 = sc1.StaticMember()
    // error FS0493: StaticMember is not an instance method


    // Static fields

    // Example 5-11. Creating and using static fields

    // Static fields
    type RareType() =

        // there is only one instance of m_numLeft for all instances of RareType
        static let mutable m_numLeft = 2

        do
            if m_numLeft <= 0 then
                failwith "No more RareType left!"
            m_numLeft <- m_numLeft - 1
            printfn $"Initialized a rare type, only %d{m_numLeft} left!"

        static member NumLeft = m_numLeft

    let rt1 = RareType()

    let rt2 = RareType()

    //let rt3 = RareType()


module MethodOverloading =

    // Example 5-12. Method overloading in F#
    type BitCounter =

        static member CountBits (x: int16) =
            let mutable x' = x
            let mutable numBits = 0
            for i = 0 to 15 do
                numBits <- numBits + int (x' &&& 1s)
                x' <- x' >>> 1
            numBits

        static member CountBits (x: int) =
            let mutable x' = x
            let mutable numBits = 0
            for i = 0 to 31 do
                numBits <- numBits + int (x' &&& 1)
                x' <- x' >>> 1
            numBits

        static member CountBits (x : int64) =
            let mutable x' = x
            let mutable numBits = 0
            for i = 0 to 63 do
                numBits <- numBits + int (x' &&& 1L)
                x' <- x' >>> 1
            numBits


    let cnt1 = BitCounter.CountBits 1
    let cnt2 = BitCounter.CountBits 4L


module AccessibilityModifiers =

    // Example 5-13. Accessibility modifiers

    open System

    type internal Ruby private(shininess, carats) =

        let mutable m_size = carats
        let mutable m_shininess = shininess

        // Polishing increases shininess but decreases size
        member this.Polish() =
            this.Size <- this.Size - 0.1
            m_shininess <- m_shininess + 0.1

        // Public getter, private setter
        member public  this.Size with get ()   = m_size
        member private this.Size with set newSize = m_size <- newSize

        member this.Shininess = m_shininess

        public new() =
            let rng = Random()
            let s = float (rng.Next() % 100) + 0.01
            let c = float (rng.Next() % 16) + 0.1
            Ruby(s, c)

        public new(carats) =
            let rng = Random()
            let s = float (rng.Next() % 100) + 0.01
            Ruby(s, carats)


module AccessibilityModifiersOnModules =

    // Example 5-14. Accessibility modifiers in modules
    open System.IO
    open System.Collections.Generic

    module Logger =

        let mutable private m_filesToWriteTo = List<string>()

        let AddLogFile filePath = m_filesToWriteTo.Add(filePath)

        let LogMessage(message : string) =
            for logFile in m_filesToWriteTo do
                use file = new StreamWriter(logFile, true)
                file.WriteLine(message)


    //let currentDir = Directory.GetCurrentDirectory()
    // "C:\src\FSharpProjects\ProgrammingThree\chap05"

    do Logger.AddLogFile("out.txt")
    do Logger.LogMessage("Hello message")
    do Logger.LogMessage("Status message")


//module FSharpSignatureFile =
    // F# signature file
    // File.fsi
    // File.fs
    // Program.fs

    //#load "File.fsi"
    //#load "File.fs"

    //open MyProject.Utilities
    //let mClass = MyClass()
