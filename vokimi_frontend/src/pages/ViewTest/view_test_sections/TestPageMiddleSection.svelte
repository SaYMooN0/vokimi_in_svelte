<script lang="ts">
    import type { TestInfoTabData } from "../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/TestInfoTabData";
    import MiddleSectionDiscussionsTab from "./middle_section_tabs/MiddleSectionDiscussionsTab.svelte";
    import MiddleSectionInfoTab from "./middle_section_tabs/MiddleSectionInfoTab.svelte";
    import MiddleSectionRatingsTab from "./middle_section_tabs/MiddleSectionRatingsTab.svelte";

    export let testId: string;
    export let testInfoTabData: TestInfoTabData;
    export let startingTab: string | undefined;
    enum MiddlePartTabs {
        Info,
        Ratings,
        Discussions,
    }
    let activeTab: MiddlePartTabs = MiddlePartTabs.Info;
    function setStartingTab() {
        if (startingTab === "ratings") {
            activeTab = MiddlePartTabs.Ratings;
        } else if (startingTab === "discussions") {
            activeTab = MiddlePartTabs.Discussions;
        } else {
            activeTab = MiddlePartTabs.Info;
        }
    }
    setStartingTab();
</script>

<div class="middle-section">
    <div class="tab-links-container unselectable">
        <div
            class="tab-link"
            class:tab-link-active={activeTab === MiddlePartTabs.Info}
            on:click={() => (activeTab = MiddlePartTabs.Info)}
        >
            Information
        </div>
        <div
            class="tab-link"
            class:tab-link-active={activeTab === MiddlePartTabs.Ratings}
            on:click={() => (activeTab = MiddlePartTabs.Ratings)}
        >
            Ratings
        </div>
        <div
            class="tab-link"
            class:tab-link-active={activeTab === MiddlePartTabs.Discussions}
            on:click={() => (activeTab = MiddlePartTabs.Discussions)}
        >
            Discussions
        </div>
    </div>
    <div class="tabs-content-container">
        <div class:tab-content-visible={activeTab === MiddlePartTabs.Info}>
            <MiddleSectionInfoTab tabData={testInfoTabData} />
        </div>
        <div class:tab-content-visible={activeTab === MiddlePartTabs.Ratings}>
            <MiddleSectionRatingsTab {testId} />
        </div>
        <div
            class:tab-content-visible={activeTab === MiddlePartTabs.Discussions}
        >
            <MiddleSectionDiscussionsTab />
        </div>
    </div>
</div>

<style>
    .middle-section {
        margin-top: 12px;
        display: grid;
        grid-row: auto 1fr;
    }
    .tab-links-container {
        display: flex;
        flex-direction: row;
        justify-content: left;
        padding: 0 8px;
        background-color: var(--back-secondary);
        border-radius: 8px 8px 4px 4px;
    }

    .tabs-content-container div {
        display: none;
        animation: fadeIn 0.32s;
    }
    .tab-content-visible {
        display: block !important;
    }
    .tab-link {
        color: var(--text-faded);
        margin: 8px 2px;
        padding: 6px 12px 2px 12px;
        text-align: center;
        cursor: pointer;
        font-size: 18px;
        border-bottom: 2px solid transparent;
        transition: all 0.12s ease-in;
    }
    .tab-link:hover {
        color: var(--text);
    }
    .tab-link-active {
        color: var(--text);
        border-color: var(--primary);
    }

    @keyframes fadeIn {
        from {
            opacity: 0.1;
        }

        to {
            opacity: 1;
        }
    }
</style>
