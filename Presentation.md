# 0 Disclaimer 

- We're not mathematicians
- Don't be shy, just ask, there is no stupid question
- We're not Category Theory experts
- We might not have the answer
- Category Theory isn't mandatory to code in any language/paradigm
- Goal of the session: highlight main concepts through code examples

# 1 Why ?

- Interest is to understand mathematical concept before language implementation

# 2 Business example 

- Classical code review, refactor and enumerate some Categorical concepts
- Kind of business code example, several concepts visible

# 3 What's Category Theory 

- Category: objects and morphisms (arrows) to link them

# 4 Composition

- Core concept of Category Theory
- Composition: if morphisms ``f: a -> b`` and ``g: b -> c`` exists, then a morphism ``g.f: a -> c`` exists (read *g after f*)
- Associativity: if morphisms ``f: a -> b``, ``g: b -> c`` and ``h: c -> d``, then ``h.(g.f) = (h.g).f``

# 5 Morphism (Abstraction)

- Most morphisms generates a loss of information (abstractions)

# 6 Identity / Isomorphism

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
- Monoids compose: a product of monoid is also a monoid

# 8 Functor

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
- Sometimes used by functional programmers to transform non-total (partial) functions to total (and pure) functions (Ex: ``either`` or ``result``)  
- Endofunctor: a functor that can lift objets of its own category, example: ``a list list``  

# 9 Natural transformation

- You can have categories of categories 
- Natural transformations are morphisms between categories
- Example: morphism from a ``list`` to a ``maybe``
```F#
let maybeHead = function
    | [] -> None
    | head::tail -> Some head
```

# 10 Endofunctor

- A functor that can apply to itself is an endofunctor
- All the Functors we are dealing with in functional programming are Endofunctors
```C#
public List<List<char>> Endofunctor(List<string> list) =>
    list.Select(s => s.ToList());
```

# 11 Monad

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

# 12 Product

- Tuple
- Product type
- Equivalent to class with fields in OOP 

# 13 Coproduct

- Enum
- Sum type

# To add (?)

- Injective, surjective & bijective on homset






Railway programming - Scott Wlaschin - FSharpforfunandprofit