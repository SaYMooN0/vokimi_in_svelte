<script lang="ts">
    import AuthorizeView from "../../../../../components/AuthorizeView.svelte";
    import CustomCheckbox from "../../../../../components/shared/CustomCheckbox.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { RatingsTabFilter } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ratings_tab_classes/RatingsTabFilter";
    import type { TestRatingVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ratings_tab_classes/TestRatingVm";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import MinMaxInputType from "../tabs_shared/MinMaxInputType.svelte";

    export function show() {
        isHidden = false;
    }
    export function hide() {
        isHidden = true;
    }
    export let showFilteredRatings: (ratings: TestRatingVm[]) => void;
    export let testId: string;

    function clearFilter() {
        filter = new RatingsTabFilter();
    }
    async function applyFilter() {
        const response = await fetch(
            "/api/viewTest/ratings/getFilteredRatings",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ testId, ...filter }),
            },
        );

        if (response.ok) {
            const data = await response.json();
            return showFilteredRatings(data);
        } else if (response.status === 400) {
            filterApplyingError = await getErrorFromResponse(response);
        } else {
            filterApplyingError = "An unknown error occurred.";
        }
    }
    let isHidden = true;
    let filter: RatingsTabFilter = new RatingsTabFilter();
    let filterApplyingError: string = "";
</script>

<form
    class="ratings-filter"
    class:is-hidden={isHidden}
    on:submit|preventDefault={applyFilter}
>
    <p class="filter-line">
        <span class="filter-name">Rating value</span>
        <MinMaxInputType
            minPossibleValue={1}
            maxPossibleValue={5}
            bind:minVal={filter.ratingMinValue}
            bind:maxVal={filter.ratingMaxValue}
        />
    </p>
    <div class="filter-line">
        <span class="filter-name">Rating last update</span>
        <div class="last-update-input-container">
            From
            <input
                type="date"
                bind:value={filter.minDate}
                min="2000-01-01"
                max={new Date(new Date().getTime() + 86400000)
                    .toISOString()
                    .split("T")[0]}
            />
            To
            <input
                type="date"
                bind:value={filter.maxDate}
                min="2000-01-01"
                max={new Date(new Date().getTime() + 86400000)
                    .toISOString()
                    .split("T")[0]}
            />
        </div>
    </div>
    <AuthorizeView>
        <div slot="authenticated">
            <p class="filter-line">
                <span class="filter-name">
                    Ratings only by my followers and friends
                </span>
                <CustomCheckbox
                    bind:isChecked={filter.onlyByFollowersAndFriends}
                />
            </p>
            <p class="filter-line">
                <span class="filter-name">Ratings only by my friends</span>
                <CustomCheckbox bind:isChecked={filter.onlyByFriends} />
            </p>
            <p class="filter-line"></p>
        </div>
        <div slot="unauthenticated" class="login-message unselectable">
            Please log in to access all filter
        </div>
    </AuthorizeView>
    {#if !StringUtils.isNullOrWhiteSpace(filterApplyingError)}
        <p class="error-message">{filterApplyingError}</p>
    {/if}
    <div class="form-buttons">
        <button type="button" on:click={() => clearFilter()}>Clear</button>
        <button type="submit">Apply</button>
    </div>
</form>
