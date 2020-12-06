using Moq;
using System.Collections.Generic;
using Xunit;

namespace BluePrismConsoleApp.Tests
{
    public class WordProcessorTests
    {
        private readonly Mock<IEnglishDictionary> englishDict;
        private readonly WordsProcessorService wordsProcessorService;

        public WordProcessorTests()
        {
            this.englishDict = new Mock<IEnglishDictionary>();
            this.englishDict.Setup(d => d.GetEnglishDictionary())
               .Returns(new Dictionary<string, string>
               {
                    { "span", "span" },
                    { "spat", "spat" },
                    { "spin", "spin" },
                    { "spit", "spit" },
                    { "spud", "spud" },
                    { "spot", "spot" },
                    { "sprout", "sprout" },
               });

            this.wordsProcessorService = new WordsProcessorService(this.englishDict.Object);
        }

        /// <summary>
        /// Get filtered words success
        /// </summary>
        [Fact]
        public void GetFilteredWordsSuccess()
        {
            // Arrange
            List<string> words = new List<string> { "Spin", "Spit", "Spat", "Spot", "Span" };

            // Act
            var result = this.wordsProcessorService.ProcessWords(words, "Spin", "Spot");

            // Assert
            Assert.Equal(new List<string> { "Spin", "Spit", "Spot" }, result);
        }

        /// <summary>
        /// Invalid word inside list test
        /// </summary>
        [Fact]
        public void InvalidWordInsideListTest()
        {
            // Arrange
            List<string> words = new List<string> { "Spin", "Spit", "Spat", "Spon", "Spot", "Span" };

            // Act
            var result = this.wordsProcessorService.ProcessWords(words, "Spin", "Spot");

            // Assert
            Assert.Equal(new List<string> { "Spin", "Spit", "Spot" }, result);
        }

        /// <summary>
        /// is valid word check success
        /// </summary>
        [Theory]
        [InlineData("spat")]
        [InlineData("Spat")]
        public void IsValidWordCheckSuccess(string word)
        {
            // Act
            var isValid = this.wordsProcessorService.IsValidWord(word);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// is valid word check failed
        /// </summary>
        [Theory]
        [InlineData("sput")]
        [InlineData("Sput")]
        public void IsValidWordCheckFailed(string word)
        {
            // Act
            var isValid = this.wordsProcessorService.IsValidWord(word);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Compare words success
        /// </summary>
        [Theory]
        [InlineData("spat", "spit")]
        [InlineData("Spat", "spit")]
        public void CompareWordsSuccess(string previousWord, string currentWord)
        {
            // Act
            var isValid = this.wordsProcessorService.CompareWords(previousWord, currentWord);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Compare equal words failed
        /// </summary>
        [Fact]
        public void CompareEqualWordsFailed()
        {
            // Act
            var isValid = this.wordsProcessorService.CompareWords("spat", "spat");

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Compare words failed
        /// </summary>
        [Theory]
        [InlineData("", null)]
        [InlineData("", "spat")]
        [InlineData("spatt", "spat")]
        [InlineData("Spat", "spin")]
        public void CompareWordsFailed(string previousWord, string currentWord)
        {
            // Act
            var isValid = this.wordsProcessorService.CompareWords(previousWord, currentWord);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Sort and filter words
        /// </summary>
        [Fact]
        public void SortAndFilterWords()
        {
            // Act
            var words = this.wordsProcessorService.FilterAndSortWords(new List<string> { "", "Spin", "Spit", "Spat", "Spon", "Spot", "Span", "spott", null });

            // Assert
            Assert.Equal(new List<string> { "Span", "Spat", "Spin", "Spit", "Spon", "Spot" }, words);
        }
    }
}
