module ActivePatterns

// Example 7-7 requires a reference to System.Xml.dll
#r "System.xml.dll"

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

let bool1 = containsVowel' "xzpdg"
let bool2 = containsVowel' "xrzpdq"

//val bool1: bool = false
//val bool2: bool = false


module SingleCaseActivePatterns =

        // `(| |)`

        // Example 7-1. Single-case active pattern
        // defines an active pattern for converting a file path into its extension. This
        // allows you to pattern-match against the file extension, without needing to resort to using
        // a `when` guard.

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

            // Binding an unknown value
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
    let bool2 = isFour " 6"

    //val bool1: bool =
    //val bool2: bool = false

    //let bool2 = isFour "not a valid integer"
    //System.FormatException: The input string 'not a valid integer' was not in a correct format.


    // Example 7-2. Partial active patterns in action
    // (|xxx|_|)

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
        | ToBool  b -> printfn $"{str} is a bool with a value of %b{b}"
        | ToInt   i -> printfn $"{str} is an int with a value of %d{i}"
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

    /// Use a regular expression to capture three groups
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
        // Match input of the form "2004-12-8"
        | RegexMatch3  "(\d\d\d\d)-(\d+)-(\d+)" (year, month, day)
            -> Some (DateTime(int year, int month, int day))
        | _ -> None


    let dt1 = parseTime  "1996-3-15"
    let dt2 = parseTime "Some Junk"
    //val dt1: DateTime option = Some 3/15/1996 12:00:00 AM
    //val dt2: DateTime option = None


module MulticaseActivePatterns =

    // Example 7-4. Multicase active patterns
    // takes a string and breaks it into a Paragraph, Sentence, Word, or White
    // space categories.

    open System

    /// Active pattern divides all strings into their various meanings.
    let (|Paragraph|Sentence|Word|Whitespace|) (input: string) =
        let input = input.Trim()

        if input = "" then
            Whitespace
        elif input.IndexOf(".") > -1 then
            // Paragraph contains a tuple of sentence counts and sentences
            let sentences = input.Split([|"."|], StringSplitOptions.None)
            Paragraph (sentences.Length, sentences)
        elif input.IndexOf(" ") = -1 then
            // Sentence contains an array of string words
            Sentence (input.Split([|" "|], StringSplitOptions.None))
        else
            // Word contain a string
            Word input


    /// Count the number of letters of a string by breaking it down
    let rec countLetters str =
        match str with
        | Whitespace -> 0
        | Word x -> x.Length
        | Sentence words ->
            words
            |> Array.map countLetters
            |> Array.sum
        | Paragraph (_, sentences) ->
            sentences
            |> Array.map countLetters
            |> Array.sum


module UsingActivePatterns =

    // * Applying active patterns *
    let (|ToUpper|) (input: string) = input.ToUpper()

    let f (ToUpper x) = printfn $"x = {x}"

    do f "this is lower case"
    // x = THIS IS LOWER CASE


    // * Combining active patterns *

    // Example 7-6. Combining active patterns with And

    open System.IO

    let (|EndsWithExtension|_|) (ext: string) (x: string) =
        if x.EndsWith(ext) then Some() else None


    let (|KBInsize|MBInSize|GBInSize|) filePath =
        let file = File.Open(filePath, FileMode.Open)
        if file.Length < 1024L * 1024L then
            KBInsize
        elif file.Length < 1024L * 1024L * 1024L then
            MBInSize
        else
            GBInSize


    let (|IsImageFile|_|) filePath =
        match filePath with
        | EndsWithExtension ".jpg"
        | EndsWithExtension ".bmp"
        | EndsWithExtension ".gif"
            -> Some()
        | _ -> None

    let ImageTooBigForEmail filePath =
        match filePath with
        | IsImageFile & (MBInSize | GBInSize)
            -> true
        | _  -> false


    // * Nesting active patterns *

    // Example 7-7. Nesting active patterns within a match expression

    // This example requires a reference to System.Xml.dll
    open System.Xml

    /// Match an XML element
    let (|Elem|_|) (name: string) (inp: XmlNode) =
        if inp.Name = name then  Some(inp)
        else None

    /// Get the attributes of an element
    let (|Attributes|) (inp: XmlNode) = inp.Attributes

    /// Match a specific attribute
    let (|Attr|) attrName (inp: XmlAttributeCollection) =
        match inp.GetNamedItem(attrName) with
        | null -> failwith $"Attribute %s{attrName} not found"
        | attr ->  attr.Name

    /// What we are actually parsing
    type Part =
        | Widget  of float
        | Sprocket of string * int


    let ParseXmlNode element =
        match element with
        // Parse a Widget without nesting active patterns
        | Elem "Widget" xmlElement ->
            match xmlElement with
            | Attributes xmlElementsAttributes ->
                match xmlElementsAttributes with
                | Attr "Diameter" diameter ->
                    Widget(float diameter)

        // Parse a Sprocket using nested active patterns
        | Elem "Sprocket" (Attributes (Attr "Model" model & Attr "SerialNumber" sn)) ->
            Sprocket(model, int sn)
        |_ -> failwith "Unknown element"


    // Load example document.
    let xmlDoc =
        let doc = XmlDocument()
        let xmlText =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>
            <Parts>
                <Widget Diameter='5.0' />
                <Sprocket Model='A' SerialNumber='147' />
                <Sprocket Model='B' SerialNumber='302' />
            </Parts>
            "
        doc.LoadXml(xmlText)
        doc

    // Now parse the Xml document
    let parsed =
        xmlDoc.DocumentElement.ChildNodes
        |> Seq.cast<XmlElement>
        |> Seq.map ParseXmlNode

    do printfn $"%A{parsed}"
