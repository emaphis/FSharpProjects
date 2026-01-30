module Exceptions

    module SimpleExceptions =

        // The simplest way to report an error in an F# program is to use the failwith function.

        // Using `failwith`
        let divide x y =
            if y = 0 then failwithf "Cannot divide %d by zero!" x
            x / y

        let bum1 = divide 10 0

        // System.Exception: Cannot divide 10 by zero!
        // at Microsoft.FSharp.Core.PrintfModule.PrintFormatToStringThenFail@1448.Invoke(String message)


        // The simplest way to report an error in an F# program is to use the failwith function.
        let divide2 x y =
            if y = 0 then raise <| System.DivideByZeroException()
            x / y

        let bum2 = divide2 10 0

        // System.DivideByZeroException: Attempted to divide by zero.
        //   at FSI_0012.divide2(Int32 x, Int32 y) in C:\src\FSharpProjects\ProgrammingThree\chap04\Exceptions.fsx:line 18

    module HandlingExceptions =

        // Using try-catch expressions

        // Example 4-18 shows some code that runs through a minefield of potential problems,
        // with each possible exception handled by an appropriate exception handler.

        // `:?` dynamic type test operator

        // Example 4-18. Try–catch expressions
        // See Example_4_18.fs


        // Using a try-finally expressing

        // Not catching an exception might prevent unmanaged resources from being
        // freed, such as closing file handles or flushing buffers, there is a second way
        // to catch process exceptions: try-finally expressions. In a try-finally expression,

        // Example 4-19 demonstrates a try-finally expression in action.
        let tryFinallyTest() =
            try
                printfn "Before exception ..."
                failwith "ERROR!"
                printfn "After exception is raised ..."
            finally
                printfn "Finally block is executing ..."

        let test() =
            try
                tryFinallyTest()
            with
            | ex -> printfn $"Exception caught with message: %s{ex.Message}"


        do test() ;;

        // Before exception ...
        // Finally block is executing ...
        // Exception caught with message: ERROR!
        // val it: unit = ()


    module ReraisingExceptions =

        // If you can't fix the entire problem rethrow the exception

        // Example 4-20 demonstrates reraising an exception by using the reraise function

        // Retry a function throwing an exception N times before failing.
        let tryWithBackoff f times =
            let mutable attempt = 1
            let mutable success = false

            while not success do
                try
                    do f()
                    success <- true
                with ex ->
                    attempt <- attempt + 1
                    if attempt >= times then
                        reraise()

        let f() =
            failwith "Unknown error"
            ()

        do tryWithBackoff f 5


    module DefiningExceptions =

        // Example 4-21. Lightweight F# exception

        open System
        open System.Collections.Generic

        type MagicWand = { exists : bool  }

        type Environment = {
            mutable MagicWand : MagicWand | null
            mutable isMoonFull : bool
        }

        let environment = { MagicWand = { exists = true }; isMoonFull = true }

        let isFullMoon(dt: DateTime) =
            environment.isMoonFull


        exception NoMagicWand
        exception NoFullMoon of int * int
        exception BadMojo of string

        let castHex (ingredients : HashSet<string>) =
            try
                let currentWand = environment.MagicWand

                if currentWand = null then
                    raise NoMagicWand

                if not <| ingredients.Contains("Toad Wart") then
                    raise <| BadMojo("Need Toad Wart to cast the hex!")

                if not <| isFullMoon(DateTime.Today) then
                    raise <| NoFullMoon(DateTime.Today.Month, DateTime.Today.Day)

                // Begin the incatation ...
                let mana =
                    ingredients
                    |> Seq.map (fun ing -> ing.GetHashCode())
                    |> Seq.fold (+) 0

                $"%x{mana}"

            with
            | NoMagicWand
                -> "Error: A magic wand is required to hex!"
            | NoFullMoon(month, day)
                -> "Error: Hexes can only be cast during a full moon."
            | BadMojo(msg)
                -> $"Error: Hex failed due to bad mojo [%s{msg}]"

        let ingredients = new HashSet<string>()
        ingredients.Add("grapes")
        ingredients.Add("Toad Wart")
        ingredients.Add("Cherries")

        let hex = castHex ingredients
