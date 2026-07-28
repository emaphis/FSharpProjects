// Tour of F#

module BasicFunctions =

    /// You use `let` to define a function. This one accepts an integer arguement and
    let sampleFunction x = x * x

    let result1 = sampleFunction 4573

    printf $"The result of squaring the integer 4573 and 3 is %d{result1}"

    /// When needed, annotate the type of a parameter using `(argument:type)`.abs
    let sampleFunction2 (x: int) = 2*x*x - x/5 + 3

    let result2 = sampleFunction2 (7 + 4)
    printfn $"The result of appllying the 2nd sample function to (7 + 4) is %d{result2}"


    // If/then/elif/else

    let sampleFunction3 x =
        if x < 100.0 then
            2.0*x*x - x/5.0 + 3.0
        else
            2.0*x*x - x/5.0 - 37.0

    let result3 = sampleFunction3 (6.5 + 4.5)

    printfn $"The result of applying the 3rd sample function to (6.5 + 4.5) is %f{result3}"


module Immutability =

    let number = 2

    let mutable otherNumber = 2

    printfn $"`otherNumber` is {otherNumber}"
