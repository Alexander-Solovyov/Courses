import Data.Function

getSecondFrom :: a -> b -> c -> b
getSecondFrom a b c = b

multSecond :: Num t => (a, t) -> (a, t) -> t
multSecond = g `on` h where
  g = (*)
  h = snd

on3 :: (b -> b -> b -> c) -> (a -> b) -> a -> a -> a -> c
on3 op f x y z = op (f x) (f y) (f z)

doItYourself :: Double -> Double
doItYourself = f . g . h
h :: (Ord a, Num a) => a -> a
h = max 42
g :: (Num a) => a -> a
g = (^ 3)
f :: (Floating a) => a -> a
f = logBase 2

avg :: Int -> Int -> Int -> Double
avg a b c = (/ 3) $ fromInteger $ toInteger a + toInteger b + toInteger c

