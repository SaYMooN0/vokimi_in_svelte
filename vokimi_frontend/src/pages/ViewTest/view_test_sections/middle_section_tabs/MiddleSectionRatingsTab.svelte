<script script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ViewTestRatingsTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestRatingsTabData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import NoRatingsComponent from "./ratings_tab_components/NoRatingsComponent.svelte";
    import RatingsTabStarsInput from "./ratings_tab_components/RatingsTabStarsInput.svelte";
    import TestRatingsListComponent from "./ratings_tab_components/TestRatingsListComponent.svelte";

    export let testId: string;
    async function fetchRatingsListPackage(): Promise<
        ViewTestRatingsTabData | Err
    > {
        const response = await fetch(
            `/api/viewTest/getTestRatingsInfo/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            return new ViewTestRatingsTabData(
                data.viewerRating,
                data.averageRating,
                data.ratingsList,
            );
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
    }
</script>

{#await fetchRatingsListPackage() then fetchingRes}
    {#if fetchingRes instanceof Err}
        <p class="fetching-err">{fetchingRes.toString()}</p>
    {:else}
        <p class="average-rating">
            Test average rating: {fetchingRes.averageRating}
        </p>

        <div class="viewer-rating">
            <AuthorizeView>
                <div slot="authenticated">
                    <RatingsTabStarsInput
                        {testId}
                        rating={fetchingRes.viewerRating ?? 0}
                        updateRating={(newRatingVal) => {
                            fetchingRes.viewerRating = newRatingVal;
                        }}
                    />
                </div>
                <div slot="unauthenticated" class="authentication-needed-div">
                    you have to log in to rate the test if has rated
                </div>
            </AuthorizeView>
        </div>
        {#if fetchingRes.ratingsList.length > 0}
            <TestRatingsListComponent ratingsList={fetchingRes.ratingsList} />
        {:else}
            <NoRatingsComponent />
        {/if}
    {/if}
{/await}

<style>
</style>
