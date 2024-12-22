<script lang="ts">
    import type { TagSuggestionForTest } from "../../../ts/page_classes/manage_test_page/tags/TagSuggestionForTest";
    import SuggestionItem from "./SuggestionItem.svelte";

    export let tagsSuggestions: TagSuggestionForTest[];
    export let addNewTag: (newTag: string) => void;
    export let testId: string;
    enum SortBy {
        FromAToZ,
        FromZToA,
        FromOldestToNewest,
        FromNewestToOldest,
        FromMostPopularToLeastPopular,
        FromLeastPopularToMostPopular,
    }
    let sortBy: SortBy = SortBy.FromAToZ;
    function getSortedTagSuggestions(): TagSuggestionForTest[] {
        return tagsSuggestions.sort((a, b) => {
            switch (sortBy) {
                case SortBy.FromAToZ:
                    return a.value.localeCompare(b.value);
                case SortBy.FromZToA:
                    return b.value.localeCompare(a.value);
                case SortBy.FromOldestToNewest:
                    return (
                        a.firstSuggestionDate.getTime() -
                        b.firstSuggestionDate.getTime()
                    );
                case SortBy.FromNewestToOldest:
                    return (
                        b.firstSuggestionDate.getTime() -
                        a.firstSuggestionDate.getTime()
                    );
                case SortBy.FromMostPopularToLeastPopular:
                    return b.suggestionsCount - a.suggestionsCount;
                case SortBy.FromLeastPopularToMostPopular:
                    return a.suggestionsCount - b.suggestionsCount;
            }
        });
    }
</script>

{#if tagsSuggestions.length < 1}
    <p class="no-suggestions-p">No suggestions</p>
{:else}
    <p class="sort-by-p">Sort by:</p>
    <select bind:value={sortBy}>
        <option value={SortBy.FromAToZ}>From A to Z</option>
        <option value={SortBy.FromZToA}>From Z to A</option>
        <option value={SortBy.FromOldestToNewest}>From oldest to newest</option>
        <option value={SortBy.FromNewestToOldest}>From newest to oldest</option>
        <option value={SortBy.FromMostPopularToLeastPopular}>
            From most popular to least popular
        </option>
        <option value={SortBy.FromLeastPopularToMostPopular}>
            From least popular to most popular
        </option>
    </select>
    <div class="suggestions-container">
        {#key sortBy}
            {#each getSortedTagSuggestions() as suggestion}
                <SuggestionItem
                    {suggestion}
                    addThisTagToTest={() => addNewTag(suggestion.value)}
                    {testId}
                />
            {/each}
        {/key}
    </div>
{/if}

<style>
    .suggestions-container {
        display: flex;
        flex-direction: column;
        gap: 8px;
        margin: 8px 0;
    }
</style>
