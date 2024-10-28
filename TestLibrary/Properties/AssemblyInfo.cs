using NUnit.Framework; // Required for NUnit parallel configuration

// Enable parallel execution at the fixture level in NUnit
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Set the level of parallelism to control the number of concurrent threads
[assembly: LevelOfParallelism(4)] // Adjust this number based on your system capacity
