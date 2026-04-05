module Utilities


module ConversionUtils =

    // Utilities.ConversionUtils.intToString
    let intToString (x : int) = x.ToString()


module ConvertBase =

    // Utilities.ConversionUtils.ConvertBase.convertToHex
    let convertToHex x = $"%x{x}"

    // Utilities.ConversionUtils.ConvertBase.convertToOct
    let convertToOct x = $"%o{x}"

module DataTypes =
    // Utilities.DataTypes.Point
    type Point = Point of float * float * float
