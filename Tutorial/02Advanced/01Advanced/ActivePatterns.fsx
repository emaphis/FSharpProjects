// Advanced F#
// Active Patterns


// Defining Active Patterns

(*
let (|name1|name2|...|) = ...
*)

let (|Even|Odd|) n =
    if n % 2 = 0 then
        Even
    else
        Odd


// roughly equivalent:

type numKind =
    | Even'
    | Odd'

let get_choice n =
    if n % 2 = 0 then
        Even'
    else
        Odd'


// Union constructors

let (|SeqNode|SeqEmpty|) s =
    if Seq.isEmpty s then SeqEmpty
    else SeqNode (Seq.head s, Seq.skip 1 s)


// equvaletn to:

type SeqWrapper<'a> =
    | SeqEmpty'
    | SeqNode' of 'a * seq<'a>

let get_choice' s =
    if Seq.isEmpty s then SeqEmpty'
    else SeqNode' (Seq.head s, Seq.skip 1 s)


// Using Active Patterns

(*
let (|Even|Odd|) n =
    if n % 2 = 0 then Even
    else Odd
*)


let testNum n =
    match n with
    | Even -> printfn $"{n} is even"
    | Odd  -> printfn $"{n} is odd"


testNum 12
testNum 17


// The parameter in the match clause is always passed as the last argument to the active pattern expression.

(*
let (|SeqNode|SeqEmpty|) s =
    if Seq.isEmpty s then SeqEmpty
    else SeqNode ((Seq.head s), Seq.skip 1 s)
*)

let perfectSquares = seq { for a in 1 ..10 -> a * a }

let rec printSeq = function
    | SeqEmpty -> printfn "Done."
    | SeqNode(hd, tl) ->
        printf $"%A{hd} "
        printSeq tl


printSeq perfectSquares
//  1 4 9 16 25 36 49 64 81 100 Done


// Parameterizing Active Patterns

let (|Contains|) (needle: string) (haystack : string) =
    haystack.Contains needle

let testString = function
    | Contains "kitty" true -> printfn "Text contains 'kitty'"
    | Contains "doggy" true -> printfn "Text contains 'doggy'"
    | _ -> printfn "Text neither contains 'kitty' nor 'doggy'";;

testString "She's fat and purrs a lot :)"
testString "She's a kitty and purrs a lot :)"


// Equivalent to:

type choice =
    | Contains2 of bool

let get_choice2 (needle: string) (haystack: string) =
    Contains2(haystack.Contains needle)

let testString2 n =
    match get_choice2 "kitty" n with
    | Contains2 true -> printfn "Text contains 'kitty'"
    | _ ->
        match get_choice2 "doggy" n with
        | Contains2 true -> printfn "Text contains 'doggy'"
        | _ ->  printfn "Text neither contains 'kitty' nor 'doggy'"

testString2 "She's fat and purrs a lot :)"
testString2 "She's a kitty and purrs a lot :)"


// Partial Active Patterns

let (|RegexContains|_|) pattern input =
    let matches =
        System.Text.RegularExpressions.Regex.Matches(input, pattern)
    if matches.Count > 0 then
        Some [ for m in matches -> m.Value ]
    else None

let testString3 = function
    | RegexContains "http://\S+" urls -> printfn "Got urls: %A" urls
    | RegexContains "[^@]@[^.]+\.\W+" emails -> printfn "Got email address: %A" emails
    | RegexContains "\d+" numbers -> printfn "Got numbers: %A" numbers
    | _ -> printfn "Didn't find anything."


testString3 "867-5309, Jenny are you there?"
// Got numbers: ["867"; "5309"]
