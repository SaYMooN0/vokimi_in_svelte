<script lang="ts">
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import type { TestCreationTagsTabData } from "../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import TagOperatingDisplay from "../../../../components/shared/tags/TagOperatingDisplay.svelte";
    import TagsSearchBar from "../../../../components/shared/tags/TagsSearchBar.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    let tagsData: TestCreationTagsTabData;
    let dialogElement: BaseDraftTestEditingDialog;
    let tagsToChooseFrom: string[] = [];
    let tagsSearchBar: TagsSearchBar;

    export function open(tags: TestCreationTagsTabData) {
        tagsToChooseFrom = [];
        dialogElement.setErrorMessage("");
        tagsData = tags.copy();
        dialogElement.open();
        tagsSearchBar.setSearchInputEmpty();
    }

    async function saveData() {
        const response = await fetch(
            "/api/tags/updateDraftTestTags/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(tagsData.tags),
            },
        );
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
    function removeTag(tag: string) {
        tagsData.tags = tagsData.tags.filter((t) => t !== tag);
    }
    function addTag(tag: string) {
        if (!tagsData.tags.includes(tag)) {
            tagsData.tags = [...tagsData.tags, tag];
        }
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    <div class="dialog-content">
        <div class="add-tags-header">
            <label>Add tags to your test</label>
            <p>
                Be careful when choosing tags. They will determine how easy it
                is for the user to find your test.
            </p>
        </div>
        <div class="tags-adding-container">
            <div class="left-part">
                <TagsSearchBar
                    bind:this={tagsSearchBar}
                    setErrorMessage={dialogElement.setErrorMessage}
                    setSearchedTags={(tags) => (tagsToChooseFrom = tags)}
                    maxTagNameLength={tagsData.maxTagNameLength}
                />
                <div class="tags-to-choose-list">
                    {#each tagsToChooseFrom as tag}
                        <TagOperatingDisplay
                            {tag}
                            isTagAdded={tagsData.tags.includes(tag)}
                            isTagAddingState={true}
                            btnOnClick={() => addTag(tag)}
                        />
                    {/each}
                    <label class="continue-typing">
                        If you don't find the tag you need continue entering the
                        name of the tag
                    </label>
                </div>
            </div>
            <div class="right-part">
                <label class="chosen-tags-label">
                    Tags chosen ({tagsData.tags
                        .length}/{tagsData.maxTagsForTestCount}):
                </label>
                {#each tagsData.tags as tag}
                    <TagOperatingDisplay
                        {tag}
                        isTagAdded={true}
                        isTagAddingState={false}
                        btnOnClick={() => removeTag(tag)}
                    />
                {/each}
            </div>
        </div>
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .add-tags-header {
        margin-bottom: 20px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .add-tags-header label {
        font-size: 26px;
        font-weight: 700;
        color: var(--text);
        margin-bottom: 4px;
        margin-top: 12px;
    }
    .add-tags-header p {
        margin-top: 4px;
        color: var(--text-faded);
        font-size: 20px;
    }
    .tags-adding-container {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 20px;
    }

    .left-part,
    .right-part {
        display: flex;
        flex-direction: column;
        align-items: center;
        max-height: 52vh;
        overflow-y: auto;
    }

    .tags-to-choose-list {
        height: 300px;
        max-height: 300px;
        overflow-y: auto;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .continue-typing {
        font-size: 16px;
        color: var(--text-faded);
    }
</style>
