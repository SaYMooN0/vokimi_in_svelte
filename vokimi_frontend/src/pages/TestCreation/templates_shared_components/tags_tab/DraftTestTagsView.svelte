<script lang="ts">
    import { TestCreationTagsTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import TabHeaderWithButton from "../../creation_shared_components/TabHeaderWithButton.svelte";
    import TabViewDataLoader from "../../creation_shared_components/TabViewDataLoader.svelte";
    import TagsEditingDialog from "./TagsEditingDialog.svelte";

    export let tagsData: TestCreationTagsTabData;
    export let testId: string;
    let tagsEditingDialog: TagsEditingDialog;

    async function loadData() {
        const url = "/api/tags/getDraftTestTagsData/" + testId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            tagsData = new TestCreationTagsTabData(
                data.tags,
                data.maxTagsForTestCount,
                data.maxTagNameLength,
            );
        } else {
            tagsData = TestCreationTagsTabData.empty();
        }
    }
</script>

<TabViewDataLoader
    {loadData}
    isEmpty={() => {
        return tagsData.isEmpty();
    }}
>
    <div slot="empty" class="no-conclusion-div">
        <h2>Unable to fetch data. Please try again later.</h2>
    </div>
    <div slot="content" class="conclusion-data">
        <TagsEditingDialog
            bind:this={tagsEditingDialog}
            updateParentElementData={loadData}
            {testId}
        />

        {#if tagsData.tags.length > 0}
            <TabHeaderWithButton
                tabName="Chosen Tags: ({tagsData.tags.length === 0
                    ? 'No tags added yet'
                    : tagsData.tags.length}):"
                buttonText="Open Tags Editor"
                onButtonClick={() => tagsEditingDialog.open(tagsData)}
            />
            <div class="tags-display">
                {#each tagsData.tags as tag}
                    <div class="tag-display">
                        <label>#{tag}</label>
                    </div>
                {/each}
            </div>
        {:else}
            <div class="no-tags-display">
                <label>You have not added any tags yet</label>
                <p>Tags will help users to find your test</p>
                <button
                    on:click={() => tagsEditingDialog.open(tagsData)}
                    class="open-tags-editor-btn"
                >
                    Open Tags Editor
                </button>
            </div>
        {/if}
    </div>
</TabViewDataLoader>

<style>
    .tags-display {
        display: flex;
        flex-direction: column;
        gap: 8px;
        padding-left: 8px;
        margin-top: 12px;
    }
    .tag-display {
        display: flex;
        flex-direction: row;
        align-items: center;
        background-color: var(--back-secondary);
        width: fit-content;
        padding: 4px 8px;
        border-radius: 8px;
        border: 2px solid var(--primary);
    }
    .no-tags-display {
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        width: min(420px, 40vw);
        height: min(180px, 40vh);
        border: 2px dashed var(--primary);
        background-color: var(--back-secondary);
        border-radius: 8px;
    }
    .no-tags-display label {
        font-size: 20px;
        color: var(--text-faded);
    }
    .no-tags-display p {
        margin-top: 4px;
        font-size: 22px;
        color: var(--text);
    }
    .open-tags-editor-btn {
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 8px 16px;
        font-size: 16px;
        outline: none;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .open-tags-editor-btn:hover {
        background-color: var(--primary-hov);
    }
    .open-tags-editor-btn:active {
        transform: scale(0.98);
    }
</style>
