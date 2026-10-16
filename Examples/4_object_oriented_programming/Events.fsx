(* F# - Events *)

(* The Event Class and Event Module

Control.Event<'T> Class

*)


(* Creating Events *)

(* Example *)

type Worker(name: string, shift: string) =
    let mutable _name = name
    let mutable _shift = shift

    let nameChanged = Event<unit>()  (* creates event *)
    let shiftChanged = Event<unit>() (* creates event *)

    member this.NameChanged = nameChanged.Publish  (* exposed event handler *)
    member this.ShiftChanged = shiftChanged.Publish (* exposed event handler *)

    member this.Name
        with get() = _name
        and set value =
            _name <- value
            nameChanged.Trigger() (* invokes event handler *)

    member this.Shift
        with get() = _shift
        and set value =
            _shift <- value
            shiftChanged.Trigger() (* invokes event handler *)


let wk = Worker("Wilson", "Evening")

wk.NameChanged.Add(fun () -> printfn $"Worker changed name! New name: %s{wk.Name}")
wk.Name <- "William"

wk.NameChanged.Add(fun () -> printfn "-- Another handler attached to NameChanged!")
wk.Name <- "Bill"

wk.ShiftChanged.Add(fun () -> printfn $"Worker changed shift! New shift: %s{wk.Shift}")
wk.Shift <- "Morning"

wk.ShiftChanged.Add(fun () -> printfn "-- Another handler attached to ShiftChanged!")
wk.Shift <- "Night"
