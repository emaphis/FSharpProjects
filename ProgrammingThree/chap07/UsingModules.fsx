
// Using modules

// Converting Modules to Classes

// * Converting Modules to Classes *

// Example 7-8. Web scraper in a module

module WebScraper1 =

    open System.IO
    open System.Net
    open System.Text.RegularExpressions

    let imagePath = @"C:\Temp\images"
    let url = @"https://oreilly.com"

    // Download the webpage
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()

    // Extract all images
    let results = Regex.Matches(html,  "<img src=\"([^\"]*)\"")
    let allMatches =
        [
            for r in results do
                for grpIdx = 1 to r.Groups.Count - 1 do
                    yield r.Groups[grpIdx].Value
        ]

    let fullyQualified =
        allMatches
        |> List.filter (fun url -> url.StartsWith("https://"))

    // Download the images
    let downloadToDisk(url: string) (filePath: string) =
        use client = new WebClient()
        client.DownloadFile(url, filePath)


    fullyQualified
    |> List.map(fun url ->
        let parts = url.Split([| '/' |])
        url, parts[parts.Length - 1])
    |> List.iter (fun (url, filename) ->
        downloadToDisk url (imagePath + filename))


// Refactoring units of code to functions.
module WebScraper2 =

    open System.IO
    open System.Net
    open System.Text.RegularExpressions

    // Download the webpage
    let downLoadWebPage (url: string) =
        let req = WebRequest.Create(url)
        let resp = req.GetResponse()
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        reader.ReadToEnd()

    // Extract all images
    let extractImageLinks html =
        let results = Regex.Matches(html,  "<img src=\"([^\"]*)\"")
        [
            for r in results do
                for grpIdx = 1 to r.Groups.Count - 1 do
                    yield r.Groups[grpIdx].Value
        ] |> List.filter (fun url -> url.StartsWith("https://"))


    // Download the images
    let downloadToDisk(url: string) (filePath: string) =
        use client = new WebClient()
        client.DownloadFile(url, filePath)


    let scrapeWebsite destPath (imageUrls: string list) =
        imageUrls
        |> List.map(fun url ->
            let parts = url.Split([| '/' |])
            url, parts[parts.Length - 1])
        |> List.iter (fun (url, filename) ->
            downloadToDisk url (destPath + filename))


    let imagePath = @"C:\Temp\images"
    let url = @"https://oreilly.com"

    do downLoadWebPage url
        |> extractImageLinks
        |> scrapeWebsite imagePath


// Example 7-9. Web scraper converted to a class


open System.IO
open System.Net
open System.Text.RegularExpressions

/// Class to scrape image files from a website
type WebScraper(url) =

    // Download the webpage
    let downLoadWebPage (url: string) =
          let req = WebRequest.Create(url)
          let resp = req.GetResponse()
          let stream = resp.GetResponseStream()
          let reader = new StreamReader(stream)
          reader.ReadToEnd()

    // Extract all images
    let extractImageLinks html =
          let results = Regex.Matches(html,  "<img src=\"([^\"]*)\"")
          [
              for r in results do
                  for grpIdx = 1 to r.Groups.Count - 1 do
                      yield r.Groups[grpIdx].Value
          ] |> List.filter (fun url -> url.StartsWith("https://"))


    // Download the images
    let downloadToDisk(url: string) (filePath: string) =
          use client = new WebClient()
          client.DownloadFile(url, filePath)


    let scrapeWebsite destPath (imageUrls: string list) =
          imageUrls
          |> List.map(fun url ->
              let parts = url.Split([| '/' |])
              url, parts[parts.Length - 1])
          |> List.iter (fun (url, filename) ->
              downloadToDisk url (destPath + filename))

    // Class fields
    let m_html = downLoadWebPage url
    let m_images = extractImageLinks m_html

    // Add class members
    member this.SaveImagesToDisk(destPath) =
        scrapeWebsite destPath m_images


module UseWebScaper =

    // instance data
    let imagePath = @"C:\Temp\images"
    let url = @"https://oreilly.com"

    let scraper = WebScraper(url)
    do scraper.SaveImagesToDisk(imagePath)



module IntentionalShadowing =

    // Intentional Shadowing

    let test() =
        let x = 'a'
        let x = "a string"
        let x = x
        x

    printfn $"x = {test()}"
    // x = a


    // Opening modules shadows like named values

    let maxValue = System.Int32.MaxValue
    //val maxValue: int = 2147483647


    let num1 = maxValue + 1
    // val num1: int = -2147483648  // overflow

    open Checked

    let num2 = maxValue + 1
    // System.OverflowException: Arithmetic operation resulted in an overflow.

    // * Controlling Module Usage *
    // Requiring module users to use a qualified name

    [<RequireQualifiedAccess>]
    module Foo =

        let Value = 1

    [<RequireQualifiedAccess>]
    module Bar =

        let value = 2

    module UseModules =

        let baz = Foo.Value + Bar.value
        do printfn $"Baz = {baz}"

    // Opening automatically
    ;;
   // namespace Alpha.Bravo
