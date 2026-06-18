// Imperative Programming
// Basic I/O

// Working with the Console

// With F#

let name1 = "Juliet"
let name2 = "Scorpio0"
let str1 = $"Hi, I'm %s{name1} and I'm a %s{name2}"

// With .NET
open System

let str2 = String.Format("Hi, my name is {0} and I'm a {1}", "Juliet", "Scorpio")
// val str2: string = "Hi, my name is Juliet and I'm a Scorpio"

let str3 = String.Format("|{0,-50}|", "Left justified")
// val str3: string = "|Left justified

let str4 = String.Format("|{0,50}|", "Right justified")
// val str4: string = "|                                   Right justified|"

let str5 = String.Format("|{0:yyyy-MMM-dd}|", DateTime.Now)
//  val str5: string = "|2026-Jun-17|"



// Programmers can read and write to the console using the System.Console class

//open System

let main1() =
    Console.Write "What is your name? "
    let name = Console.ReadLine()
    Console.WriteLine("Hello {0}", name)


main1()


// System.IO Namespace

// Files and Directories

// The System.IO.File class exposes several useful members for creating, appending, and deleting files.
// System.IO.Directory exposes methods for creating, moving, and deleting directories.
// System.IO.Path performs operations on strings which represent file paths.
// System.IO.FileSystemWatcher which allows users to listen to a directory for changes.


// Streams

// System.IO.StreamReader which is used to read characters from a stream.
// System.IO.StreamWriter which is used to write characters to a stream.
// System.IO.MemoryStream which creates an in-memory stream of bytes.
