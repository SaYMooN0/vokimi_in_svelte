<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import type { TestCollectionVmDataForCertainTest } from "../../../../ts/page_classes/view_test_page_classes/TestCollectionVmDataForCertainTest";
    import { CollectionIconsIdComponentObj } from "../../../../ts/TestCollectionIcons";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let testId: string;
    export let close: () => void;
    export let selectedCollectionIds: Set<string>;
    export let collections: TestCollectionVmDataForCertainTest[];
    export let newCollectionBtnPressed: () => void;
    let savingErr: string = "";

    async function saveChanges() {
        savingErr = "";
        const response = await fetch(
            `/api/testCollections/testEntriesInCollectionsChanged/${testId}`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify([...selectedCollectionIds]),
            },
        );

        if (response.ok) {
            const responseData = await response.json();
            console.log(responseData);
            selectedCollectionIds = new Set(responseData);
            close();
        } else if (response.status === 400) {
            savingErr = await getErrorFromResponse(response);
        } else {
            savingErr = "An unknown error occurred.";
        }
    }

    function toggleCollection(collectionId: string) {
        if (selectedCollectionIds.has(collectionId)) {
            selectedCollectionIds.delete(collectionId);
            selectedCollectionIds = selectedCollectionIds;
        } else {
            selectedCollectionIds = new Set([
                ...selectedCollectionIds,
                collectionId,
            ]);
        }
    }
</script>

<div class="collections-list">
    {#each collections as collection}
        <div
            class="collection-tile"
            style="--current-collection-color: {collection.color}"
            on:click={() => toggleCollection(collection.collectionId)}
        >
            <svelte:component
                this={CollectionIconsIdComponentObj[collection.iconName]}
            />
            <label>
                {collection.name}
            </label>
            <div
                class="checkmark-container"
                class:checked={selectedCollectionIds.has(
                    collection.collectionId,
                )}
            >
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M5 14L8.5 17.5L19 6.5"
                        stroke="currentColor"
                        stroke-width="3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>
            </div>
        </div>
    {/each}
</div>
<button class="new-collection-btn" on:click={newCollectionBtnPressed}>
    New collection
</button>
<button class="save-changes-btn" on:click={saveChanges}>Save</button>
{#if !StringUtils.isNullOrWhiteSpace(savingErr)}
    <p class="saving-err">{savingErr}</p>
{/if}

<style>
    .collections-list {
        display: flex;
        flex-direction: column;
        padding: 2px 8px 6px 8px;
        margin: 0;
        max-height: 200px;
        overflow-y: auto;
    }
    .new-collection-btn {
        margin-top: 4px;
    }
    .collection-tile {
        height: 32px;
        display: grid;
        align-items: center;
        grid-template-columns: 32px 1fr 20px;
        gap: 8px;
        padding: 4px 8px;
        border-radius: 4px;
        box-sizing: border-box;
        transition: all 0.08s ease;
    }
    .collection-tile:hover {
        transform: scale(1.03);
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 2px 12px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
    }
    .collection-tile > :global(svg) {
        height: 100%;
        color: var(--current-collection-color);
    }
    .collection-tile > label {
        color: var(--current-collection-color);
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
    }
    .checkmark-container {
        width: 100%;
        aspect-ratio: 1/1;
        box-sizing: border-box;
        background-color: var(--back-main);
        border: 2px solid var(--text-faded);
        border-radius: 4px;
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 0;
    }
    .collection-tile:hover .checkmark-container {
        border-color: var(--primary);
    }
    .checked {
        border-color: var(--primary);
        background-color: var(--primary);
    }
    .checkmark-container > svg {
        height: 100%;
        width: 100%;
        color: transparent;
    }
    .checked > svg {
        color: var(--back-main);
    }
    .new-collection-btn {
        margin: 0 auto;
        display: block;
        margin-top: -2px;
        border: none;
        outline: none;
        background-color: transparent;
        color: var(--text-faded);
        text-decoration: underline;
        font-size: 16px;
        cursor: pointer;
    }
    .new-collection-btn:hover {
        color: var(--primary);
    }
    .save-changes-btn {
        margin: 4px auto;
        display: block;
        border: none;
        font-size: 18px;
        color: var(--back-main);
        background-color: var(--primary);
        border-radius: 80px;
        padding: 4px 24px;
        cursor: pointer;
        transition: all 0.12s ease;
    }
    .save-changes-btn:hover {
        transform: scale(1.04);
        padding: 4px 28px;
    }
    .saving-err {
        color: var(--red-del);
        font-size: 16px;
        font-weight: 500;
        text-align: center;
        margin: 0;
    }
</style>
