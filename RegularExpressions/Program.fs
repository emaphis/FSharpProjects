module app

open System
open System.IO

let cwd = Directory.GetCurrentDirectory()

printfn "Hello from F#"
printfn $"Current working directory: %s{cwd}"
