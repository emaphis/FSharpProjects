// Complex number with active patterns

[<Struct>]
type Complex(r: float, i: float) =
    static member Polar(mag, phase) = Complex (mag * cos phase, mag * sin phase)
    member x.Magnitude = sqrt(r*r + i*i)
    member x.Phase = atan2 i r
    member x.RealPart = r
    member x.ImaginaryPart = i

let (|Rect|) (x: Complex) = (x.RealPart, x.ImaginaryPart)

let (|Polar|) (x: Complex) = (x.Magnitude, x.Phase)



let addViaRect a b =
    match a, b with
    | Rect (ar, ai), Rect (br, bi) ->
        Complex (ar + br, ai + bi)

let mulViaRect a b =
    match a, b with
    | Rect (ar, ai), Rect (br, bi) ->
        Complex (ar * br - ai * bi, ai * br + bi * ar)

let mulViaPolar a b =
    match a, b with
    | Polar (m, p), Polar (n, q) ->
        Complex.Polar (m * n, p + q)


// examples
let c = Complex (3.0, 4.0)

addViaRect c c

mulViaRect c c
