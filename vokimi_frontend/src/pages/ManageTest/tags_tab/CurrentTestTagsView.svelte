<script lang="ts">
    export let tags: string[];
</script>

<h1 class="test-tags-header">Test tags</h1>
{#if tags.length < 1}
    <p class="no-tags-p">This test has no tags</p>
{:else}
    <div class="test-tags-container">
        {#each tags as tag}
            <span class="test-tag">{tag}</span>
        {/each}
    </div>{/if}

<style>
    .test-tags-header {
        color: var(--text);
        font-size: 32px;
    }
    .test-tags-container {
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        gap: 8px;
        margin: 8px 0;
    }
    .test-tag {
        color: var(--back-main);
        background-color: var(--primary);
        font-size: 16px;
        padding: 4px 8px;
        border-radius: 4px;
    }
</style>
