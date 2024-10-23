<script lang="ts">
    import MiddleSectionDiscussionsTab from "./middle_section_tabs/MiddleSectionDiscussionsTab.svelte";
    import MiddleSectionInfoTab from "./middle_section_tabs/MiddleSectionInfoTab.svelte";
    import MiddleSectionRatingsTab from "./middle_section_tabs/MiddleSectionRatingsTab.svelte";

    export let testId: string = "";
    enum MiddlePartTabs {
        Info,
        Ratings,
        Discussions,
    }
    let activeTab: MiddlePartTabs = MiddlePartTabs.Info;
</script>

<div class="middle-section">
    <div class="tab-links-container">
        <div
            class="tab-link"
            on:click={() => (activeTab = MiddlePartTabs.Info)}
        >
            Information
        </div>
        <div
            class="tab-link"
            on:click={() => (activeTab = MiddlePartTabs.Ratings)}
        >
            Ratings
        </div>
        <div
            class="tab-link"
            on:click={() => (activeTab = MiddlePartTabs.Discussions)}
        >
            Discussions
        </div>
    </div>
    <div class="tabs-content-container">
        <div class:tab-content-visible={activeTab === MiddlePartTabs.Info}>
            <MiddleSectionInfoTab />
        </div>
        <div class:tab-content-visible={activeTab === MiddlePartTabs.Ratings}>
            <MiddleSectionRatingsTab />
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
    }

    .tab-links-container {
    }
    .tabs-content-container div {
        display: none;
    }
    .tab-content-visible {
        display: block !important;
    }
</style>
