<script lang="ts">
    import type { TestRatingVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ratings_tab_classes/TestRatingVm";
    import ProfilePictureWithLink from "../tabs_shared/ProfilePictureWithLink.svelte";
    import UsernameWithLink from "../tabs_shared/UsernameWithLink.svelte";

    export let ratingsList: TestRatingVm[];
</script>

<div class="ratings-list">
    {#each ratingsList as rating}
        <div class="rating-view-div">
            <ProfilePictureWithLink
                userId={rating.userId}
                profilePicturePath={rating.userProfilePicture}
            />
            <div>
                <UsernameWithLink
                    userId={rating.userId}
                    username={rating.username}
                />
                <p class="rating-value">
                    {#each new Array(rating.ratingValue)
                        .fill(null)
                        .map((_, i) => i + 1) as star}
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            class="star-svg"
                        >
                            <path
                                d="M11.109 3.74829C11.48 3.02037 12.52 3.02037 12.891 3.74829L15.0785 8.0407C15.2237 8.32561 15.4964 8.5239 15.8122 8.5742L20.5671 9.33147C21.373 9.45983 21.6941 10.4474 21.1178 11.0252L17.7138 14.4383C17.4883 14.6644 17.3844 14.9846 17.4341 15.3001L18.1843 20.0635C18.3114 20.8702 17.4703 21.4808 16.7426 21.1102L12.4539 18.9254C12.1687 18.7801 11.8313 18.7801 11.5461 18.9254L7.25739 21.1102C6.52973 21.4808 5.68859 20.8702 5.81565 20.0635L6.56594 15.3001C6.61562 14.9846 6.51167 14.6644 6.28617 14.4383L2.88217 11.0252C2.3059 10.4474 2.62703 9.45983 3.43294 9.33147L8.18782 8.5742C8.50362 8.5239 8.77632 8.32561 8.92151 8.0407L11.109 3.74829Z"
                                stroke="currentColor"
                                stroke-width="0.2"
                            />
                        </svg>
                    {/each}
                </p>
            </div>
            <span class="rating-date">
                {rating.lastUpdateDateTime}
            </span>
        </div>
    {/each}
</div>

<style>
    .ratings-list {
        display: flex;
        flex-direction: column;
    }
    .rating-view-div {
        position: relative;
        display: grid;
        grid-template-columns: auto 1fr;
        gap: 12px;
        padding: 4px 8px;
        border-radius: 6px;
        box-sizing: border-box;
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 2px 12px 0px,
            rgb(67, 58, 178, 0.05) 0px 0px 0px 1px;
    }
    .rating-view-div div {
        display: flex;
        flex-direction: column;
        justify-content: center;
    }
    .rating-value {
        display: flex;
        flex-direction: row;
        align-items: center;
        margin: 0;
    }
    .star-svg {
        width: 24px;
        height: 24px;
        fill: var(--primary);
        color: var(--primary);
    }
    .rating-date {
        position: absolute;
        right: 12px;
        bottom: 4px;
        color: var(--text-faded);
        font-size: 14px;
    }
</style>
