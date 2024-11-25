<script lang="ts">
    import MinMaxInputType from "../tabs_shared/MinMaxInputType.svelte";
    import { Err } from "../../../../../ts/Err";
    import TabFilterComponent from "../tabs_shared/TabFilterComponent.svelte";
    import CustomCheckbox from "../../../../../components/shared/CustomCheckbox.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { DiscussionsTabFilter } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/DiscussionsTabFilter";
    import type { TestDiscussionCommentVm } from "../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/TestDiscussionCommentVm";
    export let showFilteredComments: (
        comments: TestDiscussionCommentVm[],
    ) => void;
    export let testId: string;

    function clearFilter() {
        filter = new DiscussionsTabFilter();
    }
    async function applyFilter() {
        const response = await fetch(
            "/api/viewTest/discussions/getFilteredDiscussions",
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
            return showFilteredComments(data);
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
    }
    let filter: DiscussionsTabFilter = new DiscussionsTabFilter();
</script>

<TabFilterComponent {clearFilter} {applyFilter}>
    <svelte:fragment slot="filter-content">
        <p class="filter-warning-message">
            <svg
                class="warning-icon"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <circle
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    stroke-width="0.1"
                />
                <path
                    d="M11.992 15H12.001"
                    stroke="currentColor"
                    stroke-width="2.4"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M12 12L12 8"
                    stroke="currentColor"
                    stroke-width="2"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            If a comment matches a filter but is an answer to a comment that does
            not pass through this filter, then the answer (along with the parent
            comment) will not be shown
        </p>
        <p class="filter-line">
            <span class="filter-name">Child comments count</span>
            <MinMaxInputType
                minPossibleValue={0}
                maxPossibleValue={null}
                bind:minVal={filter.minChildCommentsCount}
                bind:maxVal={filter.maxChildCommentsCount}
            />
        </p>
        <p class="filter-line">
            <span class="filter-name">Votes rating</span>
            <MinMaxInputType
                minPossibleValue={null}
                maxPossibleValue={null}
                bind:minVal={filter.minVotesRating}
                bind:maxVal={filter.maxVotesRating}
            />
        </p>
        <p class="filter-line">
            <span class="filter-name">Total votes count</span>
            <MinMaxInputType
                minPossibleValue={0}
                maxPossibleValue={null}
                bind:minVal={filter.minVotesCount}
                bind:maxVal={filter.maxVotesCount}
            />
        </p>
    </svelte:fragment>
    <svelte:fragment slot="authenticated-filters">
        <p class="filter-line">
            <span class="filter-name">
                Comments only by my followers and friends
            </span>
            <CustomCheckbox bind:isChecked={filter.onlyByFollowersAndFriends} />
        </p>
        <p class="filter-line">
            <span class="filter-name">Comments only by my friends</span>
            <CustomCheckbox bind:isChecked={filter.onlyByFriends} />
        </p>
    </svelte:fragment>
</TabFilterComponent>

<style>
    .filter-warning-message {
        margin-top: 4px;
        color: var(--text-faded);
        font-size: 16px;
        display: block;
    }
    .warning-icon {
        vertical-align: middle;
        height: 20px;
        aspect-ratio: 1/1;
        fill: var(--primary);
        color: var(--back-main);
    }
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
</style>
