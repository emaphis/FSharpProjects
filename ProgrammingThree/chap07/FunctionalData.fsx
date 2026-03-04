module FunctionalDataStructures

// Immutable data structures

module FunctionalSet =

    // Example 7-24. Using the Set type

    // The Functional set type
    open System
    open System.IO
    open System.Net

    let getHtml (url: string) =
        let req = WebRequest.Create(url)
        let rsp = req.GetResponse()

        use stream = rsp.GetResponseStream()
        use reader = new StreamReader(stream)

        reader.ReadToEnd()


    let uniqueWords(text: string) =
        let words = text.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)

        let uniqueWords =
            Array.fold (fun (acc: Set<string>) (word: string) -> Set.add word acc)
                        Set.empty
                        words
        uniqueWords


    let urlToShelleysFrankenStein = "https://www.gutenberg.org/cache/epub/84/pg84.txt"

    // Produce a set of unique words
    let wordsInBook =
        urlToShelleysFrankenStein
        |> getHtml
        |> uniqueWords

    let cnt = Set.count wordsInBook
    //val cnt: int = 16362


    // * Functional Map *

    // Example 7-25. Using the Map type
    // uses the Map<_,_> type to associate each word with the number of times
    // it is used in the book. Then, the Map<_,_> is converted to a sequence of key-value tuples,
    // sorted, and the top 20 most frequently occurring words are printed to the screen.

    // The functional Map type
    let wordUsage (text: string) =
        let words = text.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)

        let wordFrequency =
            Array.fold
                (fun (acc: Map<string, int>) (word: string) ->
                    if Map.containsKey word acc then
                        let timesUsed = acc[word]
                        Map.add word (timesUsed + 1) acc
                    else
                        Map.add word 1 acc)
                Map.empty
                words

        wordFrequency


    let printMostFrequentWords (wordFrequency: Map<string, int>) =
        let top20Words =
            wordFrequency
            |> Map.toSeq
            |> Seq.sortBy (fun (_word, timesUsed) -> -timesUsed)
            |> Seq.take 20

        printfn "Top Word Usage:"
        top20Words
        |> Seq.iteri (fun idx (word, timesUsed) ->
               printfn $"%d{idx}\t '%s{word}' was used %d{timesUsed} times")


    // Print the most frequent words
    urlToShelleysFrankenStein
    |> getHtml |> wordUsage |> printMostFrequentWords
