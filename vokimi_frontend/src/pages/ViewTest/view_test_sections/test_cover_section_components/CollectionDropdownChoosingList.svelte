<script lang="ts">
    import type { TestCollectionVmDataForCertainTest } from "../../../../ts/page_classes/view_test_page_classes/TestCollectionVmDataForCertainTest";
    import { CollectionIconsIdComponentObj } from "../../../../ts/TestCollectionIcons";

    export let testId: string;
    export let close: () => void;
    export let selectedCollectionIds: Set<string>;
    export let collections: TestCollectionVmDataForCertainTest[];
    export let newCollectionBtnPressed: () => void;

    async function saveChanges() {
        close();
    }

    function toggleCollection(collectionId: string) {
        if (selectedCollectionIds.has(collectionId)) {
            selectedCollectionIds.delete(collectionId);
        } else {
            selectedCollectionIds.add(collectionId);
        }
    }
    console.log(collections);
</script>

<div class="collections">
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
            <div class="checkmark-container">
                <div class="checkmark" />
            </div>
        </div>
    {/each}
    <button class="new-collection-btn" on:click={newCollectionBtnPressed}>
        New collection
    </button>
</div>
<button class="save-changes-btn" on:click={saveChanges}>Save</button>

<style>
    .collections {
        display: flex;
        flex-direction: column;
        gap: 4px;
    }
    .new-collection-btn {
        margin-top: 4px;
    }
    .collection-tile {
        height: 32px;
        display: flex;
        align-items: center;
        gap: 8px;
        padding: 4px 8px;
        border-radius: 6px;
        box-sizing: border-box;
        transition: background-color 0.12s ease;
    }
    .collection-tile:hover {
        background-color: var(--back-secondary);
    }
    .collection-tile > :global(svg) {
        height: 100%;
        color: var(--current-collection-color);
    }
    .collection-tile > label {
        color: var(--current-collection-color);
    }
</style>
