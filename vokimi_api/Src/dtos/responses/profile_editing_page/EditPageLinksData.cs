using vokimi_api.Src.db_related.db_entities.users;
using vokimi_api.Src.enums;

namespace vokimi_api.Src.dtos.responses.profile_editing_page
{
    public record class EditPageLinksData(
        string? Telegram,
        string? YouTube,
        string? Facebook,
        string? X,
        string? Instagram,
        string? GitHub,
        string? Other1,
        string? Other2,
        PrivacyValues LinksPrivacy
    )
    {
        public static EditPageLinksData FromUserAdditionalInfo(UserAdditionalInfo info) => new(
            info.Links.Telegram,
            info.Links.YouTube,
            info.Links.Facebook,
            info.Links.X,
            info.Links.Instagram,
            info.Links.GitHub,
            info.Links.Other1,
            info.Links.Other2,
            info.PrivacySettings.LinksPrivacy
        );
    }
}
