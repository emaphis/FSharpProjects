module Enumerations


module CreatingEnumerations =

    // Example 6-6. Declaring an enumeration

    type ChessPiece =
        | Empty  = 0
        | Pawn   = 1
        | Knight = 3
        | Bishop = 4
        | Rook   = 5
        | Queen  = 8
        | King   = 1000000


    //  Example 6-7. Initializing a chessboard enum arrayC

    /// Create a 2-D array of ChessPiece enumeration
    let createChessBoard() =
        let board = Array2D.init 8 8 (fun _ _ -> ChessPiece.Empty)

        // Place pawns
        for i = 0 to 7 do
            board[1,i] <- ChessPiece.Pawn
            board[6,i] <- enum<ChessPiece> (-1 * int ChessPiece.Pawn)

        // Place pawns
        for i = 0 to 7 do
            board[1,i] <- ChessPiece.Pawn
            board[6,i] <- enum<ChessPiece> (-1 * int ChessPiece.Pawn)

        // Place black pieces in order
        [| ChessPiece.Rook; ChessPiece.Knight; ChessPiece.Bishop; ChessPiece.Queen;
            ChessPiece.King; ChessPiece.Bishop; ChessPiece.Knight; ChessPiece.Rook |]
        |> Array.iteri(fun idx piece -> board[0,idx] <- piece)

        // Place white pieces in order
        [| ChessPiece.Rook;  ChessPiece.Knight; ChessPiece.Bishop; ChessPiece.King;
            ChessPiece.Queen; ChessPiece.Bishop; ChessPiece.Knight; ChessPiece.Rook |]
        |> Array.iteri(fun idx piece ->
                        board[7,idx] <- enum<ChessPiece> (-1 * int piece))

        // Return the board
        board


    /// Check if piece is a Pawn
    let isPawn piece =
        match piece with
        | ChessPiece.Pawn  -> true
        | _   -> false

open CreatingEnumerations

module ExampleBoard =

    let board = createChessBoard()

    let numOfPawns =
        board
        |> Seq.cast<ChessPiece>
        |> Seq.filter isPawn
        |> Seq.length

module Conversion =

    // To construct an enumeration value use `enum<_>`

    // Enums are only syntactic sugar over primitive types
    let invalidPiece = enum<ChessPiece>(42)

    // To get values from and enumeration:

    let materialValueOfQueen = int ChessPiece.Queen
    //val materialValueOfQueen: int = 8

module WhenToUse =

    // When to Use an Enum Versus a Discriminated Union
    // - Enumerations don't have a safety guarantee, they are a wrapper over primitive types
    // - Enums are value types, so are stored on the stack and have higher performance.
    // - Discriminated unions are reference types.


    let bool1 = System.Enum.IsDefined(typeof<ChessPiece>, int ChessPiece.Bishop)
    // val bool1: bool = true

    let boo2 = System.Enum.IsDefined(typeof<ChessPiece>, -711)
    // val boo2: bool = false

    // Using enums as Bitflags
    // Example 6-8. Combining enumeration values for flags

    open System

    // Enumeration of flag values
    type FlagsEnum =
        | OptionA = 0b0001
        | OptionB = 0b0010
        | OptionC = 0b0100
        | OptionD = 0b1000

    let isFlagSet (enum : FlagsEnum) (flag : FlagsEnum) =
        let flagName = Enum.GetName(typeof<FlagsEnum>, flag)
        if enum &&& flag = flag then
            printfn $"Flag [{flagName}] is set."
        else
            printfn $"Flag [{flagName}] is not set."

    // Check if given flags are set
    let customFlags = FlagsEnum.OptionA ||| FlagsEnum.OptionC

    isFlagSet customFlags FlagsEnum.OptionA
    isFlagSet customFlags FlagsEnum.OptionB
    isFlagSet customFlags FlagsEnum.OptionC
    isFlagSet customFlags FlagsEnum.OptionD

    // Flag [OptionA] is set.
    // Flag [OptionB] is not set.
    // Flag [OptionC] is set.
    // Flag [OptionD] is not set.
