// Object-Oriented Programming
// Interfaces


// An object's interface refers to all the public members and functions that
// a function exposes to consumers of the object

open System

type Monkey'(name: string, birthday: DateTime) =
    let mutable _birthday = birthday
    let mutable _lastEaten = DateTime.Now
    let mutable _foodsEaten = []: string list

    member this.Speak() = printfn "Ook ook!"
    member this.Name = name
    member this.BirthDay
        with get() = birthday
        and set value = _birthday <- value

    member internal this.UpdateFoodsEaten food = _foodsEaten <- food :: _foodsEaten
    member internal this.ResetLastEaten() = _lastEaten <- DateTime.Now

    member this.IsHungry = (DateTime.Now - _lastEaten).TotalSeconds >= 5.0
    member this.GetFoodsEaten() = _lastEaten
    member this.Feed food =
        this.UpdateFoodsEaten food
        this.ResetLastEaten()
        this.Speak()


// publicly accessible methods

(*
type Monkey =
  new: name: string * birthday: System.DateTime -> Monkey
  member Feed: food: string -> unit
  member GetFoodsEaten: unit -> System.DateTime
  member internal ResetLastEaten: unit -> unit
  member Speak: unit -> unit
  member internal UpdateFoodsEaten: food: string -> unit
  member BirthDay: System.DateTime with get, set
  member IsHungry: bool
  member Name: string
*)


// Defining Interfaces

(*
type type-name =
   interface
       inherits-decl
       member-defns
   end
*)

// example

type ILifeForm =
    abstract Name : string
    abstract Speak : unit -> unit
    abstract Eat : unit -> unit


// Using Interfaces

type Dog(name: string, age: int) =
    member this.Age = age

    interface ILifeForm with
        member this.Name = name
        member this.Speak (): unit = printfn "Woof!"
        member this.Eat() = printfn "Yumm, doggy biscuits!"


type Monkey(weight: float) =
    let mutable _weight = weight

    member this.Weight
        with get() = weight
        and set value = _weight <- value

    interface ILifeForm with
        member this.Name = "Monkey!!!"
        member this.Speak(): unit = printfn "Ook ook"
        member this.Eat() = printfn "Bananas!"

type Ninja() =
    interface ILifeForm with
        member this.Name = "Ninjas have no name"
        member this.Speak() = printfn "Ninjas are silent, deadly killers"
        member this.Eat() =
            printfn "Ninjas don't eat, they wail on guitars because they're totally sweet"



let letsEat (lifeForm: ILifeForm) = lifeForm.Eat()

let myDog = Dog("Fido", 10)

letsEat myDog
//> Yumm, doggy biscuits!


// Implementing Interfaces with Object Expressions

(*
{ new ty0 [ args-expr ] [ as base-ident ] [ with
      val-or-member-defns end ]

  interface ty1 with [
      val-or-member-defns1
   end ]

  …

  interface tyn with [
      val-or-member-defns
  end ] }
*)

// open System
open System.Collections.Generic

type person = { name: string; age: int }

let people =
    [|{ name = "Larry"; age = 20 };
      { name = "Moe"; age = 30 };
      { name = "Curly"; age = 25 } |]


let sortAndPrint msg items (comparer: IComparer<person>) =
    Array.Sort(items, comparer)
    printfn $"%s{msg}"
    Seq.iter (fun per -> printf $"({per.name}, {per.age})") items
    printfn ""


(* sort by age *)
sortAndPrint "age" people  { new IComparer<person> with member this.Compare(x, y) = x.age.CompareTo y.age }
//(Larry, 20)(Curly, 25)(Moe, 30)

(* sort by name *)
sortAndPrint "name" people { new IComparer<person> with member this.Compare(x, y) = x.name.CompareTo y.name }
//(Curly, 25)(Larry, 20)(Moe, 30)

(* sorting by name descending *)
sortAndPrint "name desc" people { new IComparer<person> with member this.Compare(x, y) = y.name.CompareTo x.name }
//(Moe, 30)(Larry, 20)(Curly, 25)



// Implementing Multiple Interfaces

//open System

type Person(name: string, age: int) =
    member this.Name = name
    member this.Age = age

    (* IComparable is used for ordering instances *)
    interface IComparable<Person> with
        member this.CompareTo other =
            match this.Name.CompareTo other.Name with
                | 0 -> this.Age.CompareTo other.Age
                | n -> n

    (* Used for comparing this type against other types *)
    interface IEquatable<string> with
        member this.Equals otherName = this.Name.Equals otherName


// Interface Hierarchies

type Point = {
    X: int
    Y: int
}

