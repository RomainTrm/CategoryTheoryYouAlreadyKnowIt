# 0 Disclaimer 

- We're not mathematicians
- We're not Category Theory experts
- Category Theory isn't mandatory to code in any language/paradigm
- Goal of the session: highlight main concepts through code examples

# 0 Business example 

- Kind of business code example, several concepts visible

# 1 Composition

- Core concept of Category Theory
- Category: objects and morphisms (arrows) to link them
- Composition: if morphisms ``f: a -> b`` and ``g: b -> c`` exists, then a morphism ``g.f: a -> c`` exists (read *g after f*)
- Identity: a morphism ``id: a -> a``, 
  - left identity: ``f.id`` = ``f``
  - right identity: ``id.f`` = ``f``
  
# 2 Morphism / Isomorphism

- Most morphisms generates a loss of information
- When two morphisms:
  - ``f: a -> b`` and ``g: b -> a``
  - and ``f.g`` = ``idLeft`` and ``g.f`` = ``idRight``
  - then it is a isomorphism
- Example of isomorphism:
  ```F#  
  let reverse (a, b) = (b, a)
  reverse >> reverse = id
  ```

# 3 Functor

- A functor is a "morphism" between two categories
- It lift an objet *a* of a category __A__ into a category __F__, notation is __Fa__
- It preserve structure:
  ```
  Fa --Ff--> Fb
  ^    ^     ^
  |    |     | 
  F   map    F
  |    |     |
  a ---f---> b
  ```
- Endofunctor: a functor that can lift objets of its own category, example: ``a list list``

# 4 Natural transformation

- You can have categories of categories 
- Natural transformations are morphisms between categories
- Example: morphism from a ``list`` to a ``maybe``
```F#
let maybeHead = function
    | [] -> None
    | head::tail -> Some head
```


# To add (?)

- injective, surjective & bijective on homset