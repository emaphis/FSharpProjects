# Object oriented programming in FSharp

A side project to experiment with OO in FSharp

Examples:

<https://fsharpforfunandprofit.com/series/object-oriented-programming-in-fsharp/>

F# is considered a functional first language but it runs on the DotNet CLR environment so it is deeply a OO language to.

## Some rules

### Declaring and defining a class

Declare a class in F# with the `type` keyword as other types

In lightweight syntax (default) the type needs at least one member

Everything is derived from System.Object

Empty class

```CSharp
class EmptyClass
{
}
```

```FSharp
type EmptyClass() =
    class
    end
```

Class with one member

```CSharp
class C
{
    private int field
}
```

```FSharp
type F() =
    int filed = 0
```

### Encapsulation

`let` inside of class is private.

Everything up until th4 first member definition is part of the primary constructor

If you want to do something that does not five a result (returns unit) use `do`

`let` adn`do` bindings must come before member or interface definitions in a type

### Member functions

```CSharp
class C
{
    private int field
    public int GetField()
    {
        return filed;
    }
}
```

``` FSharp
type F() =
    int filed = 0
    member x.GetField() = field
```

Members typically make up the public interface for a type, so by default they are public

`x` is a typical variable for the `this` but in F#, you can assign any name.

### Constructor parameters

```FSharp
type Vehicle(model: string) =
    let model = model
    member x.GetModel() = model
```

The initialization block (constructor) should set a state that satisfies the type invariant

It does not cause any problem that there are 2 expressions with the same name

### Functional languages and OOP

Too philosophies:

- A. Functional: Do not modify the object
- B.  OOP: change the state of the object
  
#### A. Functional style object update

```FSharp
type Vehicle(model: string) =
    let model = model
    member x.GetModel() = model
    member x.SetModel(m: string) = Vehicle(m)
```

In the functional style you don't change the object. (you return a new object)

This allows you to access the previous object and new values at the same time

#### B. OOP style object update

```FSharp
type Vehicle(model: string) =
    let mutable model = model
    member x.GetModel() = model
    member x.SetModel(m: string) = model <- m
```

This might be the expected behavior (non F# libraries)

### Defining properties

```CSharp
class C
{
    private int Field
    public int Field
    {
        get { return filed; }
        set { filed = value; }
    }
}

// Or ...

class C
{
    public int Field { get; set;}  // automatic property
}
```

```FSharp
type Vehicle(model string) =
    let mutable model = model
    member x.Model
        with get() = model
        and set(value) = model <- value

// Or ...

type f() =
    member val Field = 0 with get, set  // automatic property
```

## Inheritance

```CSharp
class Vehicle
{
    public string Type { get; private set; }
    public Vehicle(string Type) { Type = type; }
}

class Car : Vehicle
{
    public Car() : base("Car") { }
}
```

```FSharp
type Vehicle(t) =
    member val Type = t with get

type Car() =
    inherit Vehicle("Car")
```

## Casting

F# differentiates between casting direction in operations

- Up-casting        :>
- Down-casting      :?>
  
### Up-casting

```FSharp
let main argv =
    let c = Car()
    let v = c :> Vehicle
```

- Safe
- Can always be verified by the compiler
  
### Down-casting

```FSharp
type Motorcycle() =
    inherit Vehicle("Motorcycle")

let main argv =
    let c = Car()
    let v = c :> Vehicle
    let m = v :?> Motorcycle
```

- Unsafe
- Correct behavior cannot be enforced by the compiler
- Can result in InvalidCastException

### Type checking

```CSharp
Vehicle v = new Car();
Console.WriteLine ("is motorcycle? (0)", v is Motorcycle)
Console.WriteLine ("is car? (0)", v is Car)
```

```FSharp
let c = Car("Ford")
let v = c :> Vehicle
printfn "Is motorcycle? %b" (b :? Motorcycle)
printfn "Is car? %b" (v :? Car)
```

## Abstract methods

```CSharp
abstract class Vehicle
{
    public string Type
    {
        get; private set;
    }
    public Vehicle(string type)
    {
        Type = type;
    }
    public abstract void Start();
}
```

```FSharp
[<AbstractClass>]
type Vehicle(t) =
    member val Type = t with get, set
    abstract Start: unit -> unite

type Car() =
    inherit Vehicle("Car")
    override x.Start() =
        printfn "Car is starting"
```

## Virtual methods

Technically virtual methods are

- Abstract methods that can be overridden
- With a default implementation

```FSharp
type Vehicle(t) =
    member val Type = t with get, set
    abstract Start: unit -> unite
    default x.Start() =
        printfn "Vehicle is starting
```

## Accessing base methods

```FSharp
type Car() =
    inherit Vehicle("Car")
    override x.Start() =
        base.Start()
        printfn "It is a %s" x.Type
```

No surprises here, exactly the same as in C#

## Defining additional constructors

```FSharp
type Car() =
    inherit Vehicle("Car")
    new() = Car("Ope")
    new(model, make) =
        Car(model + " " + make.ToString())
```

Technically you cannot make more specific constructors than the primary, because they need to call the primary constructor

Static methods can be used to construct objects with non-compatible parameters (factory methods)

More generic constructors can be defined with the new keyword

## Indexers

```CSharp
private int[] _values = { 1, 2, 3 };
public int this[int index]
{
    get { return _values[index]}
    set { _values[index] = value; }
}
```

```FSharp
type Motorcycle() =
    inherit Vehicle("M")
    let mutable _Values = [| 1; 2; 3|]
    member x.Item
        with get index = _values[index]
        and set index value = _values[index] <- value
```

Be aware that this is working on the item collection

(only indexers on the item are used easily from other .Net languages)

## Static methods as constructors

```FSharp
type Motorcycle() =
    inherit Vehicle("M")
    static member makeChildMotorcycle() =
        let m = Motorcycle("Child")
        // Additional code goes here
        m
```

## Interfaces

```FSharp
type IPublicTransportVehicle =
    abstract isDoorOpen: bool with get, set

type Train() =
    inherit Vehicle("Big Chief")
    let mutable isDoorOpen = false
    interface IPublicTransportVehicle with
        member x.IsDoorOpen
            with get() = isDoorOpen
            and  set v =
                isDoorOpen <- v
                printfn "is door open? %f" is DoorOpen
```

Basically just classes with abstract methods

Without other tricks, object needs to be casted to interface before accessing it

## Object expressions

Creates anonymous types with less code

```FSharp
let airplane = {
    new Vehicle("Airplane")
    with member x.ToString() = "Hey I'm and Airplane"
}
```
