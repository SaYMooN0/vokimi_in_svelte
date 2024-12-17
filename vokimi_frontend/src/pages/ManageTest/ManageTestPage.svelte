<script lang="ts">
    import FeedbackIcon from "../../components/icons/manage_test_tab_icons/FeedbackIcon.svelte";
    import OverallIcon from "../../components/icons/manage_test_tab_icons/OverallIcon.svelte";
    import StatisticsIcon from "../../components/icons/manage_test_tab_icons/StatisticsIcon.svelte";
    import TagsIcon from "../../components/icons/manage_test_tab_icons/TagsIcon.svelte";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import FeedbackTabContent from "./feedback_tab/FeedbackTabContent.svelte";
    import OverallTabContent from "./overall_tab/OverallTabContent.svelte";
    import ManageTestTabLink from "./page_layout/ManageTestTabLink.svelte";
    import StatisticsTabContent from "./statistics_tab/StatisticsTabContent.svelte";
    import TagsTabContent from "./tags_tab/TagsTabContent.svelte";

    export let testId: string;
    enum PageTab {
        Overall = "Overall",
        Statistics = "Statistics",
        Feedback = "Feedback",
        Tags = "Tags",
    }
    function getTabLinkIcon(tab: PageTab) {
        switch (tab) {
            case PageTab.Overall:
                return OverallIcon;
            case PageTab.Statistics:
                return StatisticsIcon;
            case PageTab.Feedback:
                return FeedbackIcon;
            case PageTab.Tags:
                return TagsIcon;
            default:
                return StatisticsIcon;
        }
    }
    async function checkPageAccess(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/overall/checkTestAccess/${testId}`,
        );
        if (response.ok) {
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        }
        return new Err("Something went wrong...");
    }
    let currentPageTab: PageTab = PageTab.Statistics;
</script>

{#await checkPageAccess()}
    <p>Loading...</p>
{:then err}
    {#if err.notNone()}
        <p class="err-message">{err.toString()}</p>
    {:else}
        <p class="mage-test-title">Manage the <span>testname</span> test</p>
        <div class="tab-links-container">
            {#each Object.values(PageTab) as tab}
                <ManageTestTabLink
                    text={tab}
                    onClick={() => (currentPageTab = tab)}
                    isActive={currentPageTab === tab}
                >
                    <svelte:component this={getTabLinkIcon(tab)} />
                </ManageTestTabLink>
            {/each}
        </div>
        <div class="tab-content-container">
            <div class:activeTabContent={currentPageTab === PageTab.Overall}>
                <OverallTabContent {testId} />
            </div>
            <div class:activeTabContent={currentPageTab === PageTab.Statistics}>
                <StatisticsTabContent {testId} />
            </div>
            <div class:activeTabContent={currentPageTab === PageTab.Feedback}>
                <FeedbackTabContent {testId} />
            </div>
            <div class:activeTabContent={currentPageTab === PageTab.Tags}>
                <TagsTabContent {testId} />
            </div>
        </div>
    {/if}
{/await}

<style>
    .mage-test-title {
        text-align: center;
        color: var(--text-faded);
        font-size: 24px;
        font-weight: 600;
        margin: 0;
    }
    .tab-links-container {
        margin-top: 4px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        width: 100%;
        gap: max(24px, calc(3.2vw));
    }
    .tab-content-container div {
        display: none;
    }
    .tab-content-container div.activeTabContent {
        display: flex;
        flex-direction: column;
    }
    .err-message {
        color: red;
    }
</style>
