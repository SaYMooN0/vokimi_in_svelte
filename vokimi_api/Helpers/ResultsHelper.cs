using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace vokimi_api.Helpers
{
    public class ResultsHelper
    {
        public static IResult BadRequestWithErr(string err) =>
            TypedResults.BadRequest(new { Error = err });
        public static IResult OkResultWithImgPath(string imgPath) =>
             Results.Ok(new { ImgPath = imgPath });
    }
}
