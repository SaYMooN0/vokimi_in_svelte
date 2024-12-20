<script lang="ts">
    import type { TagsSuggestionForTest } from "../../../ts/page_classes/manage_test_page/tags/TagsSuggestionForTest";

    export let tagsSuggestions: TagsSuggestionForTest[];
    enum SortBy {
        FromAToZ,
        FromZToA,
        FromOldestToNewest,
        FromNewestToOldest,
        FromMostPopularToLeastPopular,
        FromLeastPopularToMostPopular,
    }
    let sortBy: SortBy = SortBy.FromAToZ;
    function getSortedTagSuggestions(): TagsSuggestionForTest[] {
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

<h1 class="suggestions-header">Tags suggestion({tagsSuggestions.length})</h1>
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
            <div class="suggestion">
                <p class="sugggestion-p">
                    Tag
                    <span class="suggestion-value">
                        {suggestion.value}
                    </span>
                    was suggested
                    <span class="suggestion-count">
                        {suggestion.suggestionsCount}
                    </span>
                    times
                </p>
                <span class="suggestion-date">
                    First suggestion: {suggestion.firstSuggestionDate.toLocaleString()}
                </span>
                <div class="suggestion-actions">
                    <button class="accept-btn">Accept</button>
                    <button class="decline-btn">Decline</button>
                    <button class="ban-btn">Ban</button>
                </div>
            </div>
        {/each}
    {/key}
</div>

<style>
    .suggestions-container {
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        gap: 8px;
        margin: 8px 0;
    }
    .suggestion {
        display: grid;
        grid-template-columns: auto 24px;
    }
    .suggestion span {
        color: var(--back-main);
    }
    .suggestions-header {
        margin: 12px 0;
        color: var(--text);
    }
    .suggestion-actions {
        display: flex;
        flex-direction: row;
        gap: 8px;
    }
</style>
