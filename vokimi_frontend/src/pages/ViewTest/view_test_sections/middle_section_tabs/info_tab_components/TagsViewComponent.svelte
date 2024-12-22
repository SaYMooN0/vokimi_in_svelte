<script lang="ts">
    import TagSuggestionDialog from "./TagSuggestionDialog.svelte";

    export let testId: string;
    export let tags: string[];
    export let tagsSuggestionsAllowed: boolean;

    let tagSuggestionDialog: TagSuggestionDialog;
</script>

{#if tags.length > 0}
    <p class="test-tags-p">Test tags:</p>
    <div class="tags-container">
        {#each tags as tag}
            <div class="tag-display tag-like-btn">#{tag}</div>
        {/each}
        {#if tagsSuggestionsAllowed}
            <TagSuggestionDialog bind:this={tagSuggestionDialog} {testId} />

            <div
                class="suggest-tags-btn tag-like-btn unselectable"
                on:click={() => tagSuggestionDialog.open()}
            >
                + Suggest tags
            </div>
        {/if}
    </div>
{:else if tags.length === 0 && tagsSuggestionsAllowed}
    <TagSuggestionDialog bind:this={tagSuggestionDialog} {testId} />
    <div class="first-tag-suggestion">
        <span> This test has no tags yet </span>
        <TagSuggestionDialog bind:this={tagSuggestionDialog} {testId} />
        <div
            class="suggest-tags-btn tag-like-btn unselectable"
            on:click={() => tagSuggestionDialog.open()}
        >
            + Suggest tags
        </div>
    </div>
{/if}

<style>
    .test-tags-p {
        margin: 10px;
        font-size: 18px;
        color: var(--text);
    }
    .tags-container {
        margin: 10px;
        padding: 8px;
        border-radius: 4px;
        background-color: var(--back);
        display: flex;
        flex-wrap: wrap;
        gap: 8px;
    }
    .tag-like-btn {
        padding: 4px 8px;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .tag-display {
        background-color: var(--primary);
        color: var(--back-main);
    }
    .suggest-tags-btn {
        background-color: var(--back-secondary);
        color: var(--text-faded);
    }
    .suggest-tags-btn:hover {
        background-color: var(--text-faded);
        color: var(--back-main);
    }

    .first-tag-suggestion {
        margin-top: 12px;

        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: center;
        gap: 8px;
    }
    .first-tag-suggestion span {
        color: var(--text);
        font-size: 20px;
    }
</style>
