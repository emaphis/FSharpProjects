// Interactive f#

1+2

System.DateTime.Now

let version = 9


let out = $"F#{version} is cool!"

do printfn "%s" out

let greetPerson name age =
    $"Hello, {name}, You are {age} years old."

let out2 =  greetPerson "Fred" 21

do printfn "%s" out2
