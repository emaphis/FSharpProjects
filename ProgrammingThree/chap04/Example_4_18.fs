// Example 4-18. Try–catch expressions
// shows some code that runs through a minefield of potential problems, with
// each possible exception handled by an appropriate exception handler. In the
// example, the :? dynamic type test operator is used to match against the
// exception type;

open System.IO

[<EntryPoint>]
let main (args: string[]) =

    let exitCode =
        try
            let filePath = args[0]

            printfn "Trying to gather information about file:"
            printfn $"%s{filePath}"

            // Does the drive exist?
            let matchingDrive =
                Directory.GetLogicalDrives()
                |> Array.tryFind(fun drivePatrh -> drivePatrh[0] = filePath[0])

            if matchingDrive = None then
                raise <| new DriveNotFoundException(filePath)

            // Does the folder exist?
            let directory = Path.GetPathRoot(filePath)
            if not <| Directory.Exists(directory) then
                raise <| new DirectoryNotFoundException(filePath)

            // Does the file exist?
            if not <| File.Exists(filePath) then
                raise <| new DirectoryNotFoundException(filePath)

            let fileInfo = FileInfo(filePath)
            printfn "Created  = %s" <| fileInfo.CreationTime.ToString()
            printfn "Access   = %s" <| fileInfo.LastAccessTime.ToString()
            printfn "Size     = %d" fileInfo.Length

            0

        with
        // Combine patterns using Or
        | :? DirectoryNotFoundException
        | :? DirectoryNotFoundException
            ->  printfn "Unhandled Drive or Directory not found exception"
                1
        // Bind the exception value to value ex
        | :? FileNotFoundException as ex
            ->  printfn "Unhandled FileNotFoundException: %s" ex.Message
                3
        | :? IOException as ex
            ->  printfn "Unhandled IOException: %s" ex.Message
                4

        // Us a wildcard match (ex will be of type System.Exception
        | ex
            ->  printfn "Unhandled Exception: %s" ex.Message
                5

    // return the exit code
    printfn "Exiting with code %d" exitCode
    exitCode
