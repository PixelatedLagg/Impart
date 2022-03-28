using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public class Json
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
        public Json()
        {
            _Title = null;
            _Count = 0;
            _Nodes = new Dictionary<string, JsonNode>();
        }
        public Json(string title)
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
        internal static string Escape(string aText)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in aText)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    default:
                        if (c < ' ' || c > 127)
                        {
                            ushort val = c;
                            sb.Append("\\u").Append(val.ToString("X4"));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }
        public static Json ParseFile(string file) => Parse(File.ReadAllText(file));
        public static Json Parse(string aJSON)
        {
            
            Json result = null;
            JsonNode current = null;
            Stack<JsonNode> stack = new Stack<JsonNode>();
            int i = 0;
            StringBuilder Token = new StringBuilder();
            string key = null;
            string value = null;
            string TokenName = "";
            bool QuoteMode = false;
            bool TokenIsQuoted = false;
            bool HasNewlineChar = false;
            while (i < aJSON.Length)
            {
                switch (aJSON[i])
                {
                    case '{':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        stack.Push(new JsonNode())
                        if (current != null)
                        {
                            current.Add(TokenName, stack.Peek());
                        }
                        TokenName = "";
                        Token.Length = 0;
                        ctx = stack.Peek();
                        HasNewlineChar = false;
                        break;
                    /*
                        if (QuoteMode)
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
                    */
                    case '[':
                        if (QuoteMode)
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
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (stack.Count == 0)
                        {
                            throw new ImpartError("Syntax error in JSON file!");
                        }
                        stack.Pop();
                        if (Token.Length > 0 || TokenIsQuoted)
                        {
                            ctx.Add(TokenName, ParseElement(Token.ToString(), TokenIsQuoted));
                        }
                        if (ctx != null)
                        {
                            ctx.Inline = !HasNewlineChar;
                        }   
                        TokenIsQuoted = false;
                        TokenName = "";
                        Token.Length = 0;
                        if (stack.Count > 0)
                        {
                            ctx = stack.Peek().AsJsonNode();
                        }
                        break;
                    case ':':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        TokenName = Token.ToString();
                        Token.Clear();
                        TokenIsQuoted = false;
                        break;
                    case '"':
                        QuoteMode ^= true;
                        TokenIsQuoted |= QuoteMode;
                        break;
                    case ',':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (Token.Length > 0 || TokenIsQuoted)
                        {
                            stack.Push(new JsonNode(TokenName, Token.ToString()));
                        }
                        TokenIsQuoted = false;
                        TokenName = "";
                        Token.Clear();
                        TokenIsQuoted = false;
                        break;
                    case '\r':
                    case '\n':
                        HasNewlineChar = true;
                        break;
                    case ' ':
                    case '\t':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                        }
                        break;
                    case '\\':
                        ++i;
                        if (QuoteMode)
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
                                    string s = aJSON.Substring(i + 1, 4);
                                    Token.Append((char)int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier));
                                    i += 4;
                                    break;
                                default:
                                    Token.Append(C);
                                    break;
                            }
                        }
                        break;
                    case '/':
                        if (!QuoteMode && i + 1 < aJSON.Length && aJSON[i + 1] == '/')
                        {
                            while (++i < aJSON.Length && aJSON[i] != '\n' && aJSON[i] != '\r');
                            break;
                        }
                        Token.Append(aJSON[i]);
                        break;
                    case '\uFEFF':
                        break;
                    default:
                        Token.Append(aJSON[i]);
                        break;
                }
                ++i;
            }
            if (QuoteMode)
            {
                throw new ImpartError("Syntax error in JSON file!");
            }
            return result;
        }
        public static Json Parse(Xml xml)
        {
            return new Json();
        }
    }
}