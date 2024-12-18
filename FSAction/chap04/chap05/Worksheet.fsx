// Chapter 05 - Shaping data
//  The F# type system
//  Tuples
//  Records
//  Anonymous Records


// 5.1 Composing types in F#
// 5.1.1 Data and functions in F#

// 5.2 Tuples
// 5.2.1 Tuple basics

let name1 = "issac", "abraham"
// val name1: string * string = ("issac", "abraham")
let firstName1, secondName1 = name1
// val secondName1: string = "abraham"
// val firstName1: string = "issac"

//  5.2.2 More on tuples

// Type Signatures

// name1: string * string

let name2 = "issac", "abraham", 42, "london"
// name2: string * string * int * string

// Wildcards

let nameAndAge = "Jane", "Smith", 25
let forename, surname, _ = nameAndAge
//val surname: string = "Smith"
//val forename: string = "Jane"

// Type Inference

// Listing 5.1 Type inference of a function taking a tuple as input
let makeDoctor name =
    let _, sname = name
    "Dr.", sname
// val makeDoctor: 'a * 'b -> string * 'b

let doctor1 = makeDoctor name1
// val doctor1: string * string = ("Dr.", "abraham")

// Inline Deconstruction

let makeDoctor1 (_, sname) =
    "Dr.", sname

// val makeDoctor1: 'a * sname: 'b -> string * 'b

let doctor2 = makeDoctor1 name1

// val doctor2: string * string = ("Dr.", "abraham")


// Nesting

let nameAndAge1 = ("Jeo", "Bloggs"), 28
// val nameAndAge1: (string * string) * int = (("Jeo", "Bloggs"), 28)

let name3, age3 = nameAndAge1
// val name3: string * string = ("Jeo", "Bloggs")
// val age3: int = 28

let (forename1, surname1), theAge1 = nameAndAge1
// val theAge1: int = 28
// val surname1: string = "Bloggs"
// val forename1: string = "Jeo"


// Tuple and Value Tuple


let tuple = System.Tuple.Create("str", 3)
// val tuple: string * int = ("str", 3)

// ValueTuple

let makeDoctor2 (name: struct (string * string)) =
    let struct(_, sname) = name
    struct ("Dr.", sname)
// val makeDoctor2: name: struct (string * string) -> struct (string * string)

let doctor3 = makeDoctor2 struct("isaac", "abraham")
// val doctor3: struct (string * string) = struct ("Dr.", "abraham")


// Out Parameters in F#

open System

let parsed, theDate = DateTime.TryParse "03 Dec 2020"
if parsed then printfn $"Day of week is {theDate.DayOfWeek}"


// 5.2.3 Costs and benefits of tuples


// 5.3 Records

// 5.3.1 Defining, creating, and consuming records

type Person1 =
    {
        FirstName : string
        LastName : string
        Age : int
    }

let isaac1 =
    {
        FirstName = "Isaac"
        LastName = "Abraham"
        Age = 42
    }

let fullName1 = $"{isaac1.FirstName} {isaac1.LastName}"
// val fullName1: string = "Isaac Abraham

// Listing 5.2 Constructing a nested record in F#

type Address2 =
    {
        Line1 : string
        Line2 : string
        Town : string
        Country : string
    }

type Person2 =
    {
        Name : string * string
        Address : Address2
    }


// 5.3.2 More on records

// Type Inference

// Listing 5.3 Using type inference for records within a function

let generatePerson1 theAddress =
    if theAddress.Country = "UK" then
        {
            Name = "Isaac", "Abraham"
            Address = theAddress
        }
    else
        {
            Name = "John", "Doe"
            Address = theAddress
        }
// val generatePerson1: theAddress: Address2 -> Person2




