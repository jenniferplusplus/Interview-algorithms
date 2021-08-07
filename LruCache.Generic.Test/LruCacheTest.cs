using System;
using LeastRecentCache.Generic;
using Xunit;

namespace LruCache.Generic.Test
{
    public class LruCacheTest
    {
        private LruCache<string, object> _cache;

        public LruCacheTest()
        {
            _cache = new LruCache<string, object>(10);
        }
        
        
        [Fact]
        public void CanWrite()
        {
            _cache.Write("1", "anything");
            Assert.Equal(1, _cache.Count());
        }

        [Fact]
        public void WriteReturnsValue()
        {
            var actual = _cache.Write("1", "something");
            
            Assert.Equal("something", actual);
        }

        [Fact]
        public void CanRead()
        {
            _cache.Write("1", "one");
            
            Assert.Equal("one", _cache.Read("1"));
        }
        
        [Fact]
        public void CanRead_WhenMultipleItems()
        {
            _cache.Write("1", "one");
            _cache.Write("2", "two");
            
            Assert.Equal("one", _cache.Read("1"));
            Assert.Equal("two", _cache.Read("2"));
        }

        [Fact]
        public void CanRemoveOldestOnWrite()
        {
            var cache = new LruCache<string, object>(3);

            cache.Write("1", 1);
            cache.Write("2", 2);
            cache.Write("3", 3);
            cache.Write("4", 4);
            
            Assert.Equal(3, cache.Count());
            Assert.Null(cache.Read("1"));
        }
        
        [Fact]
        public void CanUpdateOnUse()
        {
            var cache = new LruCache<string, object>(3);

            cache.Write("1", 1);
            cache.Write("2", 2);
            cache.Write("3", 3);
            cache.Write("1", "1, but as a string");
            cache.Write("4", 4);
            
            Assert.Equal(3, cache.Count());
            Assert.Null(cache.Read("2"));
            Assert.Equal("1, but as a string", cache.Read("1"));
        }
        
        [Fact]
        public void OverwriteDoesntEject()
        {
            var cache = new LruCache<string, object>(3);

            cache.Write("1", 1);
            cache.Write("2", 2);
            cache.Write("3", 3);
            cache.Write("1", "1, but as a string");
            
            Assert.NotNull(cache.Read("3"));
        }

        [Fact]
        public void CanClean()
        {
            _cache.Write("1", "something");
            var removed = _cache.Clear();
            
            Assert.Equal(0, _cache.Count());
            Assert.Equal(1, removed);
        }

        [Fact]
        public void CanDelete()
        {
            _cache.Write("1", "some value");
            var deleted = _cache.Delete("1");
            
            Assert.Equal(0, _cache.Count());
            Assert.Equal("some value", deleted);
        }

        [Fact]
        public void DeleteDoesNothingWhenNothingToDelete()
        {
            _cache.Write("1", "some value");
            _cache.Delete("does not exist");
            
            Assert.Equal(1, _cache.Count());
        }
    }
}