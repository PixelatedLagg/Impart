## Optimizations:
## To help contributors optimize the code, I have implemented a log system to log command times.

### You can either use the Action<> event, or choose to write the logs to the default debug file (DEBUG.txt).

### Using the Action<> event:
### Input:
```cs
static void Main(string[] args)
{
    Debug.ObjectEvent += EventMethod; //Attaching the event to the event method
    cswebobj obj = new cswebobj("test.html", "test.css");
    obj.AddText("Test", null);
    obj.Render();
}
static void EventMethod(Log log) //Event method
{
    Console.WriteLine($"{log.log} [{log.ms}ms]");
}
```
### Output:
```
[cswebobj] created cswebobj [0.1953ms]
[cswebobj] added text element [0.0101ms]
[cswebobj] rendered cswebobj [4.0713ms]
```

### Writing the logs to a file:
### Input:
```cs
static void Main(string[] args)
{
    Debug.FileLogging = true; //Setting the file logging bool to true (default is false)
    cswebobj obj = new cswebobj("test.html", "test.css");
    obj.AddText("Test", null);
    obj.Render();
}
```
### DEBUG.txt:
```
-- DEBUG FILE --
[cswebobj] created cswebobj [0.2632ms]
[cswebobj] added text element [0.0099ms]
[cswebobj] rendered cswebobj [1.1001ms]
```
