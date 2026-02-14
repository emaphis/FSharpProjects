// The .NET Platform

module TheNETPlatform

module GarbageCollection =

    // Implementing IDisposable

    open System
    open System.IO
    open System.Collections.Generic

    type MultiFileLogger() =
        do printfn "Constructing  ..."
        let m_logs = List<StreamWriter>()

        member this.AttachLogFile file =
            let newLogFile = new StreamWriter(file, true)
            m_logs.Add(newLogFile)

        member this.LogMessage (msg: string) =
            m_logs |> Seq.iter (fun writer -> writer.WriteLine(msg))

        interface IDisposable with
            member this.Dispose (): unit =
                printfn "Cleaning up ..."
                m_logs |> Seq.iter (fun writer -> writer.Close())
                m_logs.Clear()



    // Write some code using MultiFileLogger
    let task1() =
        use logger = new MultiFileLogger()
        logger.AttachLogFile("hello.txt")
        logger.AttachLogFile("output.txt")

        logger.LogMessage("Starting system ...")
        logger.LogMessage("Status message")
        logger.LogMessage("Quitting system ...")

        printfn "Exiting the function task1"
        // ...
        ()

    printfn $"dir = {Directory.GetCurrentDirectory()}"

    do task1()

    //Constructing  ...
    //Exiting the function task1
    //Cleaning up ...
    //val it: unit = ()
