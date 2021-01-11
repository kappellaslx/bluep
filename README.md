# BluePrismConsoleApp

The console app logic is splited into 4 main services:

## IEnglishDictionary
Single Service responsible to read all English words that are accepted on our application

## IFileManager
Service that will handle file read/write features needed.

## IWordsProcessorService
Service responsible to check if all words bellong to the English dictionary and only those will be accepted as valid results, it will also sort the words alphabeticly.
The service will search for the start letter and start to comparing the follwing words. The comparison method will check the number of letters that were modified.
If the end word is found the words processor service will finish his job returning the list of words found.

## IMainService
Service to which the console application will pass all arguments properly validated. 
This service will start by reading the dictionary file and order the list of words (filtering words with more than 4 letters).
The second step is to search the start word and after it is found, start checking the number of letters modified until we find the last word.
The last step is to write a file with the list of words that were found.

## References
Used some Google search to find more details over configuring Autofac for console applications and how to use some libraries that I did not remember.
