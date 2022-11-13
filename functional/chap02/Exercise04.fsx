// 2.4 Declare the F# function occFromIth: string * int * char -> int where
//
//      occFromIth(str, i, ch) = the number of occurrences of character ch
//                               in positions j in the string str with j ≥ i.
//
// Hint: the value should be 0 for i ≥ size str

// from ex 2.3
let isIthChar (str: string, i, ch) = str.[i] = ch

let rec occFromIth(str: string, i, ch) =
    let rec loop(str, idx, ch, count) =
        if idx >= i then count
        else 
            if isIthChar (str, idx, ch)
            then loop(str, idx+1, ch, count+1)
            else loop(str, idx+1, ch, count)
    if i >= str.Length then 0
    else loop(str, 0, ch, 0)

// testing
0 = occFromIth("aaaa", 3, 'b')
3 = occFromIth("aaaa", 3, 'a')
2 = occFromIth("aaaa", 2, 'a')
2 = occFromIth("abab", 3, 'a')
