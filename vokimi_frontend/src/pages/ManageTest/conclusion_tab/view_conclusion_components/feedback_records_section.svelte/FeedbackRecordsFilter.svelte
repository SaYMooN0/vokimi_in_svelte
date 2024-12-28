<script lang="ts">
    import {
        FeedbackRecordsFilter,
        FeedbackTypesForFilter,
    } from "../../../../../ts/page_classes/manage_test_page/conclusion/FeedbackRecordsFilter";

    export let filter: FeedbackRecordsFilter = new FeedbackRecordsFilter();

    export let filterFeedbackRecords: (filter: FeedbackRecordsFilter) => void;
</script>

<div class="feedback-filter">
    <div>
        <label for="dateFrom">Date From:</label>
        <input type="date" id="dateFrom" bind:value={filter.dateFrom} />
    </div>

    <div>
        <label for="dateTo">Date To:</label>
        <input type="date" id="dateTo" bind:value={filter.dateTo} />
    </div>

    <div>
        <label for="minLength">Minimum Length:</label>
        <input type="number" id="minLength" bind:value={filter.minLength} />
    </div>

    <div>
        <label for="maxLength">Maximum Length:</label>
        <input type="number" id="maxLength" bind:value={filter.maxLength} />
    </div>

    <div>
        <label>Feedback Type:</label>
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

    <button on:click={() => filterFeedbackRecords(filter)}>Apply</button>
</div>

<style>
</style>
