// .2 Declare an F# function pow: string * int -> string, where:
//          pow(s, n) = s · s ···· · s 
//                          n
//    where we use · to denote string concatenation. (The F# representation is +.)

let rec pow = function
    | (s, 0) -> ""
    | (s, n) -> s + pow(s, n-1)

"a" = pow("a", 1)
"aaa" = pow("a", 3)
"" = pow("abc", 0)
"abcabc" = pow("abc", 2)
"" = pow("", 1)
"" = pow("", 3)
