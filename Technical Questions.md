# Technical Questions



## 1. How long did you spend on the coding assignment? What would you add if you had more time?

I spent approximately **[10 hours]** on this assignment.  

If I had more time, I would consider adding:

- Caching for external API responses to reduce latency.

- Comprehensive error handling and retry mechanisms for API failures.

- A more robust health check system for both CoinMarketCap and ExchangeRates APIs.

- Logging and metrics for monitoring in production.

- Adding a lightweight database like SQLite, or a more robust option such as PostgreSQL if needed, to store search history and display a list of recent searches.



## 2. Most useful feature added to the latest version of C# (C# 14)

One of the features introduced in **C# 14** that caught my attention is Extension Members.
They extend the idea of extension methods by allowing developers to add properties, operators, and even static members to existing types without modifying their source code.

I have not yet used Extension Members in production code. At this stage, I see them as a powerful but potentially double-edged feature. While they can significantly improve expressiveness and reduce boilerplate, overusing them may also lead to less discoverable APIs and cluttered codebases if not applied with clear conventions.

For that reason, I prefer to evaluate new language features carefully before adopting them, especially in shared or long-lived codebases. That said, I find Extension Members promising for well-scoped scenarios such as domain-specific helpers or infrastructure code.

Example (Extension Member - shown purely for conceptual clarity; not used in this assignment or other projects):

```csharp

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> enumerable)
    {
        // Extension property
        public bool IsEmpty => !enumerable.Any();

        // Extension method
        public T FirstOrFallback(T fallback)
            => enumerable.FirstOrDefault() ?? fallback;
        
        // Extension static method
        public static IEnumerable<T> Range(int start, int count, Func<int, T> generator)
            => Enumerable.Range(start, count).Select((_, i) => generator(i));
        
        // Extension operator method
        public static IEnumerable<T> operator +(IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (var item in first)
                yield return item;
            foreach (var item in second)
                yield return item;
        }
    }
}

// Usage:
// Check if list is empty using extension property
var emptyList = new List<string>();
if (emptyList.IsEmpty)
    // ...

```




## 3. How would you track down a performance issue in production?

In production, I usually start by narrowing the problem using metrics and logs to identify where the slowdown happens, then switch to hands-on debugging and profiling.

Depending on the environment, I’ve used different tools: IDE debuggers (Visual Studio, Rider), memory and CPU profilers (Visual Studio .NET Profiler, dotMemory, ANTS), SQL Server Profiler for database bottlenecks, and browser profilers for client-side issues.

For more complex or intermittent problems, I’ve analyzed crash dumps using tools like WinDbg to understand failures that couldn’t be reproduced locally.

In my experience, performance issues are rarely solved by a single tool; they require combining observability, profiling, and low-level debugging based on the nature of the problem.



## 4. Latest technical book or conference attended

- ASP.NET Core in Action, Third Edition – Provides a clear, practical guide to building modern web APIs and services with ASP.NET Core.

- C# 12 in a Nutshell by Joseph Albahari – Excellent reference for understanding the latest C# features, syntax, and patterns.

- Clean Architecture with .NET by Dino Esposito – Explains architectural principles and clean design with .NET in a very accessible way, with concrete examples.


## 5. Thoughts on this technical assessment

This assessment is well-structured, covering:

- API integration
- Unit testing
- Practical use of .NET Web API

It allowed me to demonstrate design decisions, coding style, and testability considerations.



## 6. Describe yourself using JSON


```json

{

 "name": "Ali",
 "role": ["System Analyst", "Software Developer"],
 "languages": ["C#", "SQL", "JavaScript", "C++"],
 "interests": ["System design", "Architecture",  "Systems and low-level programming", "Cryptography", "AI"],
 "experienceYears": 27,
 "location": "IR",
 "traits": ["curious", "methodical", "detail-oriented", "analytical", "lifelong learner"]

}

```

