// Example 5-15 Example .fsi and .fs files
namespace MyProject.Utilities

type internal MyClass =
    new : unit -> MyClass
    member public Property1 : int
    member private Method1 : int * int -> int
