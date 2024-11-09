<script script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { RatingsTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/RatingsTabData";
    import RatingsTabStarsInput from "./ratings_tab_components/RatingsTabStarsInput.svelte";

    export let testId: string = "";
    let tabsData: RatingsTabData;
    async function fetchRatingsListPackage(): Promise<RatingsTabData | Err> {
        const response = await fetch(
            `/api/viewTest/getTestRatingsInfo/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            return new RatingsTabData(
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
    async function fetchViewerRating(): Promise<number | Err> {
        return new Err("not implemented");
    }
    async function updateViewersRating(rating: number): Promise<void> {}
</script>

{#await fetchRatingsListPackage() then fetchingRes}
    {#if fetchingRes instanceof Err}
        <p class="fetching-err">{fetchingRes.toString()}</p>
    {:else}
        <p class="average-rating">
            Test average rating: {fetchingRes.averageRating}
        </p>
        {#if fetchingRes.viewerRating === null}
            <div class="authentication-needed-div">
                you have to log in to rate the test if has rated
            </div>
        {:else}
            <RatingsTabStarsInput
                rating={fetchingRes.viewerRating}
                updateRating={updateViewersRating}
            />
        {/if}
        <div class="viewer-rating">
            <AuthorizeView>
                <div slot="loading">
                    <p>Loading...</p>
                </div>

                <div slot="authenticated" class="viewer-rating">
                    {#await fetchViewerRating()}
                        <p>Loading...</p>
                    {:then fetchRes}
                        {#if fetchRes instanceof Err}
                            <p>{fetchRes.toString()}</p>
                        {:else}
                            <p>{fetchRes}</p>
                        {/if}
                    {/await}
                </div>
            </AuthorizeView>
        </div>
        <div class="ratings-list"></div>
    {/if}
{/await}
