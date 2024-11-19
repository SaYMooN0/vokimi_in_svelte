<script lang="ts">
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";

    interface CollectionData {
        id: string;
        name: string;
        testCount: number;
        isTestInCollection: boolean;
    }
    export let testId: string;

    let collections: CollectionData[] = [];
    let fetchingErr: string = "";
    let selectedCollections = new Set();
    let isVisible = false;

    export async function open(): Promise<void> {
        isVisible = true;
        await fetchCollections();
    }

    export function close() {
        isVisible = false;
    }

    async function fetchCollections() {
        const response = await fetch(
            `/api/userCollections/getCollectionsInfoForTest/${testId}`,
        );
        console.log(response);
        if (response.ok) {
            const data = await response.json();
            collections = data.collections;
            console.log(collections);
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "An unknown error occurred.";
        }
    }

    async function saveChanges() {}
    function toggleCollection(collectionId: string) {
        if (selectedCollections.has(collectionId)) {
            selectedCollections.delete(collectionId);
        } else {
            selectedCollections.add(collectionId);
        }
    }
</script>

<div class="dropdown" class:is-visible={isVisible}>
    <div class="header">Manage Collections</div>
    <div class="collections">
        {#each collections as collection}
            <div>
                <input
                    type="checkbox"
                    id={collection.id}
                    checked={selectedCollections.has(collection.id)}
                    on:change={() => toggleCollection(collection.id)}
                />
                <label for={collection.id}>
                    {collection.name} ({collection.testCount})
                </label>
            </div>
        {/each}
    </div>
    <button on:click={saveChanges}>Save</button>
</div>
