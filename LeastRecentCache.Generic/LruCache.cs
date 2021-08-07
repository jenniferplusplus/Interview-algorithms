using System;
using System.Collections.Generic;
using System.Linq;

namespace LeastRecentCache.Generic
{
    public class LruCache<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem<TKey, TValue>>> _map;
        private readonly LinkedList<CacheItem<TKey, TValue>> _queue;

        public LruCache(int capacity)
        {
            _capacity = capacity;
            _queue = new LinkedList<CacheItem<TKey, TValue>>();
            _map = new Dictionary<TKey, LinkedListNode<CacheItem<TKey, TValue>>>(capacity);
        }
        
        public TValue Write(TKey key, TValue value)
        {
            LinkedListNode<CacheItem<TKey, TValue>> node;
            if (_map.ContainsKey(key))
            {
                node = _map[key];
                node.Value.Value = value;
                _queue.Remove(node);
            }
            else node = new LinkedListNode<CacheItem<TKey, TValue>>(new CacheItem<TKey, TValue>(key, value));
            
            if (Count() == _capacity)
            {
                _map.Remove(_queue.Last.Value.Key);
                _queue.RemoveLast();
            }

            _queue.AddFirst(node);
            _map[key] = node;
            
            return node.Value.Value;
        }

        public TValue Read(TKey key)
        {
            var exists = _map.TryGetValue(key, out var node);

            if (!exists) return default;
            
            _queue.Remove(node);
            _queue.AddFirst(node);

            return node.Value.Value;
        }

        public TValue Delete(TKey key)
        {
            if (!_map.TryGetValue(key, out var node)) return default;

            _map.Remove(key);
            _queue.Remove(node);

            return node.Value.Value;
        }

        public int Clear()
        {
            var count = _map.Count;
            _map.Clear();
            _queue.Clear();

            return count;
        }

        public int Count()
        {
            return _queue.Count;
        }
    }

    internal class CacheItem <TK, TV>
    {
        public TK Key { get; }
        public TV Value { get; set; }
        
        public CacheItem(TK k, TV v)
        {
            Key = k;
            Value = v;
        }
    }
}