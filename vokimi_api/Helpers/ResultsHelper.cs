using vokimi_api.Src;
using vokimi_api.Src.constants_store_classes;

namespace vokimi_api.Helpers
{
    public static class ResultsHelper
    {
        public static IResult BadRequestWithErr(string err) =>
            TypedResults.BadRequest(new { Error = err });
        public static IResult BadRequestWithErr(Err err) =>
          TypedResults.BadRequest(new { Error = err.Message });
        public static IResult BadRequestSaveChangesTryAgain() =>
            BadRequestWithErr("Server error. Save existing changes and try to refresh the page");
        public static IResult BadRequestUnknownTest() =>
            BadRequestWithErr("Unknown Test");
        public static IResult BadRequestNotCreator() =>
            BadRequestWithErr("You have to be the creator to perform this action");

        public static IResult BadRequestUserDoesnotExist() =>
            BadRequestWithErr("User doesn't exist");
        public static IResult BadRequestMaxImgSizeIs3MB() =>
            BadRequestWithErr($"File is too big. Max allowed size: {ImgOperationsConsts.MaxImageSizeInMB}MB");
        public static IResult BadRequestServerError() =>
            BadRequestWithErr("Server error. Please try again later");
        public static IResult OkResultWithImgPath(string imgPath) =>
             Results.Ok(new { ImgPath = imgPath });
    }
}
