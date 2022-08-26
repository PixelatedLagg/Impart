using Impart.Internal;

namespace Impart.Scripting
{
    public static class Element
    {
        public static Edit GetMany(ElementType type, AttrType attrType, params object[] args)
        {
            return new Edit($@"Array.from(document.getElementsByTagName('{type switch
            {
                ElementType.RegularText => "p",
                ElementType.BoldText => "b",
                ElementType.DeleteText => "del",
                ElementType.EmphasizeText => "em",
                ElementType.ImportantText => "strong",
                ElementType.InsertText => "ins",
                ElementType.ItalicText => "i",
                ElementType.MarkText => "mark",
                ElementType.SmallText => "small",
                ElementType.SubscriptText => "sub",
                ElementType.SuperscriptText => "sup",
                ElementType.OrderedList => "ol",
                ElementType.UnorderedList => "ul",
                ElementType.Link => "a",
                ElementType.Image => "img",
                ElementType.Header1 => "h1",
                ElementType.Header2 => "h2",
                ElementType.Header3 => "h3",
                ElementType.Header4 => "h4",
                ElementType.Header5 => "h5",
                ElementType.EFrame => "iframe",
                ElementType.Division => "div",
                ElementType.Button => "button",
                ElementType.Video => "video",
                ElementType.TableRow => "tr",
                ElementType.Table => "table",
                ElementType.Form => "form",
                ElementType.FormInput => "input",
                _ => throw new ImpartError("Invalid type parameters.")
            }}')).forEach(e => {{e.{ScriptingExtensions.GetEdit(attrType, args)}}});");
        }

        public static Edit Get(ElementType type, AttrType attrType, params object[] args)
        {
            return new Edit($@"document.getElementsByTagName('{type switch
            {
                ElementType.RegularText => "p",
                ElementType.BoldText => "b",
                ElementType.DeleteText => "del",
                ElementType.EmphasizeText => "em",
                ElementType.ImportantText => "strong",
                ElementType.InsertText => "ins",
                ElementType.ItalicText => "i",
                ElementType.MarkText => "mark",
                ElementType.SmallText => "small",
                ElementType.SubscriptText => "sub",
                ElementType.SuperscriptText => "sup",
                ElementType.OrderedList => "ol",
                ElementType.UnorderedList => "ul",
                ElementType.Link => "a",
                ElementType.Image => "img",
                ElementType.Header1 => "h1",
                ElementType.Header2 => "h2",
                ElementType.Header3 => "h3",
                ElementType.Header4 => "h4",
                ElementType.Header5 => "h5",
                ElementType.EFrame => "iframe",
                ElementType.Division => "div",
                ElementType.Button => "button",
                ElementType.Video => "video",
                ElementType.TableRow => "tr",
                ElementType.Table => "table",
                ElementType.Form => "form",
                ElementType.FormInput => "input",
                _ => throw new ImpartError("Invalid type parameters.")
            }}')[0].{ScriptingExtensions.GetEdit(attrType, args)}");
        }
    }
}