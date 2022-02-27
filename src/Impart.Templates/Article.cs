namespace Impart.Templates
{
    public static class Article
    {
        public static void AddArticleTemplate(this WebPage obj, params Element[] args)
        {
            string textCache = "%^";
            foreach (Element e in args)
            {
                switch (e)
                {
                    case Text:
                        Text text = (Text)e;
                        if (text.id == null)
                        {
                            textCache += $"    <p{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>%^";
                        }
                        else
                        {
                            textCache += $"    <p id=\"{text.id}\"{text.attributeBuilder.ToString()}{text.style}>{text.text}</p>%^";
                        }
                        break;
                    case Header:
                        Header header = (Header)e;
                        if (header.id == null)
                        {
                            textCache += $"    <h{header.attributes}{header.style}>{header.text}</h>%^";
                        }
                        else
                        {
                            textCache += $"    <h id=\"{header.id}\"{header.attributes}{header.style}>{header.text}</h>%^";
                        }
                        break;
                    default:
                        throw new ImpartError("Article element invalid!");
                }
            }
            obj.textBuilder.Append($"{textCache}<>".Replace("%^<>", ""));
        }
    }
}