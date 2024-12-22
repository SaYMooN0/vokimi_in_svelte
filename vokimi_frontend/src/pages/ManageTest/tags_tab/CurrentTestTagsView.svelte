<script lang="ts">
    import TabSubHeader from "../tabs_shared/TabSubHeader.svelte";
    import TagsChangingDialog from "./TagsChangingDialog.svelte";

    export let tags: string[];
    export let updateParentElement: () => Promise<void>;
    export let testId;
    export let maxTagsCount: number;
    let tagsChangingDialog: TagsChangingDialog;
</script>

<TabSubHeader headerText="Test tags ({tags.length})" />
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
    .no-tags-p {
        margin: 8px 0;
        font-size: 20px;
        color: var(--text);
        background-color: var(--back-secondary);
        padding: 16px 32px;
        width: fit-content;
        box-sizing: border-box;
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
    }
    .test-tags-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        flex-wrap: wrap;
        width: 100%;
        gap: 8px;
        margin: 8px 0;
        box-sizing: border-box;
    }
    .test-tag {
        color: var(--back-main);
        background-color: var(--primary);
        font-size: 16px;
        padding: 4px 8px;
        border-radius: 4px;
    }
</style>
