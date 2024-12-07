using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPagePrivacySettingsSectionData(
        string RealName,
        string RegistrationDate,
        string Birthdate,
        string PublishedTest,
        string Friends,
        string Followers,
        string Followings,
        string Links
    )
    {
        public static EditPagePrivacySettingsSectionData FromUser(AppUser user) => new(
            user.UserAdditionalInfo.PrivacySettings.RealNamePrivacy.GetId(),
            user.UserAdditionalInfo.PrivacySettings.RegistrationDatePrivacy.GetId(),
            user.UserAdditionalInfo.PrivacySettings.BirthDatePrivacy.GetId(),
            user.UserPageSettings.PrivacySettings.PublishedTests.GetId(),
            user.UserPageSettings.PrivacySettings.Friends.GetId(),
            user.UserPageSettings.PrivacySettings.Followers.GetId(),
            user.UserPageSettings.PrivacySettings.Followings.GetId(),
            user.UserAdditionalInfo.PrivacySettings.LinksPrivacy.GetId()
        );
    }
    public record TypedEditPagePrivacySettings(
        PrivacyValues realName,
        PrivacyValues RegistrationDate,
        PrivacyValues Birthdate,
        PrivacyValues PublishedTest,
        PrivacyValues Friends,
        PrivacyValues Followers,
        PrivacyValues Followings,
        PrivacyValues Links
    )
    {
        public static TypedEditPagePrivacySettings FromDictionary(Dictionary<string, string> dict) {
            PrivacyValues realName = PrivacyValues.Anyone;
            if (dict.TryGetValue("realName", out var privacyString)) {
                realName = PrivacyValuesExtensions.FromId(privacyString) ?? realName;
            }

            PrivacyValues registrationDate = PrivacyValues.Anyone;
            if (dict.TryGetValue("registrationDate", out privacyString)) {
                registrationDate = PrivacyValuesExtensions.FromId(privacyString) ?? registrationDate;
            }

            PrivacyValues birthDate = PrivacyValues.Anyone;
            if (dict.TryGetValue("birthdate", out privacyString)) {
                birthDate = PrivacyValuesExtensions.FromId(privacyString) ?? birthDate;
            }

            PrivacyValues publishedTest = PrivacyValues.Anyone;
            if (dict.TryGetValue("publishedTest", out privacyString)) {
                publishedTest = PrivacyValuesExtensions.FromId(privacyString) ?? publishedTest;
            }

            PrivacyValues friends = PrivacyValues.Anyone;
            if (dict.TryGetValue("friends", out privacyString)) {
                friends = PrivacyValuesExtensions.FromId(privacyString) ?? friends;
            }

            PrivacyValues followers = PrivacyValues.Anyone;
            if (dict.TryGetValue("followers", out privacyString)) {
                followers = PrivacyValuesExtensions.FromId(privacyString) ?? followers;
            }

            PrivacyValues followings = PrivacyValues.Anyone;
            if (dict.TryGetValue("followings", out privacyString)) {
                followings = PrivacyValuesExtensions.FromId(privacyString) ?? followings;
            }

            PrivacyValues links = PrivacyValues.Anyone;
            if (dict.TryGetValue("links", out privacyString)) {
                links = PrivacyValuesExtensions.FromId(privacyString) ?? links;
            }

            return new TypedEditPagePrivacySettings(
                realName,
                registrationDate,
                birthDate,
                publishedTest,
                friends,
                followers,
                followings,
                links
            );
        }

    }

}
