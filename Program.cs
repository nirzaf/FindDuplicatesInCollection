// See https://aka.ms/new-console-template for more information

using Bogus;

using FindDuplicatesInCollection;

Console.WriteLine("Searching duplicates in a list");

var f = new Faker<Patient>()
    .RuleFor(p => p.Id, f => f.IndexGlobal)
    .RuleFor(p => p.Name, f => f.Name.FullName())
    .RuleFor(p => p.Surname, f => f.Name.LastName())
    .RuleFor(p => p.Age, f => f.Random.Int(20, 60))
    .RuleFor(p => p.Address, f => f.Address.StreetAddress())
    .RuleFor(p => p.City, f => f.Address.City())
    .RuleFor(p => p.Country, f => f.Address.Country())
    .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber())
    .RuleFor(p => p.Email, f => f.Internet.Email());
    
var patients = f.Generate(1000);

//find the duplicates by Age from List of Patients using LINQ
var duplicatesByAge = patients.GroupBy(p => p.Age)
    .Where(g => g.Count() > 1)
    .Select(g => g.Key)
    .ToList();

//find the duplicates by any property from List of Patients using LINQ
var duplicatesByAnyProperty = patients.GroupBy(p => new { p.Surname, p.Age })
    .Where(g => g.Count() > 1)
    .Select(g => g.Key)
    .ToList();


if(duplicatesByAnyProperty.Count > 0)
{
    Console.WriteLine("Duplicates found:");
    foreach (var duplicate in duplicatesByAnyProperty)
    {
        Console.WriteLine(duplicate);
    }
}
else
{
    Console.WriteLine("No duplicates found");
}


Console.ReadLine();