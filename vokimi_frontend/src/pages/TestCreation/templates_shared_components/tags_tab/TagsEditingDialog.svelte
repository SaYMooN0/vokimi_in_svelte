<script lang="ts">
    import { onDestroy } from "svelte";
    import type { TestCreationTagsTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";

    export let updateParentElementData: () => void;
    export let testId: string;

    let tagsData: TestCreationTagsTabData;
    let dialogElement: BaseDraftTestEditingDialog;
    let tagsToChooseFrom: string[] = [];
    let searchTimeout: ReturnType<typeof setTimeout>;
    let tagSearchInput: string = "";

    export function open(tags: TestCreationTagsTabData) {
        dialogElement.setErrorMessage("");
        tagsData = tags;
        dialogElement.open();
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
        tagsData.tags = [...tagsData.tags, tag];
    }
    async function searchTags(tag: string) {
        dialogElement.setErrorMessage("");
        const response = await fetch(
            `/api/tags/searchTags/${encodeURIComponent(tag)}`,
        );
        if (response.ok) {
            const data = await response.json();
            tagsToChooseFrom = data;
        } else if (response.status === 400) {
            const errorMessage = await getErrorFromResponse(response);
            console.log(errorMessage);
            dialogElement.setErrorMessage(errorMessage);
        } else {
            throw new Error("Unknown error");
        }
    }
    $: if (!StringUtils.isNullOrWhiteSpace(tagSearchInput)) {
        if (searchTimeout) {
            clearTimeout(searchTimeout);
        }

        searchTimeout = setTimeout(() => {
            searchTags(tagSearchInput);
        }, 210);
    }

    onDestroy(() => {
        if (searchTimeout) {
            clearTimeout(searchTimeout);
        }
    });
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
                <div class="search-input-container">
                    <svg class="search-icon" viewBox="0 0 24 24" fill="none">
                        <path
                            d="M17.5 17.5L22 22"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                        <path
                            d="M20 11C20 6.02944 15.9706 2 11 2C6.02944 2 2 6.02944 2 11C2 15.9706 6.02944 20 11 20C15.9706 20 20 15.9706 20 11Z"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linejoin="round"
                        />
                    </svg>
                    <input
                        class="search-input"
                        placeholder="Search for tag..."
                        bind:value={tagSearchInput}
                        name={"tag-search-" + StringUtils.randomString(12)}
                        maxlength={tagsData.maxTagNameLength}
                    />
                    <svg
                        class="reset-button"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        stroke-width="2"
                        on:click={() => (tagSearchInput = "")}
                    >
                        <path
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            d="M6 18L18 6M6 6l12 12"
                        ></path>
                    </svg>
                </div>
                <div class="tags-to-choose-list">
                    {#each tagsToChooseFrom as tag}
                        <div class="tag-display">
                            <label>#{tag}</label>
                            {#if tagsData.tags.includes(tag)}
                                <svg viewBox="0 0 24 24" fill="none">
                                    <path
                                        d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7"
                                        stroke="currentColor"
                                        stroke-width="1.5"
                                        stroke-linecap="round"
                                        stroke-linejoin="round"
                                    />
                                </svg>
                            {:else}
                                <svg
                                    class="add-btn"
                                    on:click={() => addTag(tag)}
                                    viewBox="0 0 24 24"
                                    fill="none"
                                >
                                    <path
                                        d="M12 4V20M20 12H4"
                                        stroke="currentColor"
                                        stroke-width="1.5"
                                        stroke-linecap="round"
                                        stroke-linejoin="round"
                                    />
                                </svg>
                            {/if}
                        </div>
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
                    <div class="tag-display">
                        <label>#{tag}</label>
                        <svg
                            class="remove-btn"
                            on:click={() => removeTag(tag)}
                            viewBox="0 0 24 24"
                            fill="none"
                        >
                            <path
                                d="M20 12L4 12"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                        </svg>
                    </div>
                {/each}
            </div>
        </div>
    </div>
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: 1100px;
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
    }

    .tags-to-choose-list {
        width: 100%;
        height: 300px;
        max-height: 300px;
        overflow-y: auto;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .continue-typing {
        font-size: 16px;
        color: var(--grey);
    }

    .search-input-container {
        position: relative;
        width: 90%;
        height: 42px;
        margin-bottom: 8px;
        display: flex;
        align-items: center;
        padding: 4px 12px;
        box-sizing: border-box;
        border-radius: 40px;
        transition: border-radius 0.45s ease;
        background: var(--back-secondary);
    }

    .search-icon {
        width: 20px;
        aspect-ratio: 1/1;
    }

    .search-input {
        font-size: 16px;
        background-color: transparent;
        width: 100%;
        height: 100%;
        padding-inline: 0.5em;
        padding-block: 0.7em;
        border: none;
    }

    .search-input-container:before {
        content: "";
        position: absolute;
        background: var(--primary);
        transform: scaleX(0);
        transform-origin: center;
        width: 100%;
        height: 2px;
        left: 0;
        bottom: 0;
        border-radius: 1px;
        transition: transform 0.25s ease;
    }

    .search-input-container:focus-within {
        border-radius: 4px;
    }

    .search-input:focus {
        outline: none;
    }

    .search-input-container:focus-within:before {
        transform: scale(1);
    }

    .reset-button {
        width: 20px;
        margin-top: 3px;
        cursor: pointer;
        opacity: 0;
        visibility: hidden;
    }

    .search-input:not(:placeholder-shown) ~ .reset-button {
        opacity: 1;
        visibility: visible;
    }

    .tag-display {
        height: 32px;
        width: calc(100% - 6px);
        margin: 4px 0;
        display: grid;
        grid-template-columns: 1fr auto;
        border-radius: 4px;
        box-shadow:
            rgba(0, 0, 0, 0.02) 0px 1px 3px 0px,
            rgba(27, 31, 35, 0.15) 0px 0px 0px 1px;
    }

    .tag-display label {
        margin: auto 6px;
        font-size: 20px;
    }

    .tag-display svg {
        margin: auto 6px;
        height: 76%;
        aspect-ratio: 1/1;
        box-sizing: border-box;
        padding: 4px;
        color: var(--back-main);
        background-color: var(--text-faded);
        border-radius: 16%;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }

    .tag-display svg path {
        stroke-width: 2.6;
    }

    .add-btn:hover {
        background-color: var(--primary);
    }

    .remove-btn:hover {
        background-color: var(--red-del);
    }
</style>
