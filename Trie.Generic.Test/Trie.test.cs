using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Trie.Generic.Test
{
    public class TrieTest
    {
        private readonly ITestOutputHelper _output;
        private Trie<string> trie;

        public TrieTest(ITestOutputHelper output)
        {
            _output = output;
            trie = new Trie<string>();
        }

        [Fact]
        public void GetAllIncludesRoot()
        {
            Assert.Single(trie.GetAll());
        }
        
        [Fact]
        public void GetAllReturnsAllWithNoDupes()
        {
            trie.Add("a", "a");
            trie.Add("ab", "ab");
            trie.Add("abc", "abc");

            _output.WriteLine(string.Join(", ", trie.GetAll().Select(each => each.Value).ToArray()));
            Assert.Equal(4, trie.GetAll().Count());
        }

        [Fact]
        public void ValuesReturnsAllWithNoDupes()
        {
            trie.Add("a", "a");
            trie.Add("ab", "ab");
            trie.Add("abc", "abc");
            
            Assert.Equal(4, trie.Values.Count());
        }

        [Fact]
        public void AddUsesExistingNodeWhenPresent()
        {
            trie.Add("run", "run");
            trie.Add("r", "r");

            Assert.Contains("run", trie.Values);
            Assert.Contains("r", trie.Values);
            Assert.Equal(4, trie.Values.Count());

            _output.WriteLine(string.Join(", ", trie.GetAll().Select(each => each.Value).ToArray()));
        }

        [Fact]
        public void FindReturnsRootOfASubtree()
        {
            trie.Add("runner", "leaf - runner");
            trie.Add("r", "root - r");
            trie.Add("ru", "mid - ru");
            
            var actual = trie.Find("run");
            
            Assert.Equal(4, actual.Values.Count());
            Assert.DoesNotContain("root - r", actual.Values);
            Assert.DoesNotContain("mid - ru", actual.Values);
            Assert.Contains("leaf - runner", actual.Values);
            
            _output.WriteLine(string.Join(", ", trie.GetAll().Select(each => each.Value).ToArray()));
            _output.WriteLine(string.Join(", ", actual.GetAll().Select(each => each.Value).ToArray()));
        }

        [Fact]
        public void FindReturnsNullWhenNotFound()
        {
            trie.Add("runner", "runner");

            var actual = trie.Find("no");
            
            Assert.Null(actual);
        }

        [Fact]
        public void ItemAccessorReturnsExistingValue()
        {
            trie.Add("runner", "runner");

            var actual = trie["runner"];
            
            Assert.Equal("runner", actual);
        }
        
        [Fact]
        public void ItemAccessorReturnsExistingEmptyValue()
        {
            trie.Add("runner", "runner");

            var actual = trie["run"];
            
            Assert.Null(actual);
        }

        [Fact]
        public void ItemAccessorThrowsWhenMissing()
        {
            trie.Add("runner", "runner");

            Assert.Throws<KeyNotFoundException>(() => trie["no"]);
        }

        [Fact]
        public void ItemAccessorAddsAValue()
        {
            trie["runner"] = "runner";

            Assert.Contains("runner", trie.Values);
        }
        
        [Fact]
        public void ItemAccessorSetsANewValue()
        {
            trie["runner"] = "runner";
            trie["runner"] = "faster";

            Assert.Contains("faster", trie.Values);
            Assert.DoesNotContain("runner", trie.Values);
        }
    }
}