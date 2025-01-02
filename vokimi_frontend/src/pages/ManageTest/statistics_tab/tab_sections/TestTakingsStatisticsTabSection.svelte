<script lang="ts">
    import { TestStatisticsTestTakenRecordsCount } from "../../../../ts/page_classes/manage_test_page/statistics/templates_shared/TestStatisticsTestTakenRecordsCount";
    import SectionStatisticsCardsContainer from "../sections_shared_components/SectionStatisticsCardsContainer.svelte";
    import StatisticsTabSectionHeader from "../sections_shared_components/StatisticsTabSectionHeader.svelte";

    export let sectionData: TestStatisticsTestTakenRecordsCount;

    function formCardsForTestTakings(): { label: string; value: string }[] {
        return [
            {
                label: "Total number of test takings",
                value: sectionData.all.toString(),
            },
            {
                label: "Number of test takings by followers",
                value: sectionData.byFollowers.toString(),
            },
            {
                label: "Number of test takings by friends",
                value: sectionData.byFriends.toString(),
            },
            {
                label: "Number of test takings for the last hour",
                value: sectionData.lastHour.toString(),
            },
            {
                label: "Number of test takings for the last day",
                value: sectionData.lastDay.toString(),
            },
            {
                label: "Number of test takings for the last month",
                value: sectionData.lastMonth.toString(),
            },
            {
                label: "Number of test takings for the last year",
                value: sectionData.lastYear.toString(),
            },
        ];
    }
</script>

<StatisticsTabSectionHeader text="Test Takings" />
<SectionStatisticsCardsContainer cards={formCardsForTestTakings()} />
