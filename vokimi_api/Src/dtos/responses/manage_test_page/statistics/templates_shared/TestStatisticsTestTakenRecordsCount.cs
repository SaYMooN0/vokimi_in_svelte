using vokimi_api.Src.db_related.db_entities;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.dtos.responses.manage_test_page.statistics.templates_shared
{
    public record class TestStatisticsTestTakenRecordsCount(
        int All,
        int ByFollowers,
        int ByFriends,
        int LastHour,
        int LastDay,
        int LastMonth,
        int LastYear
    )
    {
        public static TestStatisticsTestTakenRecordsCount Create(
            ICollection<BaseTestTakenRecord> testTakings,
            HashSet<AppUserId> creatorsFollowers,
            HashSet<AppUserId> creatorFriends
        ) => new(
            testTakings.Count(),
            testTakings.Where(x => x.UserId is not null && creatorsFollowers.Contains(x.UserId.Value)).Count(),
            testTakings.Where(x => x.UserId is not null && creatorFriends.Contains(x.UserId.Value)).Count(),
            testTakings.Count(x => x.Date > DateTime.Now - TimeSpan.FromHours(1)),
            testTakings.Count(x => x.Date > DateTime.Now - TimeSpan.FromDays(1)),
            testTakings.Count(x => x.Date > DateTime.Now - TimeSpan.FromDays(30)),
            testTakings.Count(x => x.Date > DateTime.Now - TimeSpan.FromDays(365))
        );
    }
}
