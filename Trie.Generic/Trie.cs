using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Trie.Generic
{
    public class Trie<T>
    {
        public Dictionary<object, Trie<T>> Children { get; }
        public T Value { get; private set; }

        public IEnumerable<T> Values => GetAll().Select(each => each.Value);
        public T this [IEnumerable key]
        {
            get
            {
                var result = Search(key, false);
                if (result == null) throw new KeyNotFoundException("The key sequence does not exist in the Trie");

                return result.Value;
            }
            set => Search(key, true).Value = value;
        }

        public Trie ()
        {
            Children = new Dictionary<object, Trie<T>>();
        }

        public void Add(IEnumerable keys, T value)
        {
            var node = Search(keys, true);
            node.Value = value;
        }
        
        public Trie<T> Find(IEnumerable keys)
        {
            return Search(keys, false);
        }

        public IEnumerable<Trie<T>> GetAll() => Children.Values.SelectMany(child => child.GetAll()).Append(this);

        private Trie<T> Search(IEnumerable keys, bool build)
        {
            var cursor = this;
            foreach (var key in keys)
            {
                if (!cursor.Children.ContainsKey(key))
                {
                    if (!build) return default;
                    cursor.Children[key] = new Trie<T>();
                }
                cursor = cursor.Children[key];
            }

            return cursor;
        }
    }
}