// Object-Oriented Programming
// Events

// Defining Events

type Person(name: string) =
    let mutable _name = name
    let nameChanged = Event<unit>()

    member this.NameChanged = nameChanged.Publish // exposed event handler

    member this.Name
        with get() = _name
        and set value =
            _name <- value
            nameChanged.Trigger() // invokes event handler


// Adding Callbacks to Event Handlers

(*
val Add : event:('T -> unit) -> unit
  Connect a listener function to the event. The listener will be invoked when
  the event is fired.

val AddHandler : 'del -> unit
  Connect a handler delegate object to the event. A handler can be later removed
  using RemoveHandler. The listener will be invoked when the event is fired.

val RemoveHandler : 'del -> unit
  Remove a listener delegate from an event listener store.
*)

// example

let p = Person "Bob"

// add a handler
p.NameChanged.Add(fun () -> printfn $"--- Name Changed! New name: %s{p.Name}")

p.Name <- "Joe"
// --- Name Changed! New name: Joe

p.Name <- "Moe"
// --- Name Changed! New name: Moe

// add another Handler
p.NameChanged.Add(fun () -> printfn "-- Another handler attached to NameChanged!")

p.Name <- "Bo"
// --- Name Changed! New name: Bo
// -- Another handler attached to NameChanged!


// Working with EventHandlers Explicitly

// Adding and Removing Event Handlers

let p2 = Person "Bob"

let person_NameChanges =
    Handler<unit>(fun sender eventargs -> printfn $"-- Name changes! New name: %s{p2.Name}")

p2.NameChanged.AddHandler person_NameChanges

p2.Name <- "Joe"
//-- Name changes! New name: Joe

p2.Name <- "Moe"
// -- Name changes! New name: Moe

p2.NameChanged.RemoveHandler person_NameChanges
p2.NameChanged.Add(fun () -> printfn "-- Another handler attached to NameChanged!")

p2.Name <- "Bo"
// -- Another handler attached to NameChanged!


// Defining New Delegate Types

type NameChangingEventArgs(oldName: string, newName: string) =
    inherit System.EventArgs()

    member this.OldName = oldName
    member this.NewName = newName

type NameChangingDelegate = delegate of obj * NameChangingEventArgs -> unit
