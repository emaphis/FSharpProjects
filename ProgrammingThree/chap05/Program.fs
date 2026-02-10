// Example 5-15. Example .fsi and .fs files
open MyProject.Utilities

[<EntryPoint>]
let main _ =
    let mClass = MyClass()
    let prop1 = mClass.Property1
    do printfn $"prop1 = %d{prop1}"
    0
