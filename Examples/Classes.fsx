(* F# - Classes *)

(* Syntax

// Class definition:
type [access-modifier] type-name [type-params] [access-modifier] ( parameter-list ) [ as identifier ] =
   [ class ]
      [ inherit base-type-name(base-constructor-args) ]
      [ let-bindings ]
      [ do-bindings ]
      member-list
      ...
   [ end ]

// Mutually recursive class definitions:
type [access-modifier] type-name1 ...
and [access-modifier] type-name2 ...
...

*)

(* Constructor of a Class

  new (argument-list) = constructor-body
*)

(* Example *)

type Line = class
    val X1 : float
    val Y1 : float
    val X2 : float
    val Y2 : float

    new (x1, y1, x2, y2) as this =
        { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;}
        then
         printfn $" Creating Line: {{(%g{this.X1}, %g{this.Y1}), (%g{this.X2}, %g{this.Y2})}}\nLength: %g{this.Length}"

    member x.Length =
        let sqr x = x * x
        sqrt(sqr(x.X1 - x.X2) + sqr(x.Y1 - x.Y2))
end

let aLine = Line(1.0, 1.0, 4.0, 5.0)
aLine.Length


(* Let Bindings

The let bindings in a class definition allow you to define private fields and private functions for F# classes
*)

type Greetings(name) as gr =
    let data = name
    do
        gr.PrintMessage()

    member this.PrintMessage() =
        printfn $"Hello %s{data}\n"

let gtr = Greetings "Zara"
gtr.PrintMessage
