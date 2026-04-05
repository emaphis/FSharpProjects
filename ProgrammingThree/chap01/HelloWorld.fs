// HelloWorld.fs

open System

[<EntryPoint>]
let main (args: string[]) =

    if args.Length <> 2 then
        failwith "Error: Expected arguments <greeting> and <thing>"

    let greeting, thing = args[0], args[1]
    let timeOfDay = DateTime.Now.ToString("hh:mm tt")

    printfn $"%s{greeting}, %s{thing} at %s{timeOfDay}"

    0 // Program exit code.
