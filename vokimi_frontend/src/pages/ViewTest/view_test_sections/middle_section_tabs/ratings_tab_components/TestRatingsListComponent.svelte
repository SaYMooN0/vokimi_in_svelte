<script lang="ts">
    import type { TestRatingVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestRatingsTabData";
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";

    export let ratingsList: TestRatingVm[];
</script>

<div class="ratings-list">
    {#each ratingsList as rating}
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
