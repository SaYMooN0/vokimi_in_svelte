<script lang="ts">
    import type { TestRatingVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestRatingsTabData";
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";
    import ProfilePictureWithLink from "../tabs_shared/ProfilePictureWithLink.svelte";
    import UsernameWithLink from "../tabs_shared/UsernameWithLink.svelte";

    export let ratingsList: TestRatingVm[];
</script>

<div class="ratings-list">
    {#each ratingsList as rating}
        <div class="rating-view-div">
            <ProfilePictureWithLink
                userId={rating.userId}
                profilePicturePath={rating.profilePicturePath}
            />
            <div>
                <UsernameWithLink
                    userId={rating.userId}
                    username={rating.username}
                />
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
