public class Solution {
    public class Trie
    {
        public TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public string GetLongestPrefix()
        {
            // root must have only 1 child
            if (root.children.Count != 1) { return ""; }

            var current = new StringBuilder(); // working storage
            var longest = new StringBuilder(); // final result
            var queue = new Queue<char>();
            var node = root;
            
            // put the first common prefix in the queue
            foreach (char key in node.children.Keys)
            {
                queue.Enqueue(key);
            }

            // process the queue
            while (queue.Count > 0)
            {
                // get next letter
                char letter = queue.Dequeue();

                // add letter current longest
                current.Append(letter);

                // add letter to the longest so far
                if (longest.Length < current.Length) { longest.Append(letter); }

                // set node pointer to new letter
                node = node.children[letter];
                
                // add child to queue (we're looking for exactly one child)
                // we don't add this letter if it is an 'end of word' letter
                // because it means we're about to go past a shorter word
                // that was added earlier. The longest common prefix can't
                // be longer than the end of any word.
                if (node.children.Count == 1 && !node.isEndOfWord)
                {
                    foreach (char key in node.children.Keys)
                    {
                        queue.Enqueue(key);
                    }
                }
            }

            return longest.ToString(); 
        }
        public void Add(string word)
        {
            TrieNode temp = root;

            // iterate characters
            for (int i  = 0; i < word.Length; i++)
            {
                // grab a letter
                char c = word[i];

                // if it's not in the dictionary, add it
                if (!temp.children.ContainsKey(c))
                {
                    temp.children.Add(c, new TrieNode());
                }
                
                // update pointer to the letter we just added
                temp = temp.children[c];

                // mark end of word
                if (i == word.Length - 1)
                {
                    temp.isEndOfWord = true;
                }
            }
        }
    }
    public class TrieNode
    {
        public bool isEndOfWord;
        public Dictionary<char, TrieNode> children;

        public TrieNode()
        {
            isEndOfWord = false;
            children = new Dictionary<char, TrieNode>();
        }
    }
    public string LongestCommonPrefix(string[] strs) {
        if (strs.Length == 0) return "";
        if (strs.Length == 1) return strs[0];

        var trie = new Trie();
        foreach (string str in strs)
        {
            if (str == "") { return ""; } // automatically won't have a common prefix
            trie.Add(str);
        }


        return trie.GetLongestPrefix();
    }
}