# CSWeb-lib
## A C# library dedicated to streamlining web development. Made to replace HTML and CSS fluency with a single C# file.
### (still in very early development - use at your own risk!)

## How to Use:

### Input:
```cs
cswebobj obj = new cswebobj("test.html", "test.css");
idstyle style = new idstyle("id");
style.AddRGBColor(255, 0, 0);
obj.AddStyle(style);
obj.AddText("Hello World!", "id");
obj.Render();
```

### Output:
<img src="Images/output.png"></img>
