// Chapter 05 - Shaping data
//  The F# type system
//  Tuples
//  Records
//  Anonymous Records


// 5.1 Composing types in F#
// 5.1.1 Data and functions in F#

// 5.2 Tuples
// 5.2.1 Tuple basics

// Constructs a tuple
let name1 = "issac", "abraham"
// val name1: string * string = ("issac", "abraham")

// Deconstructs a tuple
let firstName1, secondName1 = name1
// val secondName1: string = "abraham"
// val firstName1: string = "issac"

//  5.2.2 More on tuples

// Type Signatures

// Type1 * Type2 ... TypeN

let name2 = "issac", "abraham", 42, "london"
// name2: string * string * int * string


// Wildcards

let nameAndAge = "Jane", "Smith", 25
//val nameAndAge: string * string * int = ("Jane", "Smith", 25)

// _ discards thrid item
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


// Inline Deconstruction of tupples in parameter list

let makeDoctor1 (_, sname) =
    "Dr.", sname

// val makeDoctor1: 'a * sname: 'b -> string * 'b

let doctor2 = makeDoctor1 name1

// val doctor2: string * string = ("Dr.", "abraham")


// Nesting
// Nesting tupples or groups of tupples

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

let tuple1 = System.Tuple.Create("str", 3)
// val tuple1: string * int = ("str", 3)

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
//val theDate: DateTime = 12/3/2020 12:00:00 AM
//val parsed: bool = true

if parsed then printfn $"Day of week is {theDate.DayOfWeek}"
//Day of week is Thursday
//val it: unit = ()


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
        Age : int
    }

let person3 =
    {
        Name = "Isaac", "Asimov"
        Address =
            {
                Line1 = "10 Abc Street"
                Line2 = "Apt AA"
                Town = "London"
                Country = "UK"
            }
        Age = 38
    }


    // 5.3.2 More on records

// Type Inference

// Listing 5.3 Using type inference for records within a function

let generatePerson1 theAddress =
    if theAddress.Country = "UK" then
        {
            Name = "Isaac", "Abraham"
            Address = theAddress
            Age = 38
        }
    else
        {
            Name = "John", "Doe"
            Address = theAddress
            Age = 25
        }
// val generatePerson1: theAddress: Address2 -> Person2

let person4 =
     generatePerson1  
        {
                Line1 = "10 Abc Street"
                Line2 = "Apt AA"
                Town = "London"
                Country = "UK"
            }


// Copy and Update

// Listing 5.5 Using copy-and-update syntax to clone a record

// with keyword

let theAddress =
    {
        Line1 = "1st Street"
        Line2 = "Apt. 1"
        Town = "London"
        Country = "UK"
    }

let addressInDE =
    {
        theAddress with
            Town = "Berlin"
            Country = "DE"
    }


// Equlaity Checking - structural equality

//  Listing 5.6 Using explicit type annotations when
//  creating a record valu

let isaac =
    {
        Name = "Issac", "Abraham"
        Address = theAddress
        Age = 35
    }

let isaac2 =
    {
        Name = "Issac", "Abraham"
        Address = theAddress
        Age = 35
    }

let areTheyTheSame = (isaac = isaac2)
// val areTheyTheSame: bool = true


// 5.3.3 Records and .NET

// Records compile down to classes.

// C# record

[<Struct>]
type Address3 =
    {
        Line1 : string
    }


// Listing 5.7 Modeling similar types through
// composition
// See Exercise 5.3

type Address = { Street : string; Country : string }
type Name = { Forename : string; Surname : string }

type Customer =
    {
        Name : Name
        Address : Address2
        CreditRating : int
    }

type Supplier =
    {
        Name : string
        Address : Address2
        Balance : decimal
        NextDateDue  : DateTime
    }


// 5.4 Anonymous records
// defined as needed doesn't have aname

let company =
    {|
        Name = "Ny Company Inc."
        Town = "The Townn"
        Country = "USA"
        TaxNumber = 123456
    |}
