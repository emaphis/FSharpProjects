let toPigLatin (word: string): string =
    let isVowel (c: char) =
        match c with
        | 'a' | 'e' | 'i' | 'o' | 'u'
        | 'A' | 'E' | 'I' | 'O' | 'U' -> true
        | _ -> false

    let out =
        if isVowel word[0] then
            word + "yay"
        else
            word[1..] + string(word[0]) + "ay"

    out


printfn $"""apple = %s{toPigLatin"apple"}"""
printfn $"""banana = %s{toPigLatin "banana"}"""