type ILifeForm' =
    abstract member location : Point

type 'a IAnimal =
    inherit ILifeForm'
    inherit System.IComparable<'a>
    abstract member speak: unit -> unit

type IFeline =
    inherit IAnimal<IFeline>
    abstract member purr: unit -> unit


// Examples

// Generalizing a function to many classes

let lifeforms =
    [ Dog("Fido", 7) :> ILifeForm
      Monkey 500.0 :> ILifeForm
      Ninja() :> ILifeForm ]

let handleLifeForms (x: ILifeForm) =
    printfn $"Handling lifeform '%s{x.Name}'"
    x.Speak()
    x.Eat()
    printfn ""


let main1() =
    printfn "Processing .. \n"
    lifeforms |> Seq.iter handleLifeForms
    printfn "Done"


main1()


// Using interfaces in generic type definitions

// open System

type tree<'a> when 'a :> IComparable<'a> =
    | Nil
    | Node of 'a * 'a tree * 'a tree

let rec insert (x : #IComparable<_>) = function
    | Nil -> Node(x, Nil, Nil)
    | Node(y, l, r) as node ->
        if x.CompareTo y = 0 then node
        elif x.CompareTo y = -1 then Node(y, insert x l, r)
        else Node(y, l, insert x r)

let rec contains (x : #IComparable<'a>) = function
    | Nil -> false
    | Node(y, l, r) ->
        if x.CompareTo y = 0 then true
        elif x.CompareTo y = -1 then contains x l
        else contains x r


let x =
    let rnd = Random()
    [ for _ in 1 .. 10 -> rnd.Next(1, 100) ]
    |> Seq.fold (fun acc x -> insert x acc) Nil

// val x: tree<int> =
//  Node
//    (52, Node (3, Nil, Node (9, Nil, Node (47, Node (41, Nil, Nil), Nil))),
//     Node (53, Nil, Node (85, Node (77, Nil, Node (78, Nil, Nil)), Nil)))

let bool1 = contains 39 x

let bool2 = contains 78 x


// Simple dependency injection

type Emailer() =
    member this.SendMsg(address: string, msg: string) =
        printfn $"msg sent to: %s{address}, msg = %s{msg}"

type IFailureNotifier =
    abstract Notify : string -> unit

(*
type Processor(notifier: IFailureNotifier) =
    // ...
    member this.Process items =
        try
            // do stuff with items
        with
            | err -> notifier.Notify(err.Message
*)

(* concrete implementations of IFailureNotifier *)

type EmailNotifier() =
    interface IFailureNotifier with
        member this.Notify msg = Emailer().SendMsg("admin@company.com", "Error! " + msg)

type DummyNotifier() =
    interface IFailureNotifier with
        member this.Notify _ = () // swallow message


type LogfileNotifier(filename : string) =
    interface IFailureNotifier with
        member this.Notify msg = System.IO.File.AppendAllText(filename, msg)


// adder example

type IAddStrategy =
    abstract add : int -> int -> int

type DefaultAdder() =
    interface IAddStrategy with
        member this.add x y = x + y

type SlowAdder() =
    interface IAddStrategy with
        member this.add x y =
            let rec loop acc = function
                | 0 -> acc
                | n -> loop (acc + 1) (n - 1)
            loop x y

type OffByOneAdder() =
    interface IAddStrategy with
        member this.add x y = x + y - 1


type SwappableAdder(adder : IAddStrategy) =
    let mutable _adder = adder
    member this.Adder
        with get() = _adder
        and set value = _adder <- value

    member this.Add x y = this.Adder.add x y;;


#time;;

let myAdder = SwappableAdder(DefaultAdder())
// let myAdder = new SwappableAdder(new DefaultAdder())

myAdder.Add 10 1000000000
//Real: 00:00:00.001, CPU: 00:00:00.031, GC gen0: 0, gen1: 0, gen2: 0
//myAdder.Add 10 1000000000

myAdder.Adder <- SlowAdder()

myAdder.Add 10 1000000000
// Real: 00:00:00.230, CPU: 00:00:00.234, GC gen0: 0, gen1: 0, gen2: 0
//val it: int = 1000000010

myAdder.Adder <- SlowAdder()

myAdder.Add 10 1000000000
//Real: 00:00:00.223, CPU: 00:00:00.218, GC gen0: 0, gen1: 0, gen2: 0
//val it: int = 1000000010

myAdder.Adder <- OffByOneAdder()

myAdder.Add 10 1000000000
// Real: 00:00:00.000, CPU: 00:00:00.000, GC gen0: 0, gen1: 0, gen2: 0
// val it: int = 1000000009

#time;;
