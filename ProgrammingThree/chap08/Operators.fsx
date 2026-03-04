module Operators

// Learning how to create F# types that integrate better with other object
// oriented languages like C#.

module OperatorOverloading =

    // on primitive types, you can overload those operators to accept custom
    // types you create. Operator overloading in F# can be done by simply
    // adding a static method to a type.

    // Example 8-1. Operator overload
    // s the + and − operators to represent operators on a bottle.

    [<Measure>]
    type ml

    type Bottle(capacity: float<ml>) =

        new() = Bottle 0.0<ml>

        member this.Volume = capacity

        static member (+) (lhs: Bottle, rhs) =
            Bottle(lhs.Volume + rhs)

        static member (-) (lhs: Bottle, rhs) =
            Bottle(lhs.Volume - rhs)

        static member (~-) (rhs: Bottle) =
            Bottle(rhs.Volume * -1.0<1>)

        override this.ToString (): string =
            let fc = float capacity
            sprintf  $"Bottle(%.1f{fc}ml)"


    // Test examples

    let half = Bottle 500.0<ml>

    let _amt1 = half + 500.0<ml>
    //val amt1: Bottle = Bottle(1000.0ml)

    let _amt2 = half - 500.0<ml>
    //val amt2: Bottle = Bottle(0.0ml)

    let _amt3 = -half
    //val amt3: Bottle = Bottle(-500.0ml)


    //  overloaded operators to discriminated unions and record types

    type Person =
        | Boy of string
        | Girl of string
        | Couple of Person * Person

        static member (+) (lhs, rhs) =
            match lhs, rhs with
            | Couple _, _
            | _, Couple _
                -> failwith "Three's a crowd"
            | _, _ -> Couple(lhs, rhs)

    let cpl1 = Boy "Dick" + Girl "Jane"
    //val cpl1: Person = Couple (Boy "Dick", Girl "Jane")


module Indexers =

    // Example 8-2. Adding an indexer to a clas
    // creates a class Year and adds an indexer to allow you to look up the nth
    // day of the year.

    open System

    type Year(year: int) =

        member this.Item (idx: int) =
            if idx < 1 || idx > 365 then
                failwith "Invalid day range"

            let dateStr = sprintf $"1-1-%d{year}"
            //printfn "%s" dateStr
            DateTime.Parse(dateStr).AddDays(float (idx - 1))


     // Using a custom indexer

    let eightyTwo  = Year 1982
    let specialDay = eightyTwo[171]
    //val specialDay: DateTime = 6/20/1982 12:00:00 AM

    let spDay = specialDay.Month, specialDay.Day, specialDay.DayOfWeek
    do printfn $"%A{spDay}"
    //(6, 20, Sunday)


    // Example 8-3. Non-integer indexer
    // defines an indexer for a type that accepts a non-integer parameter. In a
    // different take on the Year class example, the indexer takes a month and
    // date tuple.

    type Year2(year: int) =

        member this.Item (month: string, day: int) =
            let monthIdx =
                match month.ToUpper() with
                | "JANUARY" -> 1  | "FEBRUARY" -> 2  | "MARCH"     -> 3
                | "APRIL"   -> 4  | "MAY"      -> 5  | "JUNE"      -> 6
                | "JULY"    -> 5  | "AUGUST"   -> 8  | "SEPTEMBER" -> 9
                | "OCTOBER" -> 10 | "NOVEMBER" -> 11 | "DECEMBER"  -> 12
                | _ -> failwith $"Invalid month [%s{month}]"

            let dateStr = $"1-1-%d{year}"
            DateTime.Parse(dateStr).AddMonths(monthIdx - 1).AddDays(float (day - 1))


    // `Year2` uses a more intuitive indexer thaT makes the type easier to use:
    let O'seven = Year2 2007
    let randomDay = O'seven["April", 7]

    let day2 =  randomDay.Month, randomDay.Day, randomDay.DayOfWeek
    do printfn $"%A{day2}"
    //(4, 7, Saturday)


    // Example 8-4. Read/write indexer
    // defines a type `WordBuilder` that allows you to access and update letters
    // of a word at a given index.

    open System.Collections.Generic

    type WordBuilder(startingLetters : string) =
        let m_letters = List<char>(startingLetters)

        member this.Item
           with get idx   = m_letters[idx]
           and  set idx c = m_letters[idx] <- c

        member this.Word = new string (m_letters.ToArray())


    // test example
    let wb = WordBuilder "Jurassic Park"

    let word1 = wb.Word
    do printfn $"%s{word1}"

    let chr1 = wb[10]
    do printfn $"%c{chr1}"

    do wb[10] <- 'o'
    do printfn $"%s{wb.Word}"
    //"Jurassic Pork"


