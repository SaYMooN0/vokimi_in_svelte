using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Helpers
{
    public static class ImgOperationsHelper
    {
        public static string ExtractFileExtension(IFormFile file) => Path.GetExtension(file.FileName);
        private static string CombineStoragePath(params string[] paths) =>
            Path.Combine(paths).Replace('\\', '/');
        public static string PublishedTestImgsParentFolder(TestId testId)
            => CombineStoragePath(ImgOperationsConsts.PublishedTestsFolderName, testId.Value.ToString());
        public static string GetPublishedTestCoverImgKey(
            TestId testId,
            string imgExtension
        ) => CombineStoragePath(
            PublishedTestImgsParentFolder(testId),
            ImgOperationsConsts.TestCoverFileName +
            imgExtension
        );
        public static string GetPublishedGeneralTestResultImgKey(
            TestId testId,
            GeneralTestResultId resultId,
            string imgExtension
        ) => CombineStoragePath(
            PublishedTestImgsParentFolder(testId),
            ImgOperationsConsts.ResultsSubFolderName,
            resultId.Value.ToString() + imgExtension
        );
        public static string GetPublishedGeneralTestQuestionImgKey(
            TestId testId,
            GeneralTestQuestionId questionId,
            string imgExtension
        ) => CombineStoragePath(
            PublishedTestImgsParentFolder(testId),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.QuestionImgFileName.ToString() + imgExtension
        );
        public static string GetPublishedGeneralTestAnswerImgKey(
            TestId testId,
            GeneralTestQuestionId questionId,
            string imgExtension
        ) => CombineStoragePath(
            PublishedTestImgsParentFolder(testId),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.AnswersSubFolderName,
            Guid.NewGuid().ToString() + imgExtension
        );
        //draft tests
        public static string DraftTestImgsParentFolder(DraftTestId testId)
            => CombineStoragePath(ImgOperationsConsts.DraftTestsFolderName, testId.Value.ToString());
        public static string GetDraftTestCoverImgKey(
           DraftTestId testId,
           string imgExtension
        ) => CombineStoragePath(
            DraftTestImgsParentFolder(testId),
            ImgOperationsConsts.TestCoverFileName + imgExtension
        );

        //draft general tests
        public static string GetDraftGeneralTestResultImgKey(
            DraftTestId testId,
            DraftGeneralTestResultId resultId,
            string imgExtension
        ) => CombineStoragePath(
            DraftGeneralTestResultsFolder(testId, resultId),
            Guid.NewGuid().ToString() + imgExtension
        );
        public static string DraftGeneralTestResultsFolder(
            DraftTestId testId,
            DraftGeneralTestResultId resultId
        ) => CombineStoragePath(
            DraftTestImgsParentFolder(testId),
            ImgOperationsConsts.ResultsSubFolderName,
            resultId.Value.ToString()
        );
        public static string GetDraftGeneralTestQuestionImgKey(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId,
            string imgExtension
        ) => CombineStoragePath(
            DraftGeneralTestQuestionsFolder(testId, questionId),
            Guid.NewGuid() + imgExtension
        );
        public static string DraftGeneralTestQuestionsFolder(
           DraftTestId testId,
           DraftGeneralTestQuestionId questionId
       ) => CombineStoragePath(
           DraftTestImgsParentFolder(testId),
           ImgOperationsConsts.QuestionsSubFolderName,
           questionId.ToString()
       );
        public static string GetDraftGeneralTestAnswerImgKey(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId,
            string imgExtension
        ) => CombineStoragePath(
            DraftGeneralTestAnswersFolder(testId, questionId),
            Guid.NewGuid().ToString() + imgExtension
        );
        public static string DraftGeneralTestAnswersFolder(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId
        ) => CombineStoragePath(
            DraftTestImgsParentFolder(testId),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.AnswersSubFolderName
        );
        public static string GetProfilePicImgKey(
            AppUserId userId,
            string imgExtension
        ) => CombineStoragePath(
            ImgOperationsConsts.ProfilePicturesFolder,
            userId.Value.ToString(),
            ImgOperationsConsts.ProfilePic + imgExtension
        );
    }
}
