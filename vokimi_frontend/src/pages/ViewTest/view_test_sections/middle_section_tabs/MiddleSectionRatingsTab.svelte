<script script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { RatingsTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/RatingsTabData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import RatingsTabStarsInput from "./ratings_tab_components/RatingsTabStarsInput.svelte";

    export let testId: string;
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
        <div class="ratings-list">
            {#each fetchingRes.ratingsList as rating}
                <div class="rating-view-div">
                    <img
                        class="rating-user-profile-picture"
                        src={ImgUtils.imgUrl(rating.userProfilePicture)}
                        alt="user-profile-picture"
                    />
                    <div>
                        <a class="rating-username" href="/user/{rating.userId}">
                            {rating.username}
                        </a>
                        <span>{rating.ratingValue}</span>
                        <span class="rating-update-date">
                            {rating.lastUpdateDateTime}
                        </span>
                    </div>
                </div>
            {/each}
        </div>
    {/if}
{/await}

<style>
    .ratings-list {
        display: flex;
        flex-direction: column;
    }
    .rating-view-div {
        display: grid;
        height: 60px;
        grid-template-columns: 60px 1fr;
        gap: 12px;
    }
    .rating-user-profile-picture {
        height: inherit;
    }
    .rating-username {
        color: var(--primary);
        padding: 4px;
        border-radius: 2px;
    }
    .rating-username:hover {
        color: var(--primary-hov);
    }
    .rating-username:active {
        background-color: var(--back-secondary);
    }
    .rating-update-date {
        color: var(--text-faded);
    }
</style>
