using System;
using System.Collections.Generic;

namespace Impart
{
    public class List : Element
    {
        private Dictionary<int, Text> _entries;
        private string id;
        private string style;
        private string attributes;
        private string type;
        internal string textCache;
        public Dictionary<int, Text> entries 
        {
            get 
            {
                return _entries;
            }
        }
        public List(ICommon.ListTypes type, params Text[] textEntries)
        {
            switch (type)
            {
                case ICommon.ListTypes.Unordered:
                    this.type = "ul";
                    break;
                case ICommon.ListTypes.Ordered:
                    this.type = "ol";
                    break;
                default:
                    throw new ListError("Invalid list type!", this);
            }
            _entries = new Dictionary<int, Text>();
            foreach (Text text in textEntries)
            {
                if (text.id == null)
                {
                    textCache += $"        <li>%^            <p{text.attributes}{text.style}>{text.text}</p>%^        </li>%^";
                }
                else
                {
                    textCache += $"        <li>%^            <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^        <li>%^";
                }
                _entries.Add(_entries.Count + 1, text);
            }
            id = "";
            style = "";
            attributes = "";
        }
        public List(int type, string id, params Text[] textEntries)
        {
            switch (type)
            {
                case 0:
                    this.type = "ul";
                    break;
                case 1:
                    this.type = "ol";
                    break;
                default:
                    throw new ListError("Invalid list type!", this);
            }
            if (String.IsNullOrEmpty(id))
            {
                throw new ListError("Specified ID cannot be null or empty!", this);
            }
            else
            {
                this.id = $" id=\"{id}\"";
            }
            _entries = new Dictionary<int, Text>();
            foreach (Text text in textEntries)
            {
                if (text.id == null)
                {
                    textCache += $"        <li>%^            <p{text.attributes}{text.style}>{text.text}</p>%^        </li>%^";
                }
                else
                {
                    textCache += $"        <li>%^            <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^        <li>%^";
                }
                _entries.Add(_entries.Count + 1, text);
            }
            style = "";
            attributes = "";
        }
        internal string Render()
        {
            if (style == "")
            {
                return $"%^    <{type}{id}{$"{attributes}".Replace("\" ", "\"")}>%^{textCache}%^    <>".Replace("%^    <>", $"    </{type}>");
            }
            return $"%^    <{type}{id}{$"{style}".Replace("\" ", "\"")}>%^{textCache}%^    <>".Replace("%^    <>", $"    </{type}>");
        }
    }
}