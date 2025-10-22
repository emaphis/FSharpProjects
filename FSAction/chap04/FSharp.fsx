// Age description F# script
// See CSharp.csx version

let describeAge age =
    let ageDescription =
        if age < 18 then "Child"
        elif age < 65 then "Adult"
        else "DAP"
    
    let greeting = "Hello"
    
    $"{greeting}! You are a '{ageDescription}'"

let desc = describeAge 45
do printfn "%s" desc
