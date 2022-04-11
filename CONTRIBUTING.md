# Contributing Rules:
### 1. Stick to the common coding style
As of April 2022, all of the code in Impart is written entirely by me. 
This means that it all follows the same style, and I would appreciate that you also conform to it. Example from Form.cs:
```csharp
namespace Impart
{
    /// <summary>The main input class in Impart.</summary>
    public sealed class Form
    {
        private string textCache;

        /// <summary>Creates a Form instance.</summary>
        /// <returns>A Form instance.</returns>
        public Form()
        {
            textCache = "<form>";
        }

        /// <summary>Add <paramref name="textFields"/> to the Form.</summary>
        /// <param name="textFields">The TextField array to add.</param>
        public Form AddTextField(params TextField[] textFields)
        {
            foreach (TextField tf in textFields)
            {
                textCache += tf.textCache;
            }
            return this;
        }
...
```
### 2. Report secuity vulnerabilities responsibly
Message me on Discord (Pixelated_Lagg#8321) or email me (michiganmii2@gmail.com). ***DO NOT*** open an issue if you think the related bug compromises security.

### 3. Assure there is no merge conflicts
Make sure that your branch has no merge conflicts when eventually merging into the main branch.

### 4. Be nice
Treat others with kindness and respect, we are all contributing to the same codebase!

### Oh and thanks for showing interest in contributing to Impart :)
