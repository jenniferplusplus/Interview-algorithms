# Least Recently Used Cache

Cache invalidation is one of the famously Hard™ problems in computer science. Which makes this stand out as a bad choice among bad choices to be used as an interview assignment. And it's also an extremely common one, in my experience. Clearly.

An LRU cache is pretty simple to reason about, but finicky to implement if you don't already know how, which is trivial to look up. As a cache, it needs to have very fast look up and insert performance, and it needs to have very fast performance to update an invalidation queue and eject the invalidated element when it expires or is removed to adhere to a size constraint.

To accomplish this, the cache maintains a linked list of items in the cache, and a dictionary mapping keys to list items. When a cache item is accessed it's moved to the head of the linked list. Ejections happen from the tail. When I started this project, I made some false starts trying to use a Queue as the invalidation queue, and eventually went with a basic List because I wasn't familiar with C#'s LinkedList object and I only had about an hour to implement the whole thing. Afterward I spent a little bit of time to clean this up, make it generic, fully test it, and replace the List with a LinkedList (it turns out they're actually very convenient to work with. Thanks, Microsoft). That took about 90 more minutes, roughly 5 of which was research.

# Notes

This should be a pretty good implementation, actually. It's not at all optimized, but the algorithm is correct and performant. I may revisit this in the near future to add performance testing, and a benchmark against .Net's built-in MemCache class, which I'm sure is better. MemCache provides an LRU invalidation strategy. A fact that I think does an excellent job of highlighting just how absurd it is to use these activities as interview assessments.