<script lang="ts">
    import type { DraftTestBriefInfo } from "../../../ts/page_classes/my_tests_page/DraftTestBriefInfo";
    import type { PublishedTestBriefInfo } from "../../../ts/page_classes/my_tests_page/PublishedTestBriefInfo";
    import DraftTestsTab from "./my_tests_tabs/DraftTestsTab.svelte";
    import PublishedTestsTab from "./my_tests_tabs/PublishedTestsTab.svelte";
    import NewTestCreationDialog from "./new_test_creation_components/NewTestCreationDialog.svelte";

    enum MyTestsPageTabs {
        DraftTests = "Draft Tests",
        PublishedTests = "Published Tests",
    }
    let currentActiveTab: MyTestsPageTabs = MyTestsPageTabs.DraftTests;
    const tabs = Object.values(MyTestsPageTabs);

    let draftTests: DraftTestBriefInfo[] = [];
    let publishedTests: PublishedTestBriefInfo[] = [];

    let testCreationDialog: NewTestCreationDialog;
</script>

<NewTestCreationDialog bind:this={testCreationDialog} />
<div class="tabs-container unselectable">
    <div class="tab-links-container">
        {#each tabs as tab}
            <div
                class="tab-link {currentActiveTab === tab
                    ? 'active-tab-link'
                    : ''}"
                on:click={() => (currentActiveTab = tab)}
            >
                {tab}
            </div>
        {/each}
    </div>
    <div class="tab-content-container">
        {#if currentActiveTab === MyTestsPageTabs.DraftTests}
            <DraftTestsTab bind:draftTests />
        {:else if currentActiveTab === MyTestsPageTabs.PublishedTests}
            <PublishedTestsTab bind:publishedTests />
        {:else}
            <p>Something went wrong</p>
        {/if}
    </div>
</div>
<div class="create-new-btn" on:click={() => testCreationDialog.open()}>
    Create New Test
</div>

<style>
    .tabs-container {
        display: grid;
        grid-template-rows: auto 1fr;
        padding-bottom: 60px;
    }

    .tab-links-container {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 3%;
    }
    .tab-link {
        width: 200px;
        height: 44px;
        border-radius: 4px;
        background-color: var(--primary);
        color: var(--back-main);
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
        transition: all 0.1s;
    }
    .tab-link:hover {
        transform: translateY(2px);
    }
    .active-tab-link {
        text-decoration: underline;
        text-decoration-color: var(--back-main);
        text-decoration-thickness: 2px;
        text-underline-offset: 4px;
    }
    .create-new-btn {
        width: 200px;
        height: 44px;
        border-radius: 6px;
        background-color: var(--primary);
        color: var(--back-main);
        font-size: 20px;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
        transition: all 0.12s ease-in;
        position: fixed;
        bottom: 6px;
        left: 50%;
        transform: translateX(-50%);
        margin-bottom: 40px;
    }
    .create-new-btn:hover {
        width: 220px;
        background-color: var(--primary-hov);
    }
</style>
