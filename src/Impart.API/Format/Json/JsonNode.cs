using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public class JsonNode
    {
        private bool _IsArray;
        public bool IsArray
        {
            get
            {
                return _IsArray;
            }
        }

        private int _Count;
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        private object _Value;
        public object Value
        {
            get
            {
                return _Value;
            }
        }

        private string _Key;
        public string Key
        {
            get
            {
                return _Key;
            }
        }

        private Dictionary<string, JsonNode> _Nodes;
        public JsonNode(string key, object value)
        {
            _Count = 0;
            _Key = key;
            _Value = value;
            _IsArray = false;
            _Nodes = new Dictionary<string, JsonNode>();
        }
        public JsonNode Add(params (string, object)[] nodes)
        {
            List<JsonNode> result = new List<JsonNode>();
            foreach ((string, object) node in nodes)
            {
                result.Add(new JsonNode(node.Item1, node.Item2));
            }
            return Add(result.ToArray());
        }
        public JsonNode Add(params JsonNode[] nodes)
        {
            foreach (JsonNode node in nodes)
            {
                _Nodes.Add(node.Key, node);
                _Count++;
            }
            return this;
        }
        public JsonNode Remove(JsonNode node) => Remove(node.Key);
        public JsonNode Remove(string key)
        {
            if (!_Nodes.ContainsKey(key))
            {
                throw new ImpartError($"Node with key: '{key}' not found!");
            }
            _Nodes.Remove(key);
            _Count--;
            if (_Nodes.Count == 0)
            {
                _IsArray = false;
            }
            return this;
        }
        public JsonNode this[string key]
        {
            get
            {
                if (!_Nodes.ContainsKey(key))
                {
                    throw new ImpartError($"Node with key: '{key}' not found!");
                }
                return _Nodes[key];
            }
        }
        internal string Render()
        {
            if (IsArray)
            {
                StringBuilder result = new StringBuilder($"\"{_Key}\" : {{");
                foreach (JsonNode node in _Nodes.Values)
                {
                    if (result.Length == 0)
                    {
                        result.Append(node.Render());
                    }
                    else
                    {
                        result.Append($", {node.Render()}");
                    }
                }
                return result.ToString();
            }
            else
            {
                return $"\"{_Key}\" : \"{_Value}\"";
            }
        }
    }
}