module ActivePatterns

// Active Patterns

(*
// Does not compile
let containsVowel (word: string) =
    let letters = word :> seq<char>
    match letters with
    |  ContainsAny ['a'; 'e'; 'i'; 'o'; 'u']
        -> true
    | SometimesContains [ 'y' ]
        -> true
    | _ -> false
  *)


let containsVowel' (word : string) =
    let letters = word |> Set.ofSeq
    match letters with
    | _ when letters.Contains('a') || letters.Contains('e') ||
             letters.Contains('i') || letters.Contains('o') ||
             letters.Contains('u') || letters.Contains('y')
        -> true
    | _ -> false


module SingleCaseActivePatterns =

        // `(| |)`

        // Example 7-1. Single-case active pattern
        // defines an active pattern for converting a file path into its extension. This
        // allows you to pattern-match against the file extension, without needing to resort to using
        // a when guard.

        open System.IO

        /// Convert a file path into its extension
        let (|FileExtension|) (filePath: string) = Path.GetExtension(filePath)

        let determineFileType (filePath: string) =
            match filePath with

            // Without active patterns
            | filePath when Path.GetExtension(filePath) = ".txt"
                -> printfn "it is a text file"

            // Converting the data using an active pattern
            | FileExtension ".jpg"
            | FileExtension ".png"
            | FileExtension ".gif"
                -> printfn "It is an image file."

            // Binding an unknown balue
            | FileExtension ext
                -> printfn $"Unknown file extension [%s{ext}]"


module PartialActivePatterns =

    // Useful for when input doesn't have a one for one correspondence with output

    open System

    /// Active pattern for converting strings to ints
    let (|ToInt'|) x = Int32.Parse(x)

    /// Check if the input string parses as the number 4
    let isFour str =
        match str with
        | ToInt' 4 -> true
        | _  -> false


    // Test
    let bool1 = isFour "  4 "
    //val bool1: bool = true

    //let bool2 = isFour "not a valid integer"
    //System.FormatException: The input string 'not a valid integer' was not in a correct format.


    // Example 7-2. Partial active patterns in action

    /// Partial active pattern for converting strings to booleans
    let (|ToBool|_|) x =
        let success, result = Boolean.TryParse(x: string)
        if success then
            Some result
        else
            None

    /// Partial active pattern for converting strings to ints
    let (|ToInt|_|) x =
        let success, result = Int32.TryParse(x: string)
        if success then
            Some(result)
        else
            None

    /// Partial active pattern for converting strings to floats
    let (|ToFloat|_|) x =
         let success, result = Double.TryParse(x: string)
         if success then
            Some(result)
         else
            None


    let describeString str =
        match str with
        | ToBool b -> printfn $"{str} is a bool with a value of %b{b}"
        | ToInt i -> printfn $"{str} is an int with a value of %d{i}"
        | ToFloat f -> printfn $"{str} is a float with a value of %f{f}"
        | _         -> printfn $"{str} is not a bool int, or float"


    do describeString " 3.141 "
    do describeString "Not a valid integer"
    do describeString "  false "

    // 3.141  is a float with a value of 3.141000
    // Not a valid integer is not a bool int, or float
    // false  is a bool with a value of false


module ParameterizedActivePatterns =

    // Example 7-3. Parameterized active patterns for regular expression matching

    open System
    open System.Text.RegularExpressions

    /// Use a regular expressifion to capture three groups
    let (|RegexMatch3|_|) (pattern : string) (input : string) =
        let result = Regex.Match(input, pattern)
        if result.Success then
            match (List.tail [ for g in result.Groups -> g.Value ]) with
            | fst :: snd :: [ trd ]
                -> Some (fst, snd, trd)
            | [] -> failwith <| "Match succeeded, but no groups found.\n" +
                                "Use '(.*)' to capture groups"
            | _  -> failwith "Match succeeded, but did not find exactly three groups."
        else
            None


    let parseTime input =
        match input with
        // Match input of the form "6/20/2008"
        | RegexMatch3 "(\d+)/(\d+)/(\d\d\d\d)" (month, day, year)
        // Match input of the form "2004-12-8
        | RegexMatch3  "(\d\d\d\d)-(\d+)-(\d+)" (year, month, day)
            -> Some (DateTime(int year, int month, int day))
        | _ -> None


    let dt1 = parseTime  "1996-3-15"
    let dt2 = parseTime "Some Junk"
