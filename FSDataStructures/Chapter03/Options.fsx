// Option types  pg. 130

let isEven (n: int) =
    if n % 2 = 0 then
        Some n
    else
        None

let opt1 = isEven 3
// val opt1: int option = None

let opt2 = isEven 4
// al opt2: int option = Some 4
