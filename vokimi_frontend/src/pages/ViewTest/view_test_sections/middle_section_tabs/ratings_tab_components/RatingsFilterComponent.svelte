<script lang="ts">
    import { RatingsTabFilter } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ratings_tab_classes/RatingsTabFilter";
    import TabFilterComponent from "../tabs_shared/TabFilterComponent.svelte";
    import CustomCheckbox from "../../../../../components/shared/CustomCheckbox.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import type { TestRatingVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ratings_tab_classes/TestRatingVm";
    import MinMaxInputType from "../tabs_shared/MinMaxInputType.svelte";
    import { Err } from "../../../../../ts/Err";

    export let showFilteredRatings: (ratings: TestRatingVm[]) => void;
    export let testId: string;

    let filter: RatingsTabFilter = new RatingsTabFilter();

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
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
    }
</script>

<TabFilterComponent {clearFilter} {applyFilter}>
    <svelte:fragment slot="filter-content">
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
                <span>From</span>
                <input
                    type="date"
                    bind:value={filter.minDate}
                    min="2000-01-01"
                    max={new Date(new Date().getTime() + 86400000)
                        .toISOString()
                        .split("T")[0]}
                />
                <span>To</span>
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
    </svelte:fragment>
    <svelte:fragment slot="authenticated-filters">
        <p class="filter-line">
            <span class="filter-name">
                Ratings only by my followers and friends
            </span>
            <CustomCheckbox bind:isChecked={filter.onlyByFollowersAndFriends} />
        </p>
        <p class="filter-line">
            <span class="filter-name">Ratings only by my friends</span>
            <CustomCheckbox bind:isChecked={filter.onlyByFriends} />
        </p>
    </svelte:fragment>
</TabFilterComponent>

<style>
    .filter-line {
        display: flex;
        align-items: center;
        justify-content: space-between;
        box-sizing: border-box;
    }
    .filter-name {
        color: var(--text);
        font-size: 18px;
    }
    .last-update-input-container {
        display: flex;
        align-items: center;
        gap: 8px;
    }
    .last-update-input-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 6px;
    }
    .last-update-input-container span {
        margin-left: 12px;
        color: var(--text-faded);
        font-size: 18px;
    }
    .last-update-input-container input {
        padding: 0 4px;
        outline: none;
        border: 2px solid var(--text-faded);
        border-radius: 4px;
        background-color: var(--back-main);
        font-size: 18px;
        text-align: center;
    }
    .last-update-input-container input:focus {
        border-color: var(--primary);
    }
</style>
