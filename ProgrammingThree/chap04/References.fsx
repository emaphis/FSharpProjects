module References

let isNull = function
    | null -> true
    | _ -> false

let bool1 = isNull "A string"
//val bool1: bool = false

let bool2 = isNull (null : string)
//val bool2: bool = true

// references types do no have null default values

type Thing = Plant | Animal | Mineral

// Error: Thing can not be null
let testThing thing = function
    | Plant -> "Plant"
    | Animal -> "Animal"
    | Mineral -> "Mineral"
   // | null -> "(null)"
    // References.fsx(22,7): error FS0043: The type 'Thing' does not have 'null' as a proper value


module ReferenceTypeAliasing =

// two reference types point to the same memory address on the heap.
// This is known as aliasing

    // Example 4-2. Aliasing reference types

    // Value x points to an array, while y points
    // to the same memory address that x does
    let x = [| 0 |]
    let y = x

    // If you modify the value of x...
    x[0] <- 3
    // .. x will change
    x
    //val it: int array = [|3|]
    // .. so will y
    y
    //val it: int array = [|3|]


module ChangingValues =

// Mutable variables are those that you can cange.

    let mutable message = "World"
    printfn $"Hello, {message}"
    //Hello, World

    message <- "Universe"
    printfn $"Hello, {message}"
    //Hello, Universe

    // Example 4-3. Errors using mutable values in closures

    // ERROR: Cannot use mutable values except in the function they are defined
    let invalidUseOfMutable () =
        let mutable x = 0

        let incrementX () = x <- x + 1
        incrementX ()

        x

    let x1 = invalidUseOfMutable ()


module ReferenceCells =

// allows you to store mutable data on the heap, enabling you to bypass limitations
// with mutable values that are stored on the stack.
// NOTE:  `:=` and `(!)` are deptrecated, use x.Value instead.

    // ref
    // val it: ('a -> 'a ref)

    // Example 4-4. Using ref cells to mutate data

    let planets =
        ref [
            "Mercury"; "Venus";   "Earth";
            "Mars";    "Jupiter"; "Saturn";
            "Uranus";  "Neptune";  "Pluto"
        ]

    // Change in status for Pluto ...

    // Filter all planers not equal to "Pluto"
    // Get the value of the planets ref cell using (!)
    // then assign the new value using

    // planets := !planets |> List.filter (fun p -> p <> "Pluto")

    // References.fsx(97,16): info FS3370: The use of '!' from the F# library is
    // deprecated. See https://aka.ms/fsharp-refcell-ops.
    // For example, please change '!cell' to 'cell.Value'.

    // Do this instead:
    planets.Value <- planets.Value |> List.filter (fun p -> p <> "Pluto")

    do printfn "%A" planets


    // `decr` and `incr` decrement and increment int ref types.  Like `--` and `++`
    // NOTE: `decr` and `incr` have been decremated

    let x = ref 0

    do incr x

    let val1 = x.Value

    do decr x

    let val2 = x.Value


module MutableRecords =

    // Mutable record types
    open System

    type MutableCar = { Make : string; Model : string; mutable Miles : int}

    let driveForASeason car =
        let rng = new Random()
        car.Miles <- car.Miles + rng.Next() % 10000


    let kitt = { Make = "Pontiac"; Model = "Trans Am"; Miles = 0}

    do driveForASeason kitt
    do driveForASeason kitt
    do driveForASeason kitt
    do driveForASeason kitt

    do printfn "kitt = %A" kitt