module AddingSlices =

    // Example 8-5. Providing a slice to a class

    type TextBlock(text: string) =
        let words = text.Split ' '

        member this.AverageWordLenght =
            words |> Array.map float |> Array.average

        member this.GetSlice(lowerBound: int option, upperBound: int option) =
            let words =
                match lowerBound, upperBound with
                // Specify both upper and lower bound
                | Some lb, Some ub -> words[lb..ub]
                // Just one bound specified
                | Some lb, None  -> words[lb..]
                | None, Some ub  -> words[..ub]
                // No lower or upper bounds
                | None, None -> words[*]
            words


    // test examples
    let text =  "The quick brown fox jumped over the lazy dog"
    let tb = TextBlock text

    let sta1 = tb[..5]
    // [|"The"; "quick"; "brown"; "fox"; "jumped"; "over"|]

    let sta2 = tb[4..7]
    // [|"jumped"; "over"; "the"; "lazy"|]


    // Example 8-6. Defining a two-dimensional slice

    open System

    type DataPoints(points: seq<float * float>) =

        member this.GetSlice(xlb, xub, ylb, yub) =

            let getValue optType defaultValue =
                match optType with
                | Some x -> x
                | None  -> defaultValue

            let minX = getValue xlb Double.MinValue
            let maxX = getValue xub Double.MaxValue

            let minY = getValue ylb Double.MinValue
            let maxY = getValue yub Double.MaxValue


            // Retrun if a given tuple representing a point is within range
            let inRange(x, y) =
                minX < x && x < maxX &&
                minY < y && y < maxY

            Seq.filter inRange points



    // Define 1,000 random points with the valuess betwwen 0.0 to 1.o
    let points =
        seq {
            let rng = Random()
            for _ = 0 to 1000 do
                let x = rng.NextDouble()
                let y = rng.NextDouble()
                yield x, y
    }

    do printfn $"points = %A{points}"

    let d = DataPoints points

    // get all the values where the x and y are greater than 0.5
    let xx = d[0.5 .., 0.5..]
    do printfn $"x = %A{xx}"


module GenericTypeConstraints =

    // Example 8-7. Generic type constraints

    open System
    open System.Collections.Generic


    exception NotGreaterThanHead

    /// Keep a list of items where each item added to it is
    /// greater than the first element of the list.
    type GreaterThanList<'a when 'a :> IComparable<'a> >(minValue: 'a) =
        let m_head = minValue
        let m_list = List<'a>()
        do m_list.Add minValue

        member this.Add(newItem: 'a) =
            // Casting to IComparable wouldn't be possible
            // if 'a weren't constrainted
            let ic = newItem :> IComparable<'a>

            if ic.CompareTo(m_head) >= 0 then
                m_list.Add newItem
            else
                raise NotGreaterThanHead

        member this.Items = m_list :> seq<'a>



    /// Given a type x which contains a constructor and implements IFoo,
    /// returns an instance of that type cast as IFoo.
    let fooFactory<x: 'a when 'a: (new : unit -> 'a) and 'a :> IFoo> () =

        // Creating new instance of 'a because the type constraint enforces
        // that there be a default constructor.
        let clone = new 'a()

        // Comparing x with a new instance, because we know 'a implements IFoo
        let ifoo = x :> IFoo
        ifoo
