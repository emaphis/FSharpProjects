// 2.5 Declare the F# function occInString: string * char -> int where
//
//      occInString(str, ch) = the number of occurrences of character ch
//                             in the string str

// from ex 2.3
let isIthChar (str: string, i, ch) = str.[i] = ch

let occInString(str: string, ch) =
    let rec loop (str: string, ch, idx, count) =
        if idx >= str.Length then count
        else
            if isIthChar (str, idx, ch)
            then loop (str, ch, idx+1, count+1)
            else loop (str, ch, idx+1, count)
    loop (str, ch, 0, 0)

// testing
0 = occInString ("", 'a')
1 = occInString ("a", 'a')
0 = occInString ("a", 'b')
0 = occInString ("aaa", 'b')
3 = occInString ("aaa", 'a')
2 = occInString ("aab", 'a')
1 = occInString ("cab", 'a')
0 = occInString ("cdb", 'a')
