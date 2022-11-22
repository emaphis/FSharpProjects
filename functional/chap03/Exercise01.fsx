// 3.1 A time of day can be represented as a triple (hours, minutes, f) where f is either AM or PM
//     – or as a record. Declare a function to test whether one time of day comes before another. For
//     example, (11,59,"AM") comes before (1,15,"PM"). Make solutions with triples as well
//     as with records. Declare the functions in infix notation.

let earlier (hr1, min1, meridian1) (hr2, min2, meridian2) =
     (meridian1, hr1, min1) < (meridian2, hr2, min2)

true = earlier (11,59,"AM") (1,15,"PM")
false = earlier (1,15,"PM") (11,59,"AM")

true = earlier (1,15,"AM") (11,59,"AM")
false = earlier (11,59,"AM") (1,15,"AM")

true = earlier (1,15,"AM") (11,15,"AM")
false = earlier (11,15,"AM") (1,15,"AM")
