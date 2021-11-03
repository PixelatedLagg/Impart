namespace CSWeb.Templates
{
    public static class Footer
    {
        public static void AddFooterTemplate(this cswebobj obj, params Element[] args)
        {
            Timer.StartTimer();
            string textCache = $"%^    <div style=\"bottom: 0; position: fixed; column-count: {args.Length}; margin-left: 40%; margin-right: 40%;\">%^";
            foreach (Element e in args)
            {
                textCache += $"        <div>%^";
                switch (e.GetType().FullName)
                {
                    case "CSWeb.Text":
                        Text text = (Text)e;
                        if (text.id == null)
                        {
                            textCache += $"            <p{text.attributes}{text.style.Replace(";\"", "; ")}justify-content: center; display: inline;\">{text.text}</p>%^";
                        }
                        else
                        {
                            textCache += $"            <p id=\"{text.id}\"{text.attributes}{text.style.Replace(";\"", "; ")}justify-content: center; display: inline;\">{text.text}</p>%^";
                        }
                        break;
                    case "CSWeb.Link":
                        Link link = (Link)e;
                        if (link.image != null)
                        {
                            switch (link.id, link.image.id)
                            {
                                case (null, null):
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\">%^                <img src=\"{link.image.path}\" {link.image.style.Replace(";\"", "; ")} display: inline;\">%^            </a>%^";
                                    break;
                                case (string, string) a when a.Item1 != null && a.Item2 != null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\" id=\"{link.id}\">%^                <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style.Replace(";\"", "; ")} display: inline;\">%^            </a>%^";
                                    break;
                                case (string, string) b when b.Item1 == null && b.Item2 != null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\">%^                <img src=\"{link.image.path}\" id=\"{link.image.id}\" {link.image.style.Replace(";\"", "; ")} display: inline;\">%^            </a>%^";
                                    break;
                                case (string, string) c when c.Item1 != null && c.Item2 == null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\" id=\"{link.id}\">%^                <img src=\"{link.image.path}\" {link.image.style.Replace(";\"", "; ")} display: inline;\">%^            </a>%^";
                                    break;
                            }
                        }
                        else
                        {
                            switch (link.id, link.text.id)
                            {
                                case (null, null):
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\">%^                <p style=\"display: inline;\">{link.text.text}</p>%^            </a>%^";
                                    break;
                                case (string, string) a when a.Item1 != null && a.Item2 != null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\" id=\"{link.id}\">%^                <p style=\"display: inline;\" id=\"{link.image.id}\">{link.text.text}</p>%^            </a>%^";
                                    break;
                                case (string, string) b when b.Item1 == null && b.Item2 != null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\">%^                <p style=\"display: inline;\" id=\"{link.image.id}\">{link.text.text}</p>%^            </a>%^";
                                    break;
                                case (string, string) c when c.Item1 != null && c.Item2 == null:
                                    textCache += $"            <a style=\"justify-content: center; display: inline;\" href=\"{link.path}\" id=\"{link.id}\">%^                <p style=\"display: inline;\">{link.text.text}</p>%^            </a>%^";
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new TemplateError("Footer element invalid!");
                }
                textCache += $"        </div>%^";
            }
            obj.textCache += $"{textCache}%^    </div>".Replace("%^    </div>", "    </div>");
            Debug.CallObjectEvent("[cswebobj] added footer template");
        }
    }
}