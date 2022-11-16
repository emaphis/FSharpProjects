// 2.7  Binomial coeficents.

let rec bin = function
    | (n, 0) -> 1
    | (n, k) when n = k -> 1
    | (n, k) -> bin (n-1, k-1) + bin (n-1, k)

// testing
1 = bin (0, 0)
1 = bin (1, 1)
1 = bin (2, 0)
2 = bin (2, 1)
6 = bin (4, 2)
4 = bin (4, 3)
1 = bin (2, 2)
1 = bin (3, 0)
3 = bin (3, 1)
3 = bin (3, 2)
1 = bin (3, 3)
1 = bin (4, 0)
4 = bin (4, 1)
6 = bin (4, 2)
4 = bin (4, 3)
1 = bin (4, 4)
