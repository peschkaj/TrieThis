using System;
using System.Collections.Generic;
using System.Text;

namespace Trie
{
    /// <summary>
    /// A naïve trie that uses one node per character in the trie.
    /// </summary>
    public class NaiveTrie
    {
        private readonly NaiveTrieNode _root = new();
        
        public void Insert(string key) => _root.Insert(key);

        public bool Find(string key) => key != string.Empty && _root.Find(key);

        public List<string> Keys()
        {
            var keys = new List<string>();
            var sb = new StringBuilder();
            _root.Keys(ref keys, ref sb);

            return keys;
        }
    }

    internal class NaiveTrieNode
    {
        private readonly Dictionary<char, NaiveTrieNode> _children = new();
        private bool _isWord = false;

        internal void Insert(ReadOnlySpan<char> key)
        {
            if (!_children.ContainsKey(key[0]))
            {
                _children.Add(key[0], new NaiveTrieNode());
            }
            
            if (key.Length == 1)
            {
                _isWord = true;
                return;
            }
            
            _children[key[0]].Insert(key.Slice(1));
        }

        internal bool Find(ReadOnlySpan<char> key)
        {
            if (!_children.ContainsKey(key[0]))
                return false;
            
            if (key.Length > 1)
                return _children[key[0]].Find(key.Slice(1));

            return _isWord;
        }

        public void Keys(ref List<string> keys, ref StringBuilder sb)
        {
            foreach (var c in _children)
            {
                sb.Append(c.Key);

                if (_isWord) 
                    keys.Add(sb.ToString());
                
                c.Value.Keys(ref keys, ref sb);

                sb.Remove(sb.Length - 1, 1);
            }
        }
    }
}