module WorkingWithThreads

module SpawningThreads =

    // Example 9-1. Creating threads
    // spawns a couple of threads that each count to five

    // Creating new threads

    open System.Threading

    /// What will execute on each thread
    let threadBody() =
        for i in 1 .. 5 do
            // Wait 1/10 of a second
            Thread.Sleep(100)
            let threadId = Thread.CurrentThread.ManagedThreadId
            printfn $"[Thread %d{threadId}] %d{i}..."

    let spawnThread() =
        let thread = Thread(threadBody)
        thread.Start()


    // spawn a couple of threads at once
    do spawnThread()
    do spawnThread()


module TheNETThreadPool =

    open System.Threading

    let bool1 = ThreadPool.QueueUserWorkItem(fun _ -> for i = 1 to 5 do printfn $"%d{i}")

    // Our thread pool task, note that the delegate's
    // parameter is of type obj
    let printNumbers(max: obj) =
        for i = 1 to (max :?> int) do
            printfn $"%d{i}"

    let bool2 = ThreadPool.QueueUserWorkItem(WaitCallback(printNumbers), box 5)


module SharingData =

    // Race conditions

    // Example 9-2. Race conditions
    // loops through an array and adds up its elements on two threads.
    // Both threads are constantly updating the total reference cell, which leads to a data race.

    open System.Threading

    let sumArray (arr : int[]) =
        let total = ref 0

        // Add the first half
        let thread1Finished = ref false

        ThreadPool.QueueUserWorkItem(
            fun _ -> for i = 0 to arr.Length / 2 - 1 do
                        total.Value <- arr[i] + total.Value
                     thread1Finished.Value <- true
            ) |> ignore

        // Add the second half
        let thread2Finished = ref false

        ThreadPool.QueueUserWorkItem(
            fun _ -> for i = arr.Length / 2 to arr.Length - 1 do
                        total.Value <- arr[i] + total.Value
                     thread2Finished.Value <- true
            ) |> ignore

        // Wait while the two threads finish their work
        while thread1Finished.Value = false ||
              thread2Finished.Value = false do

              Thread.Sleep(0)

        total.Value


    // Array of 1,000,00 of ones
    let millionOnes = Array.create 1_000_000 1

    let sum1 =  sumArray millionOnes
    //val sum1: int = 890914
    //val sum1: int = 713841
    //val sum1: int = 783337
    //val sum1: int = 932673
    // Oops!


    // Example 9-3. Array summing using lock
    // Fix data race with `lock`

    let lockedSumArray (arr : int[]) =
        let total = ref 0

        // Add the first half
        let thread1Finished = ref false
        ThreadPool.QueueUserWorkItem(
            fun _ -> for i = 0 to arr.Length / 2 - 1 do
                        lock total (fun () -> total.Value <- arr[i] + total.Value)
                     thread1Finished.Value <- true
            ) |> ignore

        // Add the second half
        let thread2Finished = ref false
        ThreadPool.QueueUserWorkItem(
            fun _ -> for i = arr.Length / 2 to arr.Length - 1 do
                        lock total (fun () -> total.Value <- arr[i] + total.Value)
                     thread2Finished.Value <- true
            ) |> ignore

        // Wait while the two threads finish their work
        while thread1Finished.Value = false ||
              thread2Finished.Value = false do

              Thread.Sleep(0)

        total.Value


    let sum2 = lockedSumArray millionOnes
    //val sum2: int = 1000000
    //val sum2: int = 1000000


    // Deadlocks

    // Example 9-4. Deadlocks in F#
    // simple implementation of a bank account and a function to transfer
    // between two bank accounts using locking

    type BankAccount = { AccountID: int; OwnerName: string; mutable Balance: int }

    /// Transfer money between bank accounts
    let transferFunds amount fromAcct toAcct =
        printfn $"Locking %s{toAcct.OwnerName}'s account to deposit funds..."

        lock fromAcct
            (fun () ->
                printfn $"Locking %s{fromAcct.OwnerName}'s account to withdraw funds..."
                lock fromAcct
                    (fun () ->
                        fromAcct.Balance <- fromAcct.Balance - amount
                        toAcct.Balance   <- toAcct.Balance + amount  ))



    let john = { AccountID = 1; OwnerName = "John Smith"; Balance = 1000 }
    let jane = { AccountID = 2; OwnerName = "Jane Doe";   Balance = 2000 }

    let trn1 = ThreadPool.QueueUserWorkItem(fun _ -> transferFunds 100 john jane)
    let trn2 = ThreadPool.QueueUserWorkItem(fun _ -> transferFunds 100 jane john)

    printfn $"{john}"
    printfn $"{jane}"
