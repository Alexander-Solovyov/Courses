module GorkAndMork where

class KnownToGork a where
    stomp :: a -> a
    doesEnrageGork :: a -> Bool

class KnownToMork a where
    stab :: a -> a
    doesEnrageMork :: a -> Bool

class (KnownToGork a, KnownToMork a) => KnownToGorkAndMork a where
    stompOrStab :: a -> a
    stompOrStab x = enrageGork $ enrageMork x where
      enrageGork = 
        if doesEnrageMork x
          then stomp
          else id
      enrageMork = 
        if doesEnrageGork x
          then stab
          else id
