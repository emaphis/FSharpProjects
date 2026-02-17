module ObjectExpressions

open Interfaces.ImplementingInterfaces

// Implementing interfaces with object expressions

// create a new object that implements IDisposable
let makeResource name = {
    new System.IDisposable with
        member this.Dispose() = printfn $"{name} is disposed"
}

let useAndDisposeResources =
    use r1 = makeResource "first resource"
    printfn "using first resource"

    for i in [1..3] do
        let resourceName = $"\tinner resource {i}"
        use temp = makeResource resourceName
        printfn $"\tdo something with {resourceName}"

    use r2 = makeResource "second resource"
    printfn "using second resource"
    printfn "done"


(*
sing first resource
	do something with 	inner resource 1
	inner resource 1 is disposed
	do something with 	inner resource 2
	inner resource 2 is disposed
	do something with 	inner resource 3
	inner resource 3 is disposed
using second resource
done
second resource is disposed
first resource is disposed
val useAndDisposeResources: unit = ()
*)

// Creating on the fly

#load "Interfaces.fsx"

let makeAdder id = {
    new IAddingService with
    member this.Add x y =
        printfn $"Adder{id} is adding"
        let result = x + y
        printfn $"{x} + {y} = {result}"
        result
}

let testAdders =
    for i in [1..3] do
        let adder = makeAdder i
        let _ = adder.Add i i
        () // ignore result
