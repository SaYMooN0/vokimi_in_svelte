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
    import TabDataFetchingErrDiv from "./tabs_shared/TabDataFetchingErrDiv.svelte";
    import TagsTabContent from "./tags_tab/TagsTabContent.svelte";

    export let testId: string;
    enum PageTab {
        Overall = "Overall",
        Tags = "Tags",
        Statistics = "Statistics",
        Feedback = "Feedback",
    }
    function getTabLinkIcon(tab: PageTab) {
        switch (tab) {
            case PageTab.Overall:
                return OverallIcon;
            case PageTab.Tags:
                return TagsIcon;
            case PageTab.Statistics:
                return StatisticsIcon;
            case PageTab.Feedback:
                return FeedbackIcon;
            default:
                return StatisticsIcon;
        }
    }
    type testName = string;
    async function checkPageAccess(): Promise<testName | Err> {
        const response = await fetch(
            `/api/manageTest/overall/checkTestAccess/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            return data.testName;
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        }
        return new Err("Something went wrong...");
    }
    let currentPageTab: PageTab = PageTab.Statistics;
</script>

{#await checkPageAccess()}
    <p>Loading...</p>
{:then fetchedData}
    {#if fetchedData instanceof Err}
        <TabDataFetchingErrDiv
            err={fetchedData.toString()}
            tryAgainAction={() => {
                location.reload();
            }}
        />
    {:else}
        <p class="mage-test-title">
            Manage the <span>{fetchedData}</span> test
        </p>
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
        <OverallTabContent
            {testId}
            isActive={currentPageTab === PageTab.Overall}
        />
        <TagsTabContent {testId} isActive={currentPageTab === PageTab.Tags} />
        <StatisticsTabContent
            {testId}
            isActive={currentPageTab === PageTab.Statistics}
        />
        <FeedbackTabContent
            {testId}
            isActive={currentPageTab === PageTab.Feedback}
        />
    {/if}
{/await}

<style>
    .mage-test-title {
        text-align: center;
        color: var(--text-faded);
        font-size: 24px;
        font-weight: 600;
        margin: 0;
        display: flex;
        justify-content: center;
    }
    .mage-test-title span {
        margin: 0 6px;
        display: inline-block;
        color: var(--primary);
        max-width: min(320px, 32vw);
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
    }

    .tab-links-container {
        margin-top: 4px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        width: 100%;
        gap: max(24px, calc(3.2vw));
    }
</style>
