// Sequences  - pg. 125

//  seq<'a> is an alias, or a type abbreviation for IEnumerable<'a>

let countToTen = seq { 1 .. 10 }
// val countToTen: int seq

countToTen
//val it: int seq = seq [1; 2; 3; 4; ...]

let alphabets = seq { 'a'..'z' }
alphabets |> Seq.exists (fun c -> c = 'x')
// val it: bool = true

let nationalDebt = seq { 1I .. 18000000000000I }
