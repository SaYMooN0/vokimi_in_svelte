<script lang="ts">
    import type { TestStatisticsDiscussionsData } from "../../../../ts/page_classes/manage_test_page/statistics/templates_shared/TestStatisticsDiscussionsData";
    import SectionStatisticsCardsContainer from "../sections_shared_components/SectionStatisticsCardsContainer.svelte";
    import StatisticsTabSectionHeader from "../sections_shared_components/StatisticsTabSectionHeader.svelte";

    export let sectionData: TestStatisticsDiscussionsData;
    function formCardsForDiscussions(): { label: string; value: string }[] {
        return [
            {
                label: "Total comments count",
                value: sectionData.totalCommentsCount.toString(),
            },
            {
                label: "Discussions count",
                value: sectionData.discussionsCount.toString(),
            },
        ];
    }
</script>

<StatisticsTabSectionHeader text="Test Discussions" />
<SectionStatisticsCardsContainer cards={formCardsForDiscussions()} />
