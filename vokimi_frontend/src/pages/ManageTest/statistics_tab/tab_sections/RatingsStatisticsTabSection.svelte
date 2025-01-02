<script lang="ts">
    import type { TestStatisticsRatingsData } from "../../../../ts/page_classes/manage_test_page/statistics/templates_shared/TestStatisticsRatingsData";
    import SectionStatisticsCardsContainer from "../sections_shared_components/SectionStatisticsCardsContainer.svelte";
    import StatisticsTabSectionHeader from "../sections_shared_components/StatisticsTabSectionHeader.svelte";

    export let sectionData: TestStatisticsRatingsData;

    function formCardsForRatings(): { label: string; value: string }[] {
        return [
            {
                label: "Average test rating",
                value: sectionData.averageRating.toString(),
            },
            {
                label: "Total number of ratings",
                value: sectionData.ratingsCount.toString(),
            },
            {
                label: "Average test rating by followers",
                value: sectionData.averageRatingByFollowers.toString(),
            },
            {
                label: "Number of ratings by followers",
                value: sectionData.ratingsByFollowersCount.toString(),
            },
            {
                label: "Average test rating by friends",
                value: sectionData.averageRatingByFriends.toString(),
            },
            {
                label: "Number of ratings by friends",
                value: sectionData.ratingsByFriendsCount.toString(),
            },
        ];
    }
</script>

<StatisticsTabSectionHeader text="Test Ratings" />
<SectionStatisticsCardsContainer cards={formCardsForRatings()} />
