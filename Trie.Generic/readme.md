# Trie

A Trie (usually pronounced try, but sometimes tree), is a non-binary tree structure where nodes in the tree can be addressed by a string or other iterable data structure.

It has performance and usage that are similar to a hash table, so that's nice.

## Notes

This is a Generic implementation of a Trie, which allows for any IEnumerable to be used as a key. Strings are Enumerable, and this would be the expected key, but there could be good reason's to use another data structure like an array or list. Trie nodes internally make use of a Dictionary to map child nodes with their keys.

The Trie itself could implement IEnumerable, but I haven't yet. I might in the future.

Trie is an interesting case, because in C# at least there is no built-in Trie object. Although it would be pretty uncommon that a a Trie would give substantial benefits over a Dictionary for generic use, and a highly specific use case would warrant a highly specific implementation, which this is not.