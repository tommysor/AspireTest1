using DomainSpesificLanguage;

namespace Spesification.Stories
{
    /// <summary>
    /// As a user
    /// I want to try out the search functionality
    /// So that I can decide if the search would be useful for me
    /// </summary>
    public class SearchTestStory : IClassFixture<UserFixture>
    {
        private readonly User _user;

        public SearchTestStory(UserFixture userFixture)
        {
            _user = userFixture.User;
        }

        [Fact(Skip = "Not implemented")]
        public async Task ShouldHaveAListOfTestData()
        {
            // When
            await _user.GoToSearchTest();
            // Then
            await _user.AssertSearchTestHaveTestData();
        }
    }
}
