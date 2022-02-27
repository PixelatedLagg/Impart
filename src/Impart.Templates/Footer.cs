using System.Text;

namespace Impart.Templates
{
    public static class Footer
    {
        public static void AddFooterTemplate(this WebPage obj, params Element[] args)
        {
            StringBuilder textBuilder = new StringBuilder($"<div style=\"bottom: 0; position: fixed; column-count: {args.Length}; margin-left: 40%; margin-right: 40%;\">");
            foreach (Element e in args)
            {
                switch (e)
                {
                    case Text:
                        Text text = (Text)e;
                        textBuilder.Append($"<p{text.attributeBuilder.ToString()}{text.style.Replace(";\"", ";")}justify-content: center; display: inline;\">{text.text}</p>");
                        break;
                    case Link:
                        Link link = (Link)e;
                        if (link.linkType == typeof(Image))
                        {
                            if (link.style == "")
                            {
                                textBuilder.Append($"<a {link.attributeBuilder.ToString()}style=\"justify-content: center; display: inline;\" href=\"{link.path}\">");
                            }
                            else
                            {
                                textBuilder.Append($"<a {link.attributeBuilder.ToString()}{link.style.Replace(";\"", "; ")}justify-content: center; display: inline;\" href=\"{link.path}\">");
                            }
                            if (link.image.style == "")
                            {
                                textBuilder.Append($"<img {link.image.attributeBuilder.ToString()}src=\"{link.image.path}\" style=\"display: inline;\"></a>");
                            }
                            else
                            {
                                textBuilder.Append($"<img {link.image.attributeBuilder.ToString()}src=\"{link.image.path}\" {link.image.style.Replace(";\"", ";")}display: inline;\"></a>");
                            }
                        }
                        else
                        {
                            if (link.style == "")
                            {
                                textBuilder.Append($"<a {link.attributeBuilder.ToString()}style=\"justify-content: center; display: inline;\" href=\"{link.path}\">");
                            }
                            else
                            {
                                textBuilder.Append($"<a {link.attributeBuilder.ToString()}{link.style.Replace(";\"", "; ")}justify-content: center; display: inline;\" href=\"{link.path}\">");
                            }
                            if (link.text.style == "")
                            {
                                textBuilder.Append($"<{link.text.textType} {link.text.attributeBuilder.ToString()}style=\"display: inline;\"></a>");
                            }
                            else
                            {
                                textBuilder.Append($"<{link.text.textType} {link.text.attributeBuilder.ToString()}{link.text.style.Replace(";\"", ";")}display: inline;\"></a>");
                            }
                        }
                        break;
                    default:
                        throw new ImpartError("Footer element invalid!");
                }
            }
            obj.textBuilder.Append($"{textBuilder.ToString()}</div>");
        }
    }
}