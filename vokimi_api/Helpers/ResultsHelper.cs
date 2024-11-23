using vokimi_api.Src;
using vokimi_api.Src.constants_store_classes;

namespace vokimi_api.Helpers
{
    public static class ResultsHelper
    {
        public static BadRequestCollection BadRequest { get; } = new BadRequestCollection();
        public static OkCollection Ok { get; } = new OkCollection();

        public class BadRequestCollection
        {
            public IResult WithErr(string err) =>
                TypedResults.BadRequest(new { Error = err });

            public IResult WithErr(Err err) =>
                TypedResults.BadRequest(new { Error = err.Message });

            public IResult SaveChangesTryAgain() =>
                WithErr("Server error. Save existing changes and try to refresh the page");
            public IResult LogOutLogIn() =>
                WithErr("An error has occurred. Please log out, log in and try again");

            public IResult UnknownTest() =>
                WithErr("Unknown Test");

            public IResult NotCreator() =>
                WithErr("You have to be the creator to perform this action");

            public IResult UserDoesNotExist() =>
                WithErr("User doesn't exist");

            public IResult MaxImgSizeIs3MB() =>
                WithErr($"File is too big. Max allowed size: {ImgOperationsConsts.MaxImageSizeInMB}MB");

            public IResult ServerError() =>
                WithErr("Server error. Please try again later");

            public IResult NoTestAccess() =>
                WithErr("You don't have permission to access this test");
            public IResult DiscussionsDisabled() =>
                WithErr("Discussions for this test are disabled");

        }

        public class OkCollection
        {
            public IResult WithImgPath(string imgPath) =>
                Results.Ok(new { ImgPath = imgPath });
        }
    }

}
