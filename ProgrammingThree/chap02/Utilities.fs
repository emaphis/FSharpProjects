module Utilities


module ConversionUtils =

    // Utilities.ConversionUtils.intToString
    let intToString (x : int) = x.ToString()


module ConvertBase =

    // Utilities.ConversionUtils.ConvertBase.convertToHex
    let convertToHex x = sprintf "%x" x
    // Utilities.ConversionUtils.ConvertBase.convertToOct
    let convertToOct x = sprintf "%o" x

module DataTypes =
    // Utilities.DataTypes.Point
    type Point = Point of float * float * float
