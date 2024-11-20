<script lang="ts">
    import type { TestCollectionVmDataForCertainTest } from "../../../../ts/page_classes/view_test_page_classes/TestCollectionVmDataForCertainTest";

    export let testId: string;
    export let close: () => void;
    export let selectedCollectionIds: Set<string>;
    export let collections: TestCollectionVmDataForCertainTest[] = [];
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
</script>

<div class="collections">
    {#each collections as collection}
        <div>
            <input
                type="checkbox"
                id={collection.id}
                checked={selectedCollectionIds.has(collection.id)}
                on:change={() => toggleCollection(collection.id)}
            />
            <label for={collection.id}>
                {collection.name} ({collection.testCount})
            </label>
        </div>
    {/each}
    <button on:click={newCollectionBtnPressed}> New collection </button>
</div>
<button on:click={saveChanges}>Save</button>
