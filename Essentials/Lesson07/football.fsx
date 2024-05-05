// Lesson 7 - Football scores

// The rules are simple;

// Matched score = 3 - (0, 0) && (0, 0)
// Result = 1        - (2, 1) && (5, 1) || (0, 0) && (2, 2)
// Otherwise = 0



type Score = int * int

// without active patterns
let calculate0 (guess:Score) (actual:Score) =
    match guess, actual with
    | _ when guess = actual
        -> 3
    | (gh, ga), (ah, aa) when gh = ga && ah = aa // Draw
        -> 1
    | (gh, ga), (ah, aa) when gh > ga && ah > aa  // Home win
        -> 1
    | (gh, ga), (ah, aa) when gh < ga && ah < aa  // Away win
        -> 1
    | _  -> 0


// guess: Score * actual: Score -> unit option
let (|CorrectScore|_|) (guess:Score, actual:Score) =
    if guess = actual then Some () else None


// int * int -> Choice<unit,unit,unit>
let (|Draw|HomeWin|AwayWin|) (score:Score) =
    match score with
    | (h, a) when h = a  -> Draw
    | (h, a) when h > a  -> HomeWin
    | _ -> AwayWin

//  guess: Score * actual: Score -> unit option
let (|CorrectResult|_|) (guess:Score, actual:Score) =
    match guess, actual with
    | Draw, Draw
    | HomeWin, HomeWin
    | AwayWin, AwayWin ->  Some ()
    | _  -> None



let calculate (guess:Score) (actual:Score) =
    match guess, actual with
    | CorrectScore
        -> 3
    | CorrectResult -> 1
    | _  -> 0


let isEqualTo expected actual =
    expected = actual


// Tests
let correctScore = calculate (1, 1) (1, 1) |> isEqualTo 3
let correctResultDraw = calculate (2, 2) (0, 0) |> isEqualTo 1
let correctResultHomeWin = calculate (1, 0) (5, 3) |> isEqualTo 1
let correctResultAwayWin = calculate (0, 1) (3, 5) |> isEqualTo 1
