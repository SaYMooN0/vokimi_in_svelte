<script lang="ts">
    import { onMount, onDestroy } from "svelte";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import CollectionDropdownNoCollections from "./CollectionDropdownNoCollections.svelte";
    import NewCollectionCreationDialog from "../../../../components/shared/dialogs/NewCollectionCreationDialog.svelte";
    import type { TestCollectionVmDataForCertainTest } from "../../../../ts/page_classes/view_test_page_classes/TestCollectionVmDataForCertainTest";
    import CollectionDropdownChoosingList from "./CollectionDropdownChoosingList.svelte";

    export let testId: string;

    let collections: TestCollectionVmDataForCertainTest[] = [];
    let fetchingErr: string = "";
    let isVisible = false;
    let dropdownRef: HTMLElement;
    let newCollectionDialog: NewCollectionCreationDialog;

    export async function open(): Promise<void> {
        if (collections.length < 1) {
            await fetchCollections();
        }
        setTimeout(() => {
            isVisible = true;
        }, 0);
    }

    export function close() {
        isVisible = false;
    }

    async function fetchCollections() {
        const response = await fetch(
            "/api/testCollections/getCollectionsInfoForTest/" + testId,
        );
        if (response.ok) {
            collections = await response.json();
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "An unknown error occurred.";
        }
    }

    function newCollectionBtnPressed() {
        newCollectionDialog.open();
    }
    function handleOutsideClick(event: MouseEvent) {
        if (dropdownRef && !dropdownRef.contains(event.target as Node)) {
            close();
        }
    }

    onMount(() => {
        document.addEventListener("click", handleOutsideClick);
    });

    onDestroy(() => {
        document.removeEventListener("click", handleOutsideClick);
    });
</script>

<div class="dropdown" bind:this={dropdownRef}>
    <NewCollectionCreationDialog
        bind:this={newCollectionDialog}
        updateParentElementData={fetchCollections}
    />
    <div class="dropdown-menu" class:is-visible={isVisible}>
        {#if !StringUtils.isNullOrWhiteSpace(fetchingErr)}
            <p class="fetching-err">{fetchingErr}</p>
        {:else if collections.length === 0}
            <CollectionDropdownNoCollections
                {testId}
                openCollectionCreationDialog={newCollectionBtnPressed}
            />
        {:else}
            <CollectionDropdownChoosingList
                {testId}
                {close}
                {newCollectionBtnPressed}
                selectedCollectionIds={new Set(
                    collections
                        .filter((c) => c.isTestIsInCollection)
                        .map((c) => c.collectionId),
                )}
                {collections}
            />
        {/if}
    </div>
</div>

<style>
    .dropdown {
        position: relative;
        display: block;
        width: 100%;
    }

    .dropdown-menu {
        position: absolute;
        top: 100%;
        left: 50%;
        transform: translateX(-50%);
        background: var(--back-main);
        border: 1px solid var(--back-secondary);
        box-shadow:
            rgba(0, 0, 0, 0.05) 0px 6px 24px 0px,
            rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
        border-radius: 4px;
        padding: 8px;
        box-sizing: border-box;
        z-index: 1000;
        width: 300px;
        display: none;
    }

    .dropdown-menu.is-visible {
        display: block;
    }

    .collections {
        max-height: 150px;
        overflow-y: auto;
        margin-bottom: 10px;
    }

    .fetching-err {
        color: var(--red-del);
    }

    .dropdown-menu button {
        background-color: #007bff;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    .dropdown-menu button:hover {
        background-color: #0056b3;
    }
</style>
