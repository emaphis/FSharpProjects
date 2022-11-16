// 2.11 Declare a function VAT: int -> float -> float such that the value VAT n x is obtained
//      by increasing x by n percent.
//
//      Declare a function unVAT: int -> float -> float such that
//                            unVAT n (VAT n x) = x
//
//      Hint: Use the conversion function float to convert an int value to a float value.

/// n = rate, x = amount.
let VAT n x =
    let prct = (float n) / 100.0
    x + (x * prct)

let unVAT n x =
    let prct = (100.0 + (float n)) / 100.0
    x / prct

110.0 = VAT 10 100.0
unVAT 10 110.0   // should be 100.0

unVAT 10 (VAT 10 100.0)  // should be 100.0

// blech!!
