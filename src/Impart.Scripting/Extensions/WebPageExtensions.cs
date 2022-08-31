using System;
using Impart.Internal;

namespace Impart.Scripting
{
    /// <summary>Extensions to the WebPage class for client-side scripting.</summary>
    public static class WebPageScripting
    {
        /// <summary>Set an event.</summary>
        /// <param name="webPage">The WebPage to add the event.</param>
        /// <param name="eventType">The EventType of the event.</param>
        /// <param name="functions">The IFunction(s) to call when the event is triggered.</param>
        public static void Set(this WebPage webPage, EventType eventType, params IFunction[] functions)
        {
            webPage._Script.Functions.Add(new Invoke($"document.body.addEventListener('{ScriptingExtensions.GetEventName(eventType)}', (x) => {{{String.Concat<IFunction>(functions)}}}, false);"));
            webPage.Changed = true;
        }

        /// <summary>Get an event.</summary>
        /// <param name="webPage">The WebPage to get the event from.</param>
        /// <param name="attrType">The AttrType that is changed in the event.</param>
        /// <param name="args">The Attr value(s) to assign to the AttrType in the event.</param>
        public static Edit Get(this WebPage webPage, AttrType attrType, params object[] args)
        {
            return new Edit($"document.body.{ScriptingExtensions.GetEdit(attrType, args)}");
        }

        /// <summary>Add an IFunction to the WebPage's Script.</summary>
        /// <param name="webPage">The WebPage to add the IFunction to its Script.</param>
        /// <param name="functions">The IFunction(s) to add to the WebPage's Script.</param>
        public static void Add(this WebPage webPage, params IFunction[] functions)
        {
            webPage._Script.Functions.AddRange(functions);
            webPage.Changed = true;
        }

        /// <summary>Remove an IFunction from the WebPage's Script.</summary>
        /// <param name="webPage">The WebPage to remove the IFunction from its Script.</param>
        /// <param name="functions">The IFunction(s) to remove from the WebPage's Script.</param>
        public static void Remove(this WebPage webPage, params IFunction[] functions)
        {
            foreach (IFunction function in functions)
            {
                webPage._Script.Functions.Remove(function);
            }
            webPage.Changed = true;
        }

        /// <summary>Clear all IFunctions from the WebPage's Script.</summary>
        /// <param name="webPage">The WebPage to clear all IFunctions from its Script.</param>
        public static void Clear(this WebPage webPage)
        {
            webPage._Script.Functions.Clear();
            webPage.Changed = true;
        }
    }
}