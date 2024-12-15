<script lang="ts">
    import FeedbackIcon from "../../components/icons/manage_test_tab_icons/FeedbackIcon.svelte";
    import StatisticsIcon from "../../components/icons/manage_test_tab_icons/StatisticsIcon.svelte";
    import TagsIcon from "../../components/icons/manage_test_tab_icons/TagsIcon.svelte";
    import { TestTemplate } from "../../ts/enums/TestTemplate";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import type { ITestStatisticsData } from "../../ts/page_classes/manage_test_page/test_statistics/ITestStatisticsData";
    import ManageTestTabLink from "./page_layout/ManageTestTabLink.svelte";

    export let testId: string;
    async function fetchPageData(): Promise<Err> {
        const response = await fetch("/api/manageTest/checkAccess/" + testId);
        if (response.ok) {
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong...");
        }
    }
    enum PageTab {
        Statistics = "Statistics",
        Feedback = "Feedback",
        Tags = "Tags",
    }
    function getTabLinkIcon(tab: PageTab) {
        switch (tab) {
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
    let currentPageTab: PageTab = PageTab.Statistics;
</script>

{#await fetchPageData()}
    <p>Loading...</p>
{:then err}
    {#if err.notNone()}
        <p class="err-message">{err.toString()}</p>
    {:else}
        <div class="tab-links-container">
            <ManageTestTabLink
                text={PageTab.Statistics}
                onClick={() => (currentPageTab = PageTab.Statistics)}
            >
                <svelte:component this={getTabLinkIcon(PageTab.Statistics)} />
            </ManageTestTabLink>
            <ManageTestTabLink
                text={PageTab.Feedback}
                onClick={() => (currentPageTab = PageTab.Feedback)}
            >
                <svelte:component this={getTabLinkIcon(PageTab.Feedback)} />
            </ManageTestTabLink>
            <ManageTestTabLink
                text={PageTab.Tags}
                onClick={() => (currentPageTab = PageTab.Tags)}
            >
                <svelte:component this={getTabLinkIcon(PageTab.Tags)} />
            </ManageTestTabLink>
        </div>
        <div class="tab-content-container">
            <div class:activeTabContent={currentPageTab === PageTab.Statistics}>
                Statistics content
            </div>
            <div class:activeTabContent={currentPageTab === PageTab.Feedback}>
                Feedback content
            </div>
            <div class:activeTabContent={currentPageTab === PageTab.Tags}>
                Tags content
            </div>
        </div>
    {/if}
{/await}
