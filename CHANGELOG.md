# March 12th (2.0.1) Total Downloads: 519
At this point, Impart is really taking shape and I am looming closer to finishing HTML/CSS generation. Scripting events is still being researched, as I need to find the quickest way to transfer the events directly to the server with only C#. Website hosting is complete and the developer is able to easily host a webserver and switch between webpages.

Added:
<ul>
  	<li>Website hosting</li>
  	<li>Website events</li>
  	<li>No more developer HTML/CSS interaction</li>
</ul>

# March 31th (2.0.2) Total Downloads: 572
Impart is expanding to API and networking along with general formatting now. Website functionality is put on hold as I further develop the networking portion of Impart. Everything is sanitized with the exception of attributes, but I will eventually find a way to fix that. More juicy updates to come as I include more and more features to make Impart a fair contender with the existing ASP.NET.

Added:
<ul>
	<li>JSON format</li>
	<li>REST api hosting</li>
	<li>SOAP api hosting</li>
	<li>Nested element</li>
</ul>

# April 30th (2.0.3) Total Downloads: 722
Impart has started to embrace a production-ready state. Lots of bug fixes and some big additions to the codebase are only the beginning. Additionally, I have drastically shortened the load times of WebPages with a simple, yet powerful caching system.

Added:
<ul>
	<li>Table element</li>
	<li>Standardised documentation</li>
	<li>Massive overhaul to element rendering</li>
	<li>Cache system in each rendered object</li>
	<li>Animation and framing system</li>
	<li>Attribute system for styling</li>
	<li>External attribute system for HTML attributes</li>
</ul>

# May 31th (2.0.4) Total Downloads: 889
Many quality-of-life features added. Impart is slowly making the decision for more developer freedom instead of better performance, although optimisations will definitely come later on. Overhauls galore, big changes possibly coming soon.

Added:
<ul>
	<li>Integration of Animations</li>
	<li>More implicit conversions</li>
	<li>Text alignment attribute</li>
	<li>Fixed many bugs</li>
	<li>Parameterless contructors for Elements</li>
	<li>Element cloning</li>
	<li>Fixes to documentation</li>
	<li>Time measurements</li>
	<li>AnimationArgs</li>
	<li>Constant color variables for HSL/HEX</li>
	<li>ViewWidth and ViewHeight</li>
	<li>BackgroundArgs</li>
	<li>Changed the internal ID system for FormField Elements</li>
	<li>BorderArgs</li>
	<li>Overhaul to Attribute system</li>
	<li>AttrList and ExtAttrList</li>
	<li>Renamed longer names to shorter ones</li>
</ul>

# July 1st (2.0.5) Total Downloads: 1.2K
For this update, I slowed down just a bit. I have random additions planned in the near future, but it is likely going to be more optimisations until I go ahead and add a giant new feature.

Added:
<ul>
	<li>Video Playing</li>
	<li>Custom fonts via Styles</li>
	<li>Custom font Attribute</li>
	<li>Global Styles</li>
	<li>Global external Styles</li>
	<li>Force WebPage rendering</li>
	<li>Removing Style and external Style via index</li>
	<li>Various documentation corrections</li>
</ul>

# July 28th (2.0.6) Total Downloads: 1.3K
Impart is gradually turning more into an established library with code management. For starters, I've finally added a development branch and made the old main branch into a more stable one. Additionally, code analysis has been enabled for the main branch to weed out any wonky code I might have kept in. I am also shaping the final product of Impart. This library isn't meant to handle JSON/XML, it never was. Focus will shift on improving what everyone came here for: websites.

Added:
<ul>
	<li>Global Font management</li>
	<li>Integration of actual Font files</li>
	<li>Nesting of all Elements</li>
	<li>EFrame Element</li>
	<li>Overhaul of Website Fields</li>
	<li>WebsiteRequestArgs</li>
	<li>Pausing/Resuming of Timer</li>
	<li>Start of using compile-time constant LOGGING for extra logs</li>
	<li>Logger system</li>
	<li>Various documentation corrections</li>
</ul>

Removed:
<ul>
	<li>Entire data format namespace</li>
	<li>Excessive response parsing for Website</li>
	<li>Xunit dependency</li>
</ul>
