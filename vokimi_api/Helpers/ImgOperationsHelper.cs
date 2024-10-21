using Amazon.S3.Model;
using System.Net;
using vokimi_api.Services;
using vokimi_api.Src.constants_store_classes;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Helpers
{
    public static class ImgOperationsHelper
    {
        public static string ExtractFileExtension(IFormFile file) => Path.GetExtension(file.FileName);
        public static string GetPublishedTestCoverImgKey(
            TestId testId,
            string imgExtension
        ) => Path.Combine(
            ImgOperationsConsts.PublishedTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.TestCoverFileName
        );
        public static string GetPublishedGeneralTestResultImgKey(
            TestId testId,
            GeneralTestResultId resultId,
            string imgExtension
        ) => Path.Combine(
            ImgOperationsConsts.PublishedTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.ResultsSubFolderName,
            resultId.Value.ToString() + imgExtension
        );
        public static string GetPublishedGeneralTestQuestionImgKey(
            TestId testId,
            GeneralTestQuestionId questionId,
            string imgExtension
        ) => Path.Combine(
            ImgOperationsConsts.PublishedTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.QuestionImgFileName.ToString() + imgExtension
        );
        public static string GetPublishedGeneralTestAnswerImgKey(
            TestId testId,
            GeneralTestQuestionId questionId,
            string imgExtension
        ) => Path.Combine(
            ImgOperationsConsts.PublishedTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.AnswersSubFolderName,
            Guid.NewGuid().ToString() + imgExtension
        );
        public static string GetDraftTestCoverImgKey(
           DraftTestId testId,
           string imgExtension
       ) => Path.Combine(
           ImgOperationsConsts.DraftTestsFolderName,
           testId.Value.ToString(),
           ImgOperationsConsts.TestCoverFileName
       );
        public static string GetDraftGeneralTestResultImgKey(
            DraftTestId testId,
            DraftGeneralTestResultId resultId,
            string imgExtension
        ) => Path.Combine(
            DraftGeneralTestResultsFolder(testId),
            resultId.Value.ToString() + imgExtension
        );
        public static string DraftGeneralTestResultsFolder(
            DraftTestId testId
        ) => Path.Combine(
            ImgOperationsConsts.DraftTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.ResultsSubFolderName
        );
        public static string GetDraftGeneralTestQuestionImgKey(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId,
            string imgExtension
        ) => Path.Combine(
            DraftGeneralTestQuestionsFolder(testId, questionId),
            Guid.NewGuid() + imgExtension
        );
        public static string DraftGeneralTestQuestionsFolder(
           DraftTestId testId,
           DraftGeneralTestQuestionId questionId
       ) => Path.Combine(
           ImgOperationsConsts.DraftTestsFolderName,
           testId.Value.ToString(),
           ImgOperationsConsts.QuestionsSubFolderName,
           questionId.ToString()
       );
        public static string GetDraftGeneralTestAnswerImgKey(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId,
            string imgExtension
        ) => Path.Combine(
            DraftGeneralTestAnswersFolder(testId, questionId),
            Guid.NewGuid().ToString() + imgExtension
        );
        public static string DraftGeneralTestAnswersFolder(
            DraftTestId testId,
            DraftGeneralTestQuestionId questionId
        ) => Path.Combine(
            ImgOperationsConsts.DraftTestsFolderName,
            testId.Value.ToString(),
            ImgOperationsConsts.QuestionsSubFolderName,
            questionId.ToString(),
            ImgOperationsConsts.AnswersSubFolderName
        );
    }
}
