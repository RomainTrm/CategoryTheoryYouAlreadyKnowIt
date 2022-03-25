# 0 Disclaimer 

- We're not mathematicians
- Don't be shy, just ask, there is no stupid question
- We're not Category Theory experts
- We might not have the answer
- Category Theory isn't mandatory to code in any language/paradigm
- Goal of the session: highlight main concepts through code examples

# 0.1 Why ?
- Interest is to understand mathematical concept before language implementation

# 0 Business example 
- Classical code review, refactor and enumerate some Categorical concepts
- Kind of business code example, several concepts visible

# 0.1 What's Category Theory 
- Category: objects and morphisms (arrows) to link them

# 1 Composition

- Core concept of Category Theory
- Composition: if morphisms ``f: a -> b`` and ``g: b -> c`` exists, then a morphism ``g.f: a -> c`` exists (read *g after f*)
- Associativity: if morphisms ``f: a -> b``, ``g: b -> c`` and ``h: c -> d``, then ``h.(g.f) = (h.g).f``

# 2 Morphism (Abstraction)

- Most morphisms generates a loss of information (abstractions)

# 1.1 Identity / Isomorphism

- Identity: a morphism ``id: a -> a``,
  - left identity: ``f.id`` = ``f``
  - right identity: ``id.f`` = ``f``

- When two morphisms:
  - ``f: a -> b`` and ``g: b -> a``
  - and ``f.g`` = ``idLeft`` and ``g.f`` = ``idRight``
  - then it is an isomorphism
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

# 5 Product
# 6 Coproduct

# 7 Monoid

- 3 properties for monoids 
  - category of a single element
  - associativity
  - neutral element
- Examples :
  - integers through addition
  - integers through multiplication
  - string through concat
  - list through concat
  - ...

# 8 Monad

- "A monad is a monoid in the category of endofunctor"
- A list is:
  - a monoid 
  - a functor
  - an endofunctor
- The issue: given ``f: a -> b list`` and ``list: a list``, then mapping ``map f list`` returns ``b list list``
- The missing element is a "flatten" capability (called ``join``)
```F#
// 'a list list -> 'a list
let join = function
    | [] -> []
    | head::tail -> head@(join tail)
```
- By composing ``map`` and ``join``, we can obtain a method ``bind``
- Given ``f: a -> b list`` and ``list: a list``, then binding ``bind f list`` returns ``b list``
- So, the list is a monad (other examples, Maybe, Result, ...)


# To add (?)

- injective, surjective & bijective on homset