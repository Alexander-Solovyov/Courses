import Data.Char

lenVec3 x y z = sqrt (x * x + y * y + z * z)

sign x = if x == 0 then 0 else (if x > 0 then 1 else -1)

x |-| y = abs (x - y)

twoDigits2Int :: Char -> Char -> Int
twoDigits2Int x y = 
  if isDigit x && isDigit y
    then digitToInt x * 10 + digitToInt y
    else 100

dist :: (Double, Double) -> (Double, Double) -> Double
dist p1 p2 = let
  sqr f = (f p1 - f p2) ^ 2
  in sqrt $ sqr fst + sqr snd

doubleFact :: Integer -> Integer
doubleFact 0 = 1
doubleFact 1 = 1
doubleFact n = (*) n $ doubleFact $ n - 2

-- Recursive fibonacci implemenation
fibonacci' :: Integer -> Integer
fibonacci' 0 = 0
fibonacci' 1 = 1
fibonacci' n | n > 1 = fibonacci (n - 1) + fibonacci (n - 2)
             | n < 0 = fibonacci (n + 2) - fibonacci (n + 1)

-- Iterative fibonacci implementation
fibonacci :: Integer -> Integer
fibonacci n = helper 0 1 n where
    a +- b = a + b * (sign n)
    helper prev _ 0 = prev
    helper prev current m = helper current (prev +- current) (m +- (-1))

-- a(0) = 1; a(1) = 2; a(2) = 3; 
-- a(k + 3) = a(k + 2) + a(k + 1) - 2 * a(k)
seqA :: Integer -> Integer
seqA n = helper 1 2 3 n where
    helper a _ _ 0 = a
    helper a b c n = helper b c (c + b - 2 * a) (n - 1)

-- number -> (sum of digits, number of digits)
sum'n'count :: Integer -> (Integer, Integer)
sum'n'count 0 = (0, 1)
sum'n'count x = helper (abs x) (0, 0) where
    helper 0 a = a
    helper x (s, c) = helper (x `div` 10) (s + x `mod` 10, c + 1)

-- integral f on [a, b]
integration :: (Double -> Double) -> Double -> Double -> Double
integration f a b = let
    n = 1000 :: Double
    h = (b - a) / n
    x i = a + h * i
    trap 0 = 0
    -- Direct implemenation of trapezium rule. Obviously not very effective.
    trap i = h * (f (x i) + f (x $ i - 1)) / 2 + trap (i - 1)
  in trap n
