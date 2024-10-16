using System.ComponentModel.DataAnnotations.Schema;
using vokimi_api.Src.db_related.db_entities_ids;

namespace vokimi_api.Src.db_related.db_entities.draft_published_tests_shared.general_test_answers
{
    public abstract class GeneralTestAnswerTypeSpecificInfo
    {
        public GeneralTestAnswerTypeSpecificInfoId Id { get; init; }
    }
}
