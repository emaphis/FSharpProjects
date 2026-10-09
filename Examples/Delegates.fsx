(* F# - Delegates *)

(* Declaring Delegates

type delgate-typename = delgate of type -> type

*)


// Delegate1 works with tuple arguement
type DelegateA = delegate of (int * int) -> int

// Delegate2 works with curried arguments.
type DelegateB = delegate of int * int -> int


(* Example *)

type MyClass() =
    static member add (a: int, b: int) =
        a + b
    static member sub (a: int) (b: int) =
        a - b
    member x.Add(a: int, b: int) =
        a + b
    member x.Sub(a: int) (b: int) =
        a - b

// Delegate1 works with tuple arguements.
type Delegate1 = delegate of (int * int) -> int

// Delegate2 works wtih curried arguemtns
type Delegate2 = delegate of int * int -> int

let InvokeDelegate1 (dlg: Delegate1) (a: int) (b: int) =
    dlg.Invoke(a, b)
let InvokeDelegate2 (dlg: Delegate2) (a: int) (b: int) =
    dlg.Invoke(a, b)

// For static methods, use the class name, the dot operator, and the
// name of the static method.
let del1 : Delegate1 = new Delegate1( MyClass.add )
let del2 : Delegate2 = new Delegate2( MyClass.sub )
let mc = MyClass()

// For instance methods, use the instance value name, the dot operator,
// and the instance method name.

let del3 : Delegate1 = new Delegate1( mc.Add )
let del4 : Delegate2 = new Delegate2( mc.Sub )

for a, b in [ 400, 200; 100, 45 ] do
    let c1 = InvokeDelegate1 del1 a b
    printfn $"%d{a} + %d{b} = %d{c1}"
    let c2 = InvokeDelegate2 del2 a b
    printfn $"%d{a} - %d{b} = %d{c2}"
    let c3 = InvokeDelegate1 del3 a b
    printfn $"%d{a} + %d{b} = %d{c3}"
    let c4 = InvokeDelegate2 del4 a b
    printfn $"%d{a} - %d{b} = %d{c4}"
