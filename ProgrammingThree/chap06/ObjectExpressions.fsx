module ObjectExpressions

    // Anonymous classes
    // { new "name" expression }

module ObjectExpressionsForInterfaces =

    // Example 6-3. Sorting a list using IComparer<a>
    open System.Collections.Generic

    type Person =
        {
            First : string
            Last  : string
        }
        override this.ToString () = $"{this.Last}, {this.First}"

    let people =
        List<_>(
            [|
                { First = "Jomo";  Last = "Fisher" }
                { First = "Brian"; Last = "McNamara" }
                { First = "Joe";   Last = "Pamer" }
            |] )

    let printPeople () =
        Seq.iter (fun person -> printfn $"\t{person.ToString()}") people

    // Now sort by last name
    printfn "Initial ordering"
    printPeople()

    // Sort by first name
    people.Sort(
        {
            new IComparer<Person> with
                member this.Compare(l, r) =
                    if   l.First > r.First then  1
                    elif l.First = r.First then  0
                    else                        -1
        } )

    printfn "After sorting by first name:"
    printPeople()


module ObjectExpressionsForDerivedClasses =

    // Example 6-4. Object expressions for creating derived classes
    [<AbstractClass>]
    type Sandwich() =
        abstract Ingredients : string list
        abstract Calories : int


    // Object expression for a derived class
    let lunch =
        {
            new Sandwich() with
                member this.Ingredients = [ "Peanut butter"; "Jelly"]
                member this.Calories = 465
        }


    let lst1 = lunch.Ingredients
    // val lst1: string list = ["Peanut butter"; "Jelly"]
