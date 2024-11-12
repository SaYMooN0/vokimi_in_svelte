using System.Text;
using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_general_test;
using vokimi_api.Src.db_related.db_entities.draft_tests.draft_tests_shared;
using vokimi_api.Src.db_related.db_entities_ids;
using vokimi_api.Src.dtos.requests.test_creation.general_template.question_update;
using vokimi_api.Src.enums;
using vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers;

namespace vokimi_api.Src.db_related
{
    public class DbDataFaker
    {
        private readonly AppDbContext _db;
        private static readonly Random _rnd = new();
        private static readonly string[] _fakeSyllables = ["lip", "nae", "for", "peq", "wOO", "l11", "rwa", "uwu", "dii", "bop"];
        private static readonly string _fakeEmailPostfix = "@fu.fake_user";

        public DbDataFaker(AppDbContext dbContext) { _db = dbContext; }
        public Task AddAdmin() => AddNewFakeUser(_db, "admin@admin.admin", "admin@admin.admin", "adminUser");
        public async Task AddFakeDraftGeneralTestToAdmin() {
            AppUserId creatorId = _db.AppUsers.FirstOrDefault(u => u.Username == "adminUser")?.Id
                ?? throw new Exception($"Admin user not found in the {nameof(AddFakeDraftGeneralTestToAdmin)} method");

            DraftTestMainInfo mainInfo = DraftTestMainInfo.CreateNewFromName("New Fake Draft General Test");
            TestStylesSheet styles = TestStylesSheet.CreateNew();
            DraftGeneralTest test = DraftGeneralTest.CreateNew(creatorId, mainInfo.Id, styles.Id);

            styles.Update("#12db88", ArrowIconType.DefaultCircle);

            TestConclusion conslusion = TestConclusion.CreateNew();
            conslusion.Update(
                $"testConclusion_{GenRndFakeText(60)}",
                null,
                true,
                $"feedback_text {GenRndFakeText(60)}",
                (uint)_rnd.Next(60, 100)
            );
            test.SetConclusion(conslusion);

            _db.DraftTestMainInfo.Add(mainInfo);
            _db.TestStyles.Add(styles);
            _db.DraftTestsSharedInfo.Add(test);
            _db.TestConclusions.Add(conslusion);

            for (ushort i = 0; i < 2; i++) {
                string resultName = $"resName_{GenRndNumString(12)}";
                string resultText = $"resultText_{GenRndFakeText(160)}";
                var result = DraftGeneralTestResult.CreateNew(test.Id, resultName, resultText);
                _db.DraftGeneralTestResults.Add(result);
            }

            for (ushort i = 0; i < _rnd.Next(3, 8); i++) {
                DraftGeneralTestQuestion question = DraftGeneralTestQuestion.CreateNew(
                    test.Id,
                    GeneralTestAnswerType.TextOnly,
                    i
                );

                string questionText = $"question with i={i} text {GenRndFakeText(120)}";
                int answersCount = _rnd.Next(4, 7);
                QuestionWithTextOnlyAnswersUpdateRequest updateData = new(
                    string.Empty,
                    questionText,
                    null,
                    _rnd.Next(0, 2) == 1,
                    GeneralTestAnswerType.TextOnly.GetId(),
                    _rnd.Next(0, 5) == 1,
                    (ushort)_rnd.Next(1, 4),
                    (ushort)_rnd.Next(3, answersCount),
                    i,
                    []
                );
                if (_rnd.Next(0, 8) > 6) {
                    question.UpdateAsSingleChoice(updateData);
                } else {
                    question.UpdateAsMultipleChoice(updateData);
                }
                _db.DraftGeneralTestQuestions.Add(question);
                for (ushort j = 0; j < answersCount; j++) {
                    TextOnlyAnswerTypeSpecificInfo typeSpecificInfo = TextOnlyAnswerTypeSpecificInfo
                        .CreateNew($"answer with i={j} text {GenRndFakeText(60)}");
                    DraftGeneralTestAnswer answer = DraftGeneralTestAnswer.CreateNew(question.Id, j, typeSpecificInfo.Id);
                    _db.AnswerTypeSpecificInfo.Add(typeSpecificInfo);
                    _db.DraftGeneralTestAnswers.Add(answer);
                }
            }

            await _db.SaveChangesAsync();
        }
        public async Task AddFakeUsers(int count) {
            for (int i = 0; i < count; i++) {
                string fakeName = GenRndFakeUserName(i);
                string fakeEmail = GenRndFakeUserEmail(i);
                string password = Guid.NewGuid().ToString();
                await AddNewFakeUser(_db, fakeEmail, password, fakeName);
            }
        }
        private async static Task AddNewFakeUser(
            AppDbContext db,
            string email,
            string password,
            string username
        ) {
            var loginInfo = LoginInfo.CreateNew(email, BCrypt.Net.BCrypt.HashPassword(password));
            var additionalInfo = UserAdditionalInfo.CreateNew(DateOnly.FromDateTime(DateTime.UtcNow));
            var pageSettings = UserPageSettings.CreateNew();
            var user = AppUser.CreateNew(username, loginInfo.Id, additionalInfo.Id, pageSettings.Id);

            db.UserAdditionalInfo.Add(additionalInfo);
            db.LoginInfo.Add(loginInfo);
            db.AppUsers.Add(user);
            db.UserPageSettings.Add(pageSettings);

            await db.SaveChangesAsync();
        }
        private static string GenRndNumString(int n) {
            StringBuilder result = new StringBuilder(n);

            for (int i = 0; i < n; i++) {
                int digit = _rnd.Next(0, 9);
                result.Append(digit);
            }

            return result.ToString();
        }
        private static string GenRndFakeUserName(int userI) {
            StringBuilder fakeName = new();
            for (int j = 0; j < _rnd.Next(2, 5); j++) {
                fakeName.Append(_fakeSyllables[_rnd.Next(_fakeSyllables.Length)]);
            }
            fakeName.Append(userI);
            fakeName.Append(GenRndNumString(4));
            return fakeName.ToString();
        }
        private static string GenRndFakeUserEmail(int userI) {
            StringBuilder fakeEmail = new();
            fakeEmail.Append("fue");
            for (int j = 0; j < _rnd.Next(1, 4); j++) {
                fakeEmail.Append(_fakeSyllables[_rnd.Next(_fakeSyllables.Length)]);
            }
            fakeEmail.Append(userI);
            fakeEmail.Append(_fakeEmailPostfix);
            return fakeEmail.ToString();
        }
        private static string GenRndFakeText(int maxLentgh) {
            StringBuilder t = new();
            for (int j = 0; j < _rnd.Next(2, maxLentgh / 10); j++) {
                t.Append(_fakeSyllables[_rnd.Next(_fakeSyllables.Length)]);
                if (_rnd.Next(0, 10) > 7) {
                    t.Append(" ");
                }
                if (_rnd.Next(0, 11) > 6) {
                    t.Append(GenRndNumString(_rnd.Next(2, 8)));
                }
                if (_rnd.Next(0, 20) > 17) {
                    t.Append('\n');
                }

            }
            return t.ToString();
        }

    }
}
