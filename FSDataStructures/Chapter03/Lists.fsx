// Lists - pg 118

// Declare a list of numbers
let numbers = [0; 1; 2; 3; 4; 5; 6; 7; 8; 9]

let yodaQuotesFragment1 = [" Must ";" Unlearn ";" What ";" You "]

// `::` operator
let yodaQuotesFragment2 = "You"::yodaQuotesFragment1
// val yodaQuotesFragment2: string list =  ["You"; " Must "; " Unlearn "; " What "; " You "]

let yodaQuotesFragment3 = [yodaQuotesFragment1[1][3..7] + "ed"]
//val yodaQuotesFragment3: string list = ["learned"]

let yodaQuote = yodaQuotesFragment2 @ yodaQuotesFragment3
// val yodaQuote: string list = ["You"; " Must "; " Unlearn "; " What "; " You "; "learned"]

// Some of the list type operations can be seen in the
// following table:

yodaQuote.IsEmpty
yodaQuote.Length
yodaQuote.Head
yodaQuote.Tail
yodaQuote.Item (1)

//  The zip operation takes two collections and combines them into tuples as follows:
let FirstNames = ["Walter "; "Skyler"; "Jesse"; "Hank"; "Saul" ]
let LastNames = ["White "; "White"; "Pinkman"; "Schrader"; "Goodman" ]

let BreakingBadCast = List.zip FirstNames LastNames
//val BreakingBadCast: (string * string) list =
//  [("Walter ", "White "); ("Skyler", "White"); ("Jesse", "Pinkman");
//   ("Hank", "Schrader"); ("Saul", "Goodman")]

// A list can be iterated as follows:
List.iter (fun x -> printfn "%s" x) FirstNames



// List comprehensions

// In F#, ranges and generators provide syntactic sugar which allows us to perform operations like
// the following code snippet:

[ -100 .. 0 ]
[ 1 .. 10 .. 100 ]
[ 'A' .. 'Z' ]

[ for x in 1 .. 10 do
        yield (x * x * x) ]
// val it: int list = [1; 8; 27; 64; 125; 216; 343; 512; 729; 1000]
