# compared-queryable
## A library dedicated to custom ordering of `IQueryable<T>` objects.

Ever have a list of files, like so...

```
report 1.txt
report 2.txt
report 3.txt
report 4.txt
report 5.txt
report 6.txt
report 7.txt
report 8.txt
report 9.txt
report 10.txt
report 11.txt
report 12.txt
```

...only to find that when calling `.OrderBy` on a `IQueryable<T>` leaves it ordered as such?

```
report 1.txt
report 10.txt
report 11.txt
report 12.txt
report 2.txt
report 3.txt
report 4.txt
report 5.txt
report 6.txt
report 7.txt
report 8.txt
report 9.txt
```

Try instead using the following code to order the `Queryable` naturally.

#### Usage
```
Install-Package ComparedQueryable

var collection = new[]{ new {Name = "report 1.txt"}, new {Name = "report 10.txt"}, new {Name = "report 2.txt"} }
    .AsNaturalQueryable()
    .OrderBy(obj => obj.Name)
    .Select(obj => obj.Name)
    .ToArray(); // Returns new[] {"report 1.txt", "report 2.txt", "report 10.txt"}
```
