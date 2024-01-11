# Contributing to Reign (formally known as "Faraway")

If you are reading this, thank you for accepting employement on developing
_Reign_.

## Code Standards

1. When building a new `Component` or `GameObject`, the `virtual` methods are not
   meant to be called outside or internally at all
2. Try to use `GameObject.GetComponent<T>()` as little as possible
3. When calling `GameObject.AddComponent()`, cache your added object in the `GameObject`
   as a variable
4. If you're implementing something that can be used in another game project, write it in
   the game engine

## Bugs & Enhancements

There is currently no method of reporting bugs. Utilizing GitHub's issue tracker
is not recommended as we might use something else as we would like the community
and the developers have access to the same issue tracker. Send a private message
to the maintainer of the project to report any bugs or enhancements.

## Pull Request Process

1. Make sure your code is properly formatted before sending a commit
2. Write new unittests for any new enhancements
