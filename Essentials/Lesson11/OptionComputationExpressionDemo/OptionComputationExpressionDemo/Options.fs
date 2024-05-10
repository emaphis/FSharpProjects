namespace ComputationExpressions

[<AutoOpen>]
module Option =

    type OptionBuilder() =
        
        // Supports let!
        member _.Bind(x, f) =
            Option.bind f x
        
        // Supports return
        member _.Return(x) = Some x
        
        // Supports return!
        member _.ReturnFrom(x) = x

    // Computation Expression for Option 
    // Usage will be option {...}
    let option = OptionBuilder()
