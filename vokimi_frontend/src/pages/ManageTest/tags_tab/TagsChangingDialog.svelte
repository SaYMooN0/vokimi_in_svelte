<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import CloseButton from "../../../components/shared/CloseButton.svelte";
    import TagOperatingDisplay from "../../../components/shared/tags/TagOperatingDisplay.svelte";
    import TagsSearchBar from "../../../components/shared/tags/TagsSearchBar.svelte";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let testId: string;
    export let updateParentElement: () => Promise<void>;
    export let maxTagsCount;
    let chosenTags: string[] = [];
    let dialogElement: BaseDialog;
    let tagsToChooseFrom: string[] = [];
    let tagsSearchBar: TagsSearchBar;
    let errorMessage: string = "";

    export function open(testTags: string[]) {
        chosenTags = testTags;
        errorMessage = "";
        tagsToChooseFrom = [];
        dialogElement.open();
        tagsSearchBar.setSearchInputEmpty();
    }
    async function saveChanges() {
        const response = await fetch(
            "/api/manageTest/tags/setTestTags/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(chosenTags),
            },
        );
        if (response.ok) {
            dialogElement.close();
            await updateParentElement();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "Unknown error";
        }
    }
    function removeTag(tag: string) {
        chosenTags = chosenTags.filter((t) => t !== tag);
    }
    function addTag(tag: string) {
        if (!chosenTags.includes(tag)) {
            chosenTags = [...chosenTags, tag];
        }
    }
</script>

<BaseDialog dialogId="tagsChangingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="change-tags-header">Change tags of your test</p>
        <div class="tags-changing-container">
            <div class="left-part">
                <TagsSearchBar
                    bind:this={tagsSearchBar}
                    setErrorMessage={(errMsg) => (errorMessage = errMsg)}
                    setSearchedTags={(tags) => (tagsToChooseFrom = tags)}
                />
                <div class="tags-to-choose-list">
                    {#each tagsToChooseFrom as tag}
                        <TagOperatingDisplay
                            {tag}
                            isTagAdded={chosenTags.includes(tag)}
                            isTagAddingState={true}
                            btnOnClick={() => addTag(tag)}
                        />
                    {/each}
                    <span class="continue-typing">
                        If you don't find the tag you need continue entering the
                        name of the tag
                    </span>
                </div>
            </div>
            <div class="right-part">
                <span class="chosen-tags-label">
                    Tags chosen ({chosenTags.length}/{maxTagsCount}):
                </span>
                {#each chosenTags as tag}
                    <TagOperatingDisplay
                        {tag}
                        isTagAdded={true}
                        isTagAddingState={false}
                        btnOnClick={() => removeTag(tag)}
                    />
                {/each}
            </div>
        </div>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="err-msg">{errorMessage}</p>
        {/if}
        <button class="save-changes-btn" on:click={saveChanges}>
            Save changes
        </button>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        position: relative;
        padding: 12px 32px;
    }

    .change-tags-header {
        font-size: 26px;
        font-weight: 700;
        color: var(--text);
        margin: 12px 0;
    }

    .tags-changing-container {
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
