// Type Holes?

// Let's see:
let ___<'a> = failwith "not yet implemented"

let add x y =
    x + y

let result = add 10 ___


//printf "Result: %d" result

// Just a quick note. Typed holes aren't implemented in F# yet.
// But here is an experimental implementation on GitHub:
//  https://github.com/ionide/FSharp.EventHorizon

// And here is a language suggestion issue:
//   https://github.com/fsharp/fslang-suggestions/issues/1275
