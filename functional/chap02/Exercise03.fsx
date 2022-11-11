// 2.3 Declare the F# function isIthChar: string * int * char -> bool where the value of
//     isIthChar(str, i, ch) is true if and only if ch is the i’th character in the string str (numbering
//     starting at zero)

let isIthChar (str: string) ith char =
    str.[ith] = char

true = isIthChar "a" 0 'a'
true = isIthChar "abc" 0 'a'
false = isIthChar "abc" 1 'a'
