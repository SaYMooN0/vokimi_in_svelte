using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.constants_store_classes
{
    public class ImgOperationsConsts
    {
        //folders
        public const string
            GeneralFolder = "general",
            //users
            ProfilePicturesFolder = "profile_pictures",
            //draft tests
            DraftTestCoversFolder = "draft_tests_covers",
            // draft general tests
            DraftTestQuestionsFolder = "draft_general_tests_questions",
            DraftGeneralTestAnswersFolder = "draft_general_tests_answers";

        public static string DefaultTestCoverImg => $"{GeneralFolder}/test_cover_default.webp";
        public static string DefaultProfilePicture => $"{GeneralFolder}/default_profile_picture.webp";

        public static string ImgUrl(string fileKey) =>
           $"vokimiimgs/{fileKey}";
        public static string ImageUrlWithVersion(string path) =>
            $"{ImgUrl(path)}?v={Guid.NewGuid()}";
        public const int MaxImageSizeInBytes = 3 * 1024 * 1024;
        public const int MaxImageSizeInMB = 3;
    }
}
