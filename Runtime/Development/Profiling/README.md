# Profiling

It measures in a simple way the time it takes for a block of code to execute, or the memory it consumes.

```c#
using (Profiling.Time("Some slow code"))
{
    ...
}
```

Output the message: "**Task 'Some slow code' took 27.66ms (0 frames)**"

```c#
using (Profiling.Memory("Some hungry code"))
{
    ...
}
```

Output the message: "**Task 'Some hungry code' consume 4.00 kb**".
