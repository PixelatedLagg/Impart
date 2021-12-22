namespace Impart.Templates
{
    public static class Article
    {
        public static void AddArticleTemplate(this WebPage obj, params Element[] args)
        {
            string textCache = "%^";
            foreach (Element e in args)
            {
                switch (e.GetType().FullName)
                {
                    case "CSWeb.Text":
                        Text text = (Text)e;
                        if (text.id == null)
                        {
                            textCache += $"    <p{text.attributes}{text.style}>{text.text}</p>%^";
                        }
                        else
                        {
                            textCache += $"    <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>%^";
                        }
                        break;
                    case "CSWeb.Header":
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
                        throw new TemplateError("Article element invalid!");
                }
            }
            obj.textCache += $"{textCache}<>".Replace("%^<>", "");
        }
    }
}