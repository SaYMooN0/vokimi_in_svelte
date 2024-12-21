<script lang="ts">
    import TagsChangingDialog from "./TagsChangingDialog.svelte";

    export let tags: string[];
    export let updateParentElement: () => Promise<void>;
    export let testId;
    export let maxTagsCount: number;
    let tagsChangingDialog: TagsChangingDialog;
</script>

<h1 class="test-tags-header">Test tags</h1>
{#if tags.length < 1}
    <p class="no-tags-p">This test has no tags</p>
    <button class="add-tag-btn" on:click={() => tagsChangingDialog.open(tags)}>
        Add first tag
    </button>
{:else}
    <div class="test-tags-container">
        {#each tags as tag}
            <span class="test-tag">{tag}</span>
        {/each}
    </div>
    <button class="add-tag-btn" on:click={() => tagsChangingDialog.open(tags)}>
        Change tags
    </button>
{/if}
<TagsChangingDialog
    {maxTagsCount}
    {updateParentElement}
    {testId}
    bind:this={tagsChangingDialog}
/>

<style>
    .test-tags-header {
        color: var(--text);
        font-size: 32px;
        margin: 12px;
    }
    .no-tags-p {
        margin: 8px 0;
        font-size: 28px;
        color: var(--text);
        background-color: var(--back-secondary);
        padding: 24px 32px;
        border-radius: 8px;
        border: 2px var(--primary) dashed;
    }
    .add-tag-btn {
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 6px 16px;
        font-size: 20px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .add-tag-btn:hover {
        background-color: var(--primary-hov);
        padding: 6px 20px;
    }
    .test-tags-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: center;
        flex-wrap: wrap;
        max-width: 64vw;
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
