using System;
using Impart.Internal;

namespace Impart.Scripting
{
    /// <summary>Extensions to the WebPage class for the purpose of adding events.</summary>
    public static class WebPageScripting
    {
        /// <summary>Set an event.</summary>
        /// <param name="webPage">The WebPage to add the event.</param>
        /// <param name="eventType">The EventType of the event.</param>
        /// <param name="functions">The IFunction(s) to call when the event is triggered.</param>
        public static void Set(this WebPage webPage, EventType eventType, params IFunction[] functions)
        {
            webPage._Script.Functions.Add(new Invoke($"document.body.addEventListener('{ScriptingExtensions.GetEventName(eventType)}', (x) => {{{String.Concat<IFunction>(functions)}}}, false);"));
        }

        /// <summary>Get an event.</summary>
        /// <param name="webPage">The WebPage to get the event from.</param>
        /// <param name="attrType">The AttrType that is changed in the event.</param>
        /// <param name="args">The Attr value(s) to assign to the AttrType in the event.</param>
        public static Edit Get(this WebPage webPage, AttrType attrType, params object[] args)
        {
            return new Edit($"document.body.{ScriptingExtensions.GetEdit(attrType, args)}");
        }
    }
}