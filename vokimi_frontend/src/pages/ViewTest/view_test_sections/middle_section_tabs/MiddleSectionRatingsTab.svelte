<script script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ViewTestRatingsTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestRatingsTabData";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import NoRatingsComponent from "./ratings_tab_components/NoRatingsComponent.svelte";
    import RatingsFilterComponent from "./ratings_tab_components/RatingsFilterComponent.svelte";
    import RatingsTabStarsInput from "./ratings_tab_components/RatingsTabStarsInput.svelte";
    import TestRatingsListComponent from "./ratings_tab_components/TestRatingsListComponent.svelte";

    export let testId: string;
    let ratingsTabData: ViewTestRatingsTabData =
        ViewTestRatingsTabData.createEmpty();
    let fetchingErr: string = "";
    async function fetchRatingsListPackage() {
        const response = await fetch(
            `/api/viewTest/getTestRatingsInfo/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            fetchingErr = "";
            ratingsTabData = new ViewTestRatingsTabData(
                data.viewerRating,
                data.averageRating,
                data.ratingsList,
            );
        } else {
            ratingsTabData = ViewTestRatingsTabData.createEmpty();
            if (response.status === 400) {
                fetchingErr = await getErrorFromResponse(response);
            } else {
                fetchingErr = "An unknown error occurred.";
            }
        }
    }
</script>

{#await fetchRatingsListPackage() then _}
    {#if !StringUtils.isNullOrWhiteSpace(fetchingErr)}
        <p class="fetching-err">{fetchingErr}</p>
    {:else}
        <p class="average-rating">
            Test average rating: {ratingsTabData.averageRating}
        </p>

        <div class="viewer-rating">
            <AuthorizeView>
                <div slot="authenticated">
                    <RatingsTabStarsInput
                        {testId}
                        rating={ratingsTabData.viewerRating ?? 0}
                        updateParentElementRating={() => fetchRatingsListPackage()}
                    />
                </div>
                <div slot="unauthenticated" class="authentication-needed-div">
                    You have to log in to rate the test if has rated
                </div>
            </AuthorizeView>
        </div>
        <RatingsFilterComponent
            showFilteredRatings={(newRatings) => {
                ratingsTabData.ratingsList = newRatings;
            }}
            {testId}
        />
        {#if ratingsTabData.ratingsList.length > 0}
            <TestRatingsListComponent
                ratingsList={ratingsTabData.ratingsList}
            />
        {:else}
            <NoRatingsComponent />
        {/if}
    {/if}
{/await}

<style>
    .authentication-needed-div {
        text-align: center;
        color: var(--text-faded);
        font-size: 20px;
        margin: 12px;
    }
</style>
