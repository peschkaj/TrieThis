using System.Collections.Generic;
using System.Net.Http;
using NUnit.Framework;
using FluentAssertions;
using Trie;

namespace TrieTests
{
    public class NaiveTrieTests
    {
        [Test]
        public void InsertedDataCanBeRetrieved()
        {
            var nt = new NaiveTrie();
            
            nt.Insert("key");

            nt.Find("key").Should().BeTrue();
        }

        [Test]
        public void PartialKeysAreNotFound()
        {
            var nt = new NaiveTrie();
            
            nt.Insert("key");

            nt.Find("k").Should().BeFalse(); 
        }

        [Test]
        public void DataCanBeInsertedMultipleTimes()
        {
            var nt = new NaiveTrie();
            
            nt.Insert("key");
            nt.Insert("key");
            
            nt.Find("key").Should().BeTrue();
        }

        [Test]
        public void KeysContainsAllInsertedKeys()
        {
            var nt = new NaiveTrie();

            List<string> keys =  new(){ "first", "fur", "app", "apple", "applesauce" };

            foreach (var key in keys)
            {
                nt.Insert(key);
            }

            var foundKeys = nt.Keys();

            foundKeys.Should().BeSubsetOf(keys);
        }
    }
}