<script lang="ts">
    import {
        FeedbackRecordsFilter,
        FeedbackTypesForFilter,
    } from "../../../../../ts/page_classes/manage_test_page/conclusion/FeedbackRecordsFilter";

    let filter: FeedbackRecordsFilter = new FeedbackRecordsFilter();

    export let fetchFilteredRecords: (
        filter: FeedbackRecordsFilter,
    ) => Promise<void>;
    export let fetchRecords: () => Promise<void>;

    async function clearFilter() {
        filter = new FeedbackRecordsFilter();
        await fetchRecords();
    }
    let isHidden = true;
</script>

<div class="feedback-filter" class:is-hidden={isHidden}>
    <div class="filter-line">
        <span class="filter-label">Creation Date</span>
        <div>
            <label for="dateFrom">From:</label>
            <input type="date" id="dateFrom" bind:value={filter.dateFrom} />
            <label for="dateTo">To:</label>
            <input type="date" id="dateTo" bind:value={filter.dateTo} />
        </div>
    </div>
    <div class="filter-line">
        <span class="filter-label">Feedback length</span>
        <div>
            <label for="minLength">from:</label>
            <input type="number" id="minLength" bind:value={filter.minLength} />
            <label for="minLength">To:</label>
            <input type="number" id="minLength" bind:value={filter.maxLength} />
        </div>
    </div>

    <div class="filter-line">
        <span class="filter-label">Feedback Type:</span>
        <div class="feedback-type">
            {#each Object.entries(FeedbackTypesForFilter) as [key, feedbackType]}
                <div>
                    <input
                        type="radio"
                        id={key}
                        name="feedbackType"
                        bind:group={filter.feedbackType}
                        value={feedbackType}
                    />
                    <label for={key}>{feedbackType}</label>
                </div>
            {/each}
        </div>
    </div>
    <div class="buttons-container">
        <button on:click={() => clearFilter()} class="clear-btn">Clear</button>
        <button on:click={() => fetchFilteredRecords(filter)} class="apply-btn">
            Apply
        </button>
    </div>
</div>

<button on:click={() => (isHidden = !isHidden)}>
    {isHidden ? "Show Filter" : "Hide Filter"}
</button>

<style>
    .feedback-filter {
        display: flex;
        flex-direction: column;
        gap: 12px;
    }
    .is-hidden {
        display: none;
    }
    .filter-line {
        display: grid;
        grid-template-columns: 240px 1fr;
        align-items: center;

        gap: 6px;
    }
    .filter-line label {
        margin-left: 12px;
        color: var(--text-faded);
        font-size: 18px;
    }
    .apply-btn {
        background-color: var(--primary);
        color: var(--text);
        padding: 6px 12px;
        border: none;
        border-radius: 6px;
        cursor: pointer;
    }
</style>
