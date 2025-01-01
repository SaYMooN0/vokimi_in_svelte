<script lang="ts">
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { ManageTestStatisticsTabData } from "../../../ts/page_classes/manage_test_page/statistics/ManageTestStatisticsTabData";
    import { GeneralTestStatisticsData } from "../../../ts/page_classes/manage_test_page/statistics/templates/GeneralTestStatisticsData";
    import { ScoringTestStatisticsData } from "../../../ts/page_classes/manage_test_page/statistics/templates/ScoringTestStatisticsData";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import SectionStatisticsCardsContainer from "./sections_shared/SectionStatisticsCardsContainer.svelte";
    import StatisticsTabSectionHeader from "./sections_shared/StatisticsTabSectionHeader.svelte";

    export let testId: string;
    export let isActive: boolean;

    let tabData: ManageTestStatisticsTabData;
    async function fetchTabData(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/statistics/tabData/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            tabData = ManageTestStatisticsTabData.fromResponseData(data);
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
    function formCardsForTestTakings(): { label: string; value: string }[] {
        return [
            {
                label: "Total number of test takings",
                value: tabData.testTakenRecords.all.toString(),
            },
            {
                label: "Number of test takings by followers",
                value: tabData.testTakenRecords.byFollowers.toString(),
            },
            {
                label: "Number of test takings by friends",
                value: tabData.testTakenRecords.byFriends.toString(),
            },
            {
                label: "Number of test takings for the last hour",
                value: tabData.testTakenRecords.lastHour.toString(),
            },
            {
                label: "Number of test takings for the last day",
                value: tabData.testTakenRecords.lastDay.toString(),
            },
            {
                label: "Number of test takings for the last month",
                value: tabData.testTakenRecords.lastMonth.toString(),
            },
            {
                label: "Number of test takings for the last year",
                value: tabData.testTakenRecords.lastYear.toString(),
            },
        ];
    }
    function formCardsForRatings(): { label: string; value: string }[] {
        return [
            {
                label: "Average test rating",
                value: tabData.ratings.averageRating.toString(),
            },
            {
                label: "Total number of ratings",
                value: tabData.ratings.ratingsCount.toString(),
            },
            {
                label: "Average test rating by followers",
                value: tabData.ratings.averageRatingByFollowers.toString(),
            },
            {
                label: "Number of ratings by followers",
                value: tabData.ratings.ratingsByFollowersCount.toString(),
            },
            {
                label: "Average test rating by friends",
                value: tabData.ratings.averageRatingByFriends.toString(),
            },
            {
                label: "Number of ratings by friends",
                value: tabData.ratings.ratingsByFriendsCount.toString(),
            },
        ];
    }
    function formCardsForDiscussions(): { label: string; value: string }[] {
        return [];
    }
</script>

<TabContentWrapper {fetchTabData} {isActive}>
    {#if tabData instanceof GeneralTestStatisticsData}{:else if tabData instanceof ScoringTestStatisticsData}{/if}
    <StatisticsTabSectionHeader text="Test Takings" />
    <SectionStatisticsCardsContainer cards={formCardsForTestTakings()} />
    <StatisticsTabSectionHeader text="Test Ratings" />
    <SectionStatisticsCardsContainer cards={formCardsForRatings()} />
    <StatisticsTabSectionHeader text="Test Discussions" />
    <SectionStatisticsCardsContainer cards={formCardsForDiscussions()} />
</TabContentWrapper>

<style>
</style>
