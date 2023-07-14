Console.WriteLine("Welcome, this is a demo to demostrate how effictive HashSets are to search vs Lists.");
Console.WriteLine("We are going to test this theory by pileing thousands of Guids into HashSets and Lists. And then searching for them over and over again through async methods efficively 'racing' them. The 'winning' method will annouce it's success slo you can see the order of success.");

bool wasSuccessfullInParsing = false;
int numberOfGuids = 0;

// Create a Guid to search for.
Guid guidToSearchFor = Guid.NewGuid();

List<Guid> guidList = new List<Guid>();
HashSet<Guid> guidSet = new HashSet<Guid>();

guidList.Add(guidToSearchFor);
guidSet.Add(guidToSearchFor);

do
{
Console.Write("\nPlease enter the number of Guids x 1,000 to enter into the lists: ");
    wasSuccessfullInParsing = int.TryParse(Console.ReadLine(), out numberOfGuids);
    if (!wasSuccessfullInParsing)
    {
        Console.WriteLine("Was unsuccessfull in parsing value... Try Again.");
    }
    else
    {
        numberOfGuids *= 1_000;
    }
} while (!wasSuccessfullInParsing);

// Add the Guids to the list and hashSet
Console.WriteLine($"Adding {numberOfGuids} guids to both the hashset and to the list.");


for (int guidIndex = 0; guidIndex < numberOfGuids; guidIndex++)
{
    Guid guid = Guid.NewGuid();
    guidList.Add(guid);
    guidSet.Add(guid);

    // We want to simulate the worst case for both to properly test the edge case. That would be the last Guid in the List.
    if ((guidIndex + 1) == numberOfGuids)
    {
        guidToSearchFor = guid;
    }
}

Console.WriteLine("Finished writing guids.");

// Search the lists
async Task SearchListTenThousandTimes()
{
    await Task.Yield();
    for (int i = 0; i < 10_000; i++)
    {
        guidList.Contains(guidToSearchFor);
    }
    Console.WriteLine("List Wins!");
}

async Task SearchHashSetTenThousandTimes()
{
    await Task.Yield();
    for (int i = 0; i < 10_000; i++)
    {
        guidList.Contains(guidToSearchFor);
    }
    Console.WriteLine("HashSet Wins!");
}
List<Task> taskList = new List<Task>();
 SearchListTenThousandTimes();
await SearchHashSetTenThousandTimes();

