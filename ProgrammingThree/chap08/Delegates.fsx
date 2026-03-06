module DelegatesAndEvents
 
module AnExample =

    // Example 8-8. Creating proactive types
    // defines a type CoffeeCup that lets interested parties know 
    // when the cup is empty. 

    open System.Collections.Generic

    [<Measure>]
    type ml

    type CoffeeCup(amount: float<ml>) =
        let mutable m_amountLeft = amount
        let mutable m_interestedParties =
            List<(CoffeeCup -> unit)>()

        member this.Drink(amount) =
            printfn $"Drinking %.1f{float amount}..."
            m_amountLeft <- max (m_amountLeft - amount) 0.0<ml>
            if m_amountLeft <= 0.0<ml> then
                this.LetPeopleKnowI'mEmpty()

        member this.Refill(amountAdded) =
            printfn $"Coffee Cup fefilled wiht %.1f{amountAdded}"
            m_amountLeft <- m_amountLeft + amountAdded

        member this.WhenYou'reEmptyCall(func) =
            m_interestedParties.Add(func)

        member private this.LetPeopleKnowI'mEmpty() =
            printfn "Uh ho, I'm empty! Letting people know."
            for interestedParty in m_interestedParties do
                interestedParty(this)

    // test examples
    let cup = CoffeeCup(100.0<ml>)

    // Notified when the cup i empty
    do cup.WhenYou'reEmptyCall(
        (fun cup -> 
            printfn "Thanks for letting me know..."
            cup.Refill(50.0<ml>)))

    do cup.Drink(75.0<ml>)
    //Drinking 75.0...

    do cup.Drink(75.0<ml>)
    //Drinking 75.0...
    //Uh ho, I'm empty! Letting people know.
    //Thanks for letting me know...
    //Coffee Cup refilled with 50.0


module DefiningDelegates =

    // Define a delegate using syntax:
    // type ident = delegate of type1 -> type2
    // To create a simple delegate that is similar it is to a regular function value. 
    // To instantiate a delegate type, you use the new keyword and pass the 
    //   body of the delegate in as a parameter.
    // To execute a delegate, you must call its `Invoke` method7
    
    // Example 8-9. Defining and using delegates
    
    let functionValue x y =
        printfn $"x = {x}, y = {y}"
        x + y

    // Defining a delegate
    type DelegateType = delegate of int * int -> int

    // Construct a delegate value
    let delegateValue1 =
        new DelegateType (
            fun x y ->
                printfn $"x = {x}, y = {y}"
                x + y
        )

    
    // Calling function values and delegates
    let functionResult = functionValue 1 2

    let delegateResult = delegateValue1.Invoke(1, 3)

    
    // Explicit and implicit calls

    type IntDelegate = delegate of int -> unit
    
    type ListHelper =
        /// Invokes a delegate for every element of a list
        static member ApplyDelegate (l : int list, d : IntDelegate) =
            l |> List.iter (fun x -> d.Invoke(x))
    
    // Explicitly constructing the delegate
    ListHelper.ApplyDelegate([1 .. 10], new IntDelegate(fun x -> printfn "%d" x))
    
    // Implicitly constructing the delegate
    ListHelper.ApplyDelegate([1 .. 10], (fun x -> printfn "%d" x))


module CombiningDelegates =

    // Delegates are coalesced by calling the Combine and Remove static
    // methods on the System.Delegate type,

    // Example 8-10. Combining delegates

    open System.IO

    type LogMessage = delegate of string -> unit

    let printToConsole =
        LogMessage (fun msg -> printfn $"Logging to console %s{msg}...")

    let appendToLogFile =
        LogMessage (fun msg -> 
            printfn $"Logging to file %s{msg}..."
            use file = new StreamWriter("Log.txt", true)
            file.WriteLine(msg))

    let doBoth = LogMessage.Combine(printToConsole, appendToLogFile)

    let typedDoBoth = doBoth :?> LogMessage

    typedDoBoth.Invoke("[some important message]")
    //Logging to console [some important message]...
    //Logging to file [some important message]...
