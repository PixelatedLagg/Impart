using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public struct Json : Format //TODO add count property, maybe change types to 'nodes'
    {
        private int _Count;
        public int Count
        {
            get
            {
                return _Count;
            }
        }
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
        }
        private Dictionary<string, JsonNode> _Nodes;
        public Json(string title = null)
        {
            _Title = title;
            _Count = 0;
            _Nodes = new Dictionary<string, JsonNode>();
        }
        public Json Add(params (string, object)[] nodes)
        {
            List<JsonNode> result = new List<JsonNode>();
            foreach ((string, object) node in nodes)
            {
                result.Add(new JsonNode(node.Item1, node.Item2));
            }
            return Add(result.ToArray());
        }
        public Json Add(params JsonNode[] nodes)
        {
            foreach (JsonNode node in nodes)
            {
                Console.WriteLine(node.Key);
                Console.WriteLine(node.Value);
                Console.WriteLine(new JsonNode("hepatitis", 1));
                _Nodes.Add("hepatitis", new JsonNode("hepatitis", 1));
                _Nodes.Add(node.Key, node);
                _Count++;
            }
            return this;
        }
        public Json Remove(JsonNode node) => Remove(node.Key);
        public Json Remove(string key)
        {
            if (!_Nodes.ContainsKey(key))
            {
                throw new ImpartError($"Node with key: '{key}' not found!");
            }
            _Nodes.Remove(key);
            _Count--;
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
            StringBuilder result = new StringBuilder();
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
            if (_Title == null)
            {
                return $"{{ {result.ToString()} }}";
            }
            return $"{{ \"{_Title}\" : {{ {result.ToString()} }} }}";
        }
        public static Json Parse(string file) //TODO json file parsing
        {
            /*int i = 0;
            bool quote = false;
            string json = File.ReadAllText(file);
            Json result = new Json();
            while (i < json.Length)
            {
                switch (json[i])
                {
                    case '{':
                        if (quote)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        stack.Push(new JSONObject());
                        if (ctx != null)
                        {
                            ctx.Add(TokenName, stack.Peek());
                        }
                        TokenName = "";
                        Token.Length = 0;
                        ctx = stack.Peek();
                        HasNewlineChar = false;
                        break;
                    case '[':
                        if (quote)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        stack.Push(new JSONArray());
                        if (ctx != null)
                        {
                            ctx.Add(TokenName, stack.Peek());
                        }
                        TokenName = "";
                        Token.Length = 0;
                        ctx = stack.Peek();
                        HasNewlineChar = false;
                        break;
                    case '}':
                    case ']':
                        if (quote)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (stack.Count == 0)
                        {
                            throw new ImpartError($"An error occured while parsing {file}.");
                        }
                        stack.Pop();
                        if (Token.Length > 0 || TokenIsQuoted)
                            ctx.Add(TokenName, ParseElement(Token.ToString(), TokenIsQuoted));
                        if (ctx != null)
                            ctx.Inline = !HasNewlineChar;
                        TokenIsQuoted = false;
                        TokenName = "";
                        Token.Length = 0;
                        if (stack.Count > 0)
                            ctx = stack.Peek();
                        break;
                    case ':':
                        if (quote)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        TokenName = Token.ToString();
                        Token.Length = 0;
                        TokenIsQuoted = false;
                        break;
                    case '"':
                        quote ^= true;
                        TokenIsQuoted |= QuoteMode;
                        break;
                    case ',':
                        if (quote)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (Token.Length > 0 || TokenIsQuoted)
                            ctx.Add(TokenName, ParseElement(Token.ToString(), TokenIsQuoted));
                        TokenIsQuoted = false;
                        TokenName = "";
                        Token.Length = 0;
                        TokenIsQuoted = false;
                        break;
                    case '\r':
                    case '\n':
                        HasNewlineChar = true;
                        break;
                    case ' ':
                    case '\t':
                        if (quote)
                            Token.Append(aJSON[i]);
                        break;
                    case '\\':
                        ++i;
                        if (quote)
                        {
                            char C = aJSON[i];
                            switch (C)
                            {
                                case 't':
                                    Token.Append('\t');
                                    break;
                                case 'r':
                                    Token.Append('\r');
                                    break;
                                case 'n':
                                    Token.Append('\n');
                                    break;
                                case 'b':
                                    Token.Append('\b');
                                    break;
                                case 'f':
                                    Token.Append('\f');
                                    break;
                                case 'u':
                                    {
                                        string s = aJSON.Substring(i + 1, 4);
                                        Token.Append((char)int.Parse(
                                            s,
                                            System.Globalization.NumberStyles.AllowHexSpecifier));
                                        i += 4;
                                        break;
                                    }
                                default:
                                    Token.Append(C);
                                    break;
                            }
                        }
                        break;
                    case '\uFEFF':
                        break;
                    default:
                        Token.Append(aJSON[i]);
                        break;
                }
                ++i;
            }*/
            //return result;
            return new Json();
        }
        public static Json Parse(Xml xml) //TODO json xml parsing
        {
            
            return new Json();
        }
    }
}